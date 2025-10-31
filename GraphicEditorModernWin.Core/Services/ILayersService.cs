using GraphicEditorModernWin.Core.Entities;

namespace GraphicEditorModernWin.Core.Services;

public interface ILayersService
{
    public IReadOnlyCollection<Layer> Layers { get; }
    public IReadOnlyDictionary<int, Guid> LayersOrder { get; }
    public Layer? GetLayerById(Guid id);
    public void AddLayer(Layer layer, int order = -1);
    public void RemoveLayer(Guid id);
}
