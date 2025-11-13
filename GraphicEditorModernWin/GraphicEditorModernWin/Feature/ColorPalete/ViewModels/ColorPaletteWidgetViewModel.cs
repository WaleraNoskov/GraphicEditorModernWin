using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.Feature.Shared.Framework;
using GraphicEditorModernWin.Feature.Shared.Framework.Commands;

namespace GraphicEditorModernWin.Feature.ColorPalete.ViewModels;

internal class ColorPaletteWidgetViewModel : NotifyPropertyChangedBase
{
    private readonly IColorPaletteService _colorPaletteService;

    public ColorPaletteWidgetViewModel(IColorPaletteService model)
    {
        _colorPaletteService = model;
        _colorPaletteService.PrimaryColorChanged += (_, _) => OnPropertyChanged(nameof(PrimaryColor));
        _colorPaletteService.SecondaryColorChanged += (_, _) => OnPropertyChanged(nameof(SecondaryColor));
        _colorPaletteService.ColorPaletteChanged += (_, e) =>
        {
            if (e != SelectedColorPalette.Id)
                return;

            var getActualPalette = _colorPaletteService.GetPaletteById(e);
            if (getActualPalette.IsFailure)
                return;

            SelectedColorPalette = getActualPalette.Value;
            Colors.Clear();
            foreach (var color in SelectedColorPalette.Colors)
                Colors.Add(new BgraViewModel(color));
        };

		SetColorCommand = new RelayCommand<Bgra?>(OnSetColorCommand);
        InitializeCommand = new RelayCommand(OnInitializeCommandExecuted);
        SetPrimaryColorCommand = new RelayCommand<Bgra>(OnSetPrimaryColorCommand);
        SetSecondaryColorCommand = new RelayCommand<Bgra>(OnSetSecondaryColorCommand);
    }

    public ObservableCollection<BgraViewModel> Colors { get; } = [];

    private ColorPalette _selectedColorPalette;
    public ColorPalette SelectedColorPalette
    {
        get => _selectedColorPalette;
        set => SetField(ref _selectedColorPalette, value);
    }

    private BgraViewModel? _selectedColor;
    public BgraViewModel? SelectedColor
    {
        get => _selectedColor;
        set => SetField(ref _selectedColor, value);
    }

    public Bgra PrimaryColor => _colorPaletteService.PrimaryColor;
    public Bgra SecondaryColor => _colorPaletteService.SecondaryColor;

    private bool _isSecondaryColorSelecting;
    public bool IsSecondaryColorSelecting
    {
        get => _isSecondaryColorSelecting;
        set => SetField(ref _isSecondaryColorSelecting, value);
    }

    public ICommand SetColorCommand { get; private set; }
    private void OnSetColorCommand(Bgra? color)
    {
        if (!color.HasValue)
            return;

        if (!IsSecondaryColorSelecting)
            _colorPaletteService.PrimaryColor = color.Value;
        else
            _colorPaletteService.SecondaryColor = color.Value;

        SelectedColor = null;
    }

    public ICommand SetPrimaryColorCommand { get; private set; }
    private void OnSetPrimaryColorCommand(Bgra color)
    {
        _colorPaletteService.PrimaryColor = color;
    }

    public ICommand SetSecondaryColorCommand { get; private set; }
    private void OnSetSecondaryColorCommand(Bgra color)
    {
        _colorPaletteService.SecondaryColor = color;
    }

    public ICommand InitializeCommand { get; private set; }
    private void OnInitializeCommandExecuted()
    {
        var colors = new List<Bgra>
        {
            new(0, 0, 0, 255),
            new(255, 255, 255, 255),
            new(255, 0, 0, 255),
            new(0, 255, 0, 255),
            new(0, 0, 255, 255)
        };

        _colorPaletteService.AddPalette(new ColorPalette(Guid.NewGuid(), colors));
        SelectedColorPalette = _colorPaletteService.Palettes[0];

		Colors.Clear();
		foreach (var color in SelectedColorPalette.Colors)
			Colors.Add(new BgraViewModel(color));
	}
}
