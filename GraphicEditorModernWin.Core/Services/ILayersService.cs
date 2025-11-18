using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.Entities;
using GraphicEditorModernWin.Core.ValueTypes;

namespace GraphicEditorModernWin.Core.Services;

public interface ILayersService
{
    public IReadOnlyCollection<Layer> Layers { get; }
    public Layer? GetLayerById(Guid id);
    public void AddLayer(Bgra fill, int order = -1);
    public Result Edit(Layer layer);
    public void RemoveLayer(Guid id);

    public event EventHandler? LayersChanged;
    public event EventHandler<Guid> LayerChanged;
}
