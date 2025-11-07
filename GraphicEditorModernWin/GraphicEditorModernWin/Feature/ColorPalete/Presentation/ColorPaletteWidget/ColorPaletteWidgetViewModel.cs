using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        _model.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);

		SetPrimaryColorCommand = new RelayCommand<Bgra>(OnSetPrimaryColor);
        SetSecondaryColorCommand = new RelayCommand<Bgra>(OnSetSecondaryColor);
        InitializeCommand = new RelayCommand(OnInitializeCommandExecuted);
	}

    public ReadOnlyObservableCollection<BgraViewModel> Colors => new ReadOnlyObservableCollection<BgraViewModel>(new ObservableCollection<BgraViewModel>(_model.Colors.Select(c => new BgraViewModel(c))));

    private BgraViewModel _selectedColor;
    public BgraViewModel SelectedColor
    {
        get => _selectedColor;
        set => SetField(ref _selectedColor, value);
	}

	public Bgra PrimaryColor => _model.PrimaryColor;
    public Bgra SecondaryColor => _model.SecondaryColor;

	public ICommand SetPrimaryColorCommand { get; private set; }
    private void OnSetPrimaryColor(Bgra color) => _model.SetPrimaryColor(color);

    public ICommand SetSecondaryColorCommand { get; private set; }
    private void OnSetSecondaryColor(Bgra color) => _model.SetSecondaryColor(color);

    public ICommand InitializeCommand { get; private set; }
    private void OnInitializeCommandExecuted()
    {
        _model.Initialize();
    }
}
