using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using GraphicEditorModernWin.Feature.Shared.Framework;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GraphicEditorModernWin.Feature.Drawing.ViewModel;

internal class InstrumentViewModel(Contracts.Instruments value, string iconGlyph, string name) : NotifyPropertyChangedBase
{
    public Contracts.Instruments Value { get; } = value;
    public string IconGlyph { get; } = iconGlyph;
    public string Name { get; } = name;
}
