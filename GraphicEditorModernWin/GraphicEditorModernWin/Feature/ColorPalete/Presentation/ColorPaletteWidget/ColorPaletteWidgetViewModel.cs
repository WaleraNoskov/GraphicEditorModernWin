using System.Collections.ObjectModel;
using System.Windows.Input;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.Feature.ColorPalete.Busines;
using GraphicEditorModernWin.Feature.Shared.Framework;
using GraphicEditorModernWin.Feature.Shared.Framework.Commands;

namespace GraphicEditorModernWin.Feature.ColorPalete.Presentation.ColorPaletteWidget;

internal class ColorPaletteWidgetViewModel : NotifyPropertyChangedBase
{
    private readonly ColorPaletteModel _model;

    public ColorPaletteWidgetViewModel(ColorPaletteModel model)
    {
        _model = model;
        _model.PropertyChanged += _model_PropertyChanged;

        SetColorCommand = new RelayCommand<Bgra?>(OnSetColorCommand);
		InitializeCommand = new RelayCommand(OnInitializeCommandExecuted);
        SetPrimaryColorCommand = new RelayCommand<Bgra>(OnSetPrimaryColorCommand);
        SetSecondaryColorCommand = new RelayCommand<Bgra>(OnSetSecondaryColorCommand);
    }

    public ObservableCollection<BgraViewModel> Colors { get; } = [];

    private BgraViewModel? _selectedColor;
    public BgraViewModel? SelectedColor
    {
        get => _selectedColor;
        set => SetField(ref _selectedColor, value);
    }

    public Bgra PrimaryColor => _model.PrimaryColor;
    public Bgra SecondaryColor => _model.SecondaryColor;

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
            _model.SetPrimaryColor(color.Value);
        else
            _model.SetSecondaryColor(color.Value);

        SelectedColor = null;
	}

    public ICommand SetPrimaryColorCommand { get; private set; }
    private void OnSetPrimaryColorCommand(Bgra color)
    {
        _model.SetPrimaryColor(color);
    }

    public ICommand SetSecondaryColorCommand { get; private set; }
    private void OnSetSecondaryColorCommand(Bgra color)
    {
        _model.SetSecondaryColor(color);
    }

    public ICommand InitializeCommand { get; private set; }
    private void OnInitializeCommandExecuted()
    {
        _model.Initialize();
    }

    private void _model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        OnPropertyChanged(e.PropertyName);

        if (e.PropertyName == nameof(_model.Colors))
        {
            Colors.Clear();
            foreach (var color in _model.Colors)
                Colors.Add(new BgraViewModel(color));
        }
    }
}
