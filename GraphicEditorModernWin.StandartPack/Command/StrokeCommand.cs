using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.StandartPack.Command;

public record struct StrokeCommand(Guid LayerId, IList<Position> Positions, int Thickness, Bgra Color) : ICommand;
