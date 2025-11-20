using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Feature.Drawing.Contracts;

internal record struct RectangleCommitParameters(Rectangle Rectangle, bool DrawFrame, bool FillFrame) : ICommitParameters;
