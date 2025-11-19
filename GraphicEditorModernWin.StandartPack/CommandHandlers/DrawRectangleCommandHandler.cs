using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.Contracts;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.StandartPack.Command;
using GraphicEditorModernWin.Utils;
using OpenCvSharp;

namespace GraphicEditorModernWin.StandartPack.CommandHandlers;

internal class DrawRectangleCommandHandler(ILayersService layersService) : ICommandHandler<DrawRectangleCommand>
{
    public Result<CommandResult> Execute(DrawRectangleCommand command)
    {
        var layer = layersService.GetLayerById(command.LayerId);

        if (layer is null)
            return Result.Failure<CommandResult>($"Layer with id {command.LayerId} not found");
        if (!RectangleIsInDrawing(command.Rectangle, layer.Drawing))
            return Result.Failure<CommandResult>($"Figure is not in the drawing");
        if (command.FirstColor is null && command.SecondColor is null)
            return Result.Failure<CommandResult>($"At least one color should not be null");

        var region = command.Rectangle.ToRect();
        var before = new Mat(layer.Drawing, region).Clone();

        //inner rectangle
        if (command.SecondColor is not null)
            Cv2.Rectangle(
                layer.Drawing,
                command.Rectangle.ToRect(),
                command.SecondColor.Value.ToScalar(),
                -1
            );

        //outer rectangle
        if (command.FirstColor is not null)
            Cv2.Rectangle(
                layer.Drawing,
                command.Rectangle.ToRect(),
                command.FirstColor.Value.ToScalar(),
                command.Thickness
            );

        return Result.Success(new CommandResult(layer.Id, command.Rectangle, before));
    }

    private bool RectangleIsInDrawing(Rectangle rectangle, Mat drawing) => rectangle.Position.X > 0
            && rectangle.Position.Y > 0
            && rectangle.Position.X + rectangle.Size.Width <= drawing.Width
            && rectangle.Position.Y + rectangle.Size.Height <= drawing.Height;
}
