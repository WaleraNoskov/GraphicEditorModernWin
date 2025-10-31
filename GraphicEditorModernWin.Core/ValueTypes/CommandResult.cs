using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace GraphicEditorModernWin.Core.ValueTypes;

public record struct CommandResult(Guid LayerId, Rectangle Region, Mat Before);
