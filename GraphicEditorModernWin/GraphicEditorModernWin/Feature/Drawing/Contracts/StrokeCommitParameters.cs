using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Feature.Drawing.Contracts;

internal record struct StrokeCommitParameters(List<Position> Stroke) : ICommitParameters;
