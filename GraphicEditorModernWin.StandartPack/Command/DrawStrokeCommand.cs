using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.StandartPack.Command;

public record struct DrawStrokeCommand(Guid LayerId, IList<Position> Positions, int Thickness, Bgra Color) : ICommand;
