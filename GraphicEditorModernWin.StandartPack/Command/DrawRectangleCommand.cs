using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.StandartPack.Contracts;

namespace GraphicEditorModernWin.StandartPack.Command;

public record struct DrawRectangleCommand(Guid LayerId, int Thickness, Rectangle Rectangle, Bgra? FirstColor, Bgra? SecondColor) : ICommand;
