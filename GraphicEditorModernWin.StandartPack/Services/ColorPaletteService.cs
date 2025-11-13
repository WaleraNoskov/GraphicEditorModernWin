using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.StandartPack.Services;

public class ColorPaletteService : IColorPaletteService
{
    private Bgra _primaryColor = new Bgra(0, 0, 0, 255);
    public Bgra PrimaryColor
    {
        get => _primaryColor; 
        set
        {
            _primaryColor = value;
            PrimaryColorChanged?.Invoke(this, EventArgs.Empty);
		}
    }

    private Bgra _secondaryColor = new Bgra(255, 255, 255, 255);
    public Bgra SecondaryColor
    {
        get => _secondaryColor;
        set
        {
            _secondaryColor = value;
            SecondaryColorChanged?.Invoke(this, EventArgs.Empty);
		}
    }
    
    private List<ColorPalette> _palettes = new List<ColorPalette>();
    public IReadOnlyList<ColorPalette> Palettes => _palettes.AsReadOnly();

    public event EventHandler? PrimaryColorChanged;
    public event EventHandler? SecondaryColorChanged;
    public event EventHandler<Guid>? ColorPaletteChanged;
    public event EventHandler? ColorPalettesChanged;

    public Result AddPalette(ColorPalette palette)
    {
        if(_palettes.Any(p => p.Id == palette.Id))
            return Result.Failure("Palette with the same Id already exists.");

        _palettes.Add(palette);

        ColorPalettesChanged?.Invoke(this, EventArgs.Empty);
		return Result.Success();
	}

    public Result EditPalette(ColorPalette palette)
    {
        if(_palettes.All(p => p.Id != palette.Id))
            return Result.Failure("Palette not found.");

        var existingPalette = _palettes.First(p => p.Id == palette.Id);
        var existingIndex = _palettes.IndexOf(existingPalette);
		_palettes.Remove(existingPalette);
        _palettes.Insert(existingIndex, palette);

        ColorPaletteChanged?.Invoke(this, palette.Id);
		return Result.Success();
	}

    public Result RemovePalette(Guid id)
    {
        if(_palettes.All(p => p.Id != id))
            return Result.Failure("Palette not found.");

        var existingPalette = _palettes.First(p => p.Id == id);
        _palettes.Remove(existingPalette);

        ColorPalettesChanged?.Invoke(this, EventArgs.Empty);
		return Result.Success();
	}

    public Result<ColorPalette> GetPaletteById(Guid id)
    {
        if(_palettes.All(p => p.Id != id))
            return Result.Failure<ColorPalette>("Palette not found.");

        var existingPalette = _palettes.First(p => p.Id == id);
        return existingPalette;
	}
}
