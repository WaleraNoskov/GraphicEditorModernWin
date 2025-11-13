using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Core.Services;

public interface IColorPaletteService
{
	Bgra PrimaryColor { get; set; }
	Bgra SecondaryColor { get; set; }
	IReadOnlyList<ColorPalette> Palettes { get; }
	
	event EventHandler? PrimaryColorChanged;
	event EventHandler? SecondaryColorChanged;
	event EventHandler<Guid>? ColorPaletteChanged;
	event EventHandler? ColorPalettesChanged;

	Result AddPalette(ColorPalette palette);
	Result EditPalette(ColorPalette palette);
	Result RemovePalette(Guid id);
	Result<ColorPalette> GetPaletteById(Guid id);
}
