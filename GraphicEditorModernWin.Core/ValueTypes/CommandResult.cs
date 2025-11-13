using OpenCvSharp;

namespace GraphicEditorModernWin.Core.ValueTypes;

public record struct CommandResult(Guid LayerId, Rectangle Region, Mat Before);
