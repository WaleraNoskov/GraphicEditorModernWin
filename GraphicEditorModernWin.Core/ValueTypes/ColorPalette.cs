using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditorModernWin.Core.ValueTypes;

public record struct ColorPalette(Guid Id, IReadOnlyCollection<Bgra> Colors);