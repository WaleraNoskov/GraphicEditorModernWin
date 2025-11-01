using GraphicEditorModernWin.Core.Entities;
using GraphicEditorModernWin.Core.Services;

namespace GraphicEditorModernWin.StandartPack.Services;

internal class LayersService : ILayersService
{
    private readonly List<Layer> _layers = [];
    private readonly Dictionary<int, Guid> _layersOrder = [];

    public IReadOnlyCollection<Layer> Layers => _layers;
    public IReadOnlyDictionary<int, Guid> LayersOrder => _layersOrder;

    public void AddLayer(Layer layer, int order = -1)
    {
        _layers.Add(layer);

        var lastIndex = _layersOrder.Keys.Max();

        if (order < 0 || order > lastIndex)
            _layersOrder.Add(lastIndex + 1, layer.Id);
        else
        {
            var keysToShift = _layersOrder.Keys.Where(k => k >= order).OrderByDescending(k => k).ToList();
            foreach (var key in keysToShift)
            {
                var layerId = _layersOrder[key];
                _layersOrder.Remove(key);
                _layersOrder.Add(key + 1, layerId);
            }
        }
        _layersOrder.Add(order, layer.Id);
    }

    public Layer? GetLayerById(Guid id) => _layers.FirstOrDefault(l => l.Id == id);

    public void RemoveLayer(Guid id)
    {
        var layer = _layers.FirstOrDefault(l => l.Id == id);

        if (layer != null)
        {
            _layers.Remove(layer);

            var orderKey = _layersOrder.FirstOrDefault(kvp => kvp.Value == id).Key;
            if (orderKey == 0 && !_layersOrder.ContainsKey(orderKey))
                return;

            _layersOrder.Remove(orderKey);
            var keysToShift = _layersOrder.Keys.Where(k => k > orderKey).OrderBy(k => k).ToList();
            foreach (var key in keysToShift)
            {
                var layerId = _layersOrder[key];
                _layersOrder.Remove(key);
                _layersOrder.Add(key - 1, layerId);
            }
        }
    }
}
