using System.Collections.Generic;
using System.Numerics;
using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.Feature.Drawing.ViewModel;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using OpenCvSharp;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GraphicEditorModernWin.Feature.Drawing.Views;

internal sealed partial class RenderLayerControl : UserControl
{
    private RenderLayerViewModel? _viewModel;
    private CanvasRenderTarget? _renderTarget;

    private readonly List<Vector2> _currentStroke = [];
    private bool _isDrawing = false;

    public RenderLayerControl()
    {
        InitializeComponent();
        DrawingCanvas.CreateResources += DrawingCanvas_CreateResources;
    }

    private void UserControl_DataContextChanged(Microsoft.UI.Xaml.FrameworkElement sender, Microsoft.UI.Xaml.DataContextChangedEventArgs args)
    {
        if (args.NewValue is not RenderLayerViewModel viewModel)
            return;

        _viewModel?.Dispose();
        _viewModel = viewModel;
        _viewModel.PropertyChanged += _viewModel_PropertyChanged;
    }

    private void DrawingCanvas_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
    {
        _renderTarget = new CanvasRenderTarget(
            sender,
            (float)sender.ActualWidth,
            (float)sender.ActualHeight,
            sender.Dpi);
    }

    private void DrawingCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        var p = e.GetCurrentPoint(DrawingCanvas);
        if (!p.Properties.IsLeftButtonPressed || _viewModel is null)
            return;

        _isDrawing = true;
        _currentStroke.Clear();

		var position = new Vector2((int)(p.Position.X / _viewModel.Zoom), (int)(p.Position.Y / _viewModel.Zoom));
		_currentStroke.Add(position);

        DrawingCanvas.Invalidate();
    }

    private void DrawingCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
    {
        if (!_isDrawing)
            return;

        _isDrawing = false;

        if (_currentStroke.Count > 1)
            _viewModel?.CommitStrokeCommand.Execute(_currentStroke);

        _currentStroke.Clear();
        DrawingCanvas.Invalidate();
    }

    private void DrawingCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
    {
        if (!_isDrawing || _viewModel is null)
            return;

        var p = e.GetCurrentPoint(DrawingCanvas);
        if (p.IsInContact)
        {
            var position = new Vector2((int)(p.Position.X / _viewModel.Zoom), (int)(p.Position.Y / _viewModel.Zoom));
            _currentStroke.Add(position);
            DrawingCanvas.Invalidate();
        }
    }

    private async void DrawingCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
    {
        if (_viewModel?.Bitmap is null)
            return;
		var gotRenderTarget = RenderTargetFromMat(_viewModel.Bitmap);
		if (gotRenderTarget.IsFailure)
			return;


		var drawingSession = args.DrawingSession;
        drawingSession.Antialiasing = CanvasAntialiasing.Aliased;
        drawingSession.Transform = Matrix3x2.CreateScale((float)_viewModel.Zoom);

		DrawingCanvas.Width = _viewModel!.ZoomedWidth;
		DrawingCanvas.Height = _viewModel!.ZoomedHeight;
		CanvasBorder.Width = _viewModel!.ZoomedWidth;
		CanvasBorder.Height = _viewModel!.ZoomedHeight;

		args.DrawingSession.DrawImage(
            gotRenderTarget.Value,
            0, 0,
            new Windows.Foundation.Rect(0, 0, _viewModel.Bitmap.Width, _viewModel.Bitmap.Height),
            1.0f,
            CanvasImageInterpolation.NearestNeighbor
        );

        if (_currentStroke.Count > 1)
            DrawStrokePreview(drawingSession, _currentStroke, ColorFromBgra(_viewModel?.PrimaryColor ?? new Bgra(0, 0, 0, 255)), 1);
    }

    private void DrawStrokePreview(CanvasDrawingSession ds, IReadOnlyList<Vector2> pts, Color color, float size)
    {
        for (int i = 1; i < pts.Count; i++)
            ds.DrawLine(pts[i - 1], pts[i], color, size);
    }

    private Color ColorFromBgra(Bgra bgra) => Color.FromArgb(bgra.A, bgra.R, bgra.G, bgra.B);
    private Result<CanvasRenderTarget> RenderTargetFromMat(Mat mat)
    {
        if (mat.Type() != MatType.CV_8UC4)
        {
            return Result.Failure<CanvasRenderTarget>("Mat must be CV_8UC4");
        }

        int width = mat.Width;
        int height = mat.Height;

        var renderTarget = new CanvasRenderTarget(
            DrawingCanvas, width, height, 96);

        // Берём BGRA пиксели
        if (!mat.GetArray(out Vec4b[] pixels))
            return Result.Failure<CanvasRenderTarget>("Cannot read Mat data");

        byte[] dst = new byte[pixels.Length * 4];

        int di = 0;
        for (int i = 0; i < pixels.Length; i++)
        {
            var p = pixels[i];

            dst[di++] = p.Item0; // B
            dst[di++] = p.Item1; // G
            dst[di++] = p.Item2; // R
            dst[di++] = p.Item3; // A
        }

        renderTarget.SetPixelBytes(dst);
        return renderTarget;
    }

    private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_viewModel.Zoom))
        {
            DrawingCanvas.Invalidate();
        }
    }
}
