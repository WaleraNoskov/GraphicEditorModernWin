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
	}

    public ReadOnlyObservableCollection<Bgra> Colors => _model.Colors;
    public Bgra PrimaryColor => _model.PrimaryColor;
    public Bgra SecondaryColor => _model.SecondaryColor;

	public ICommand SetPrimaryColorCommand { get; private set; }
    private void OnSetPrimaryColor(Bgra color) => _model.SetPrimaryColor(color);

    public ICommand SetSecondaryColorCommand { get; private set; }
    private void OnSetSecondaryColor(Bgra color) => _model.SetSecondaryColor(color);
}
