using CSharpFunctionalExtensions;
using GraphicEditorModernWin.Core.Entities;
using GraphicEditorModernWin.Core.Services;
using GraphicEditorModernWin.Core.ValueTypes;
using GraphicEditorModernWin.Utils;

namespace GraphicEditorModernWin.StandartPack.Services;

internal class LayersService : ILayersService
{
    private readonly List<Layer> _layers = [];
    public IReadOnlyCollection<Layer> Layers => _layers;

    public event EventHandler? LayersChanged;
    public event EventHandler<Guid>? LayerChanged;

    public void AddLayer(Bgra fill, int order = -1)
    {
        var orderedLayers = _layers.OrderBy(l => l.Order);

        var lastIndex = _layers.Count == 0
            ? 0
            : orderedLayers.Last().Order;

        var layer = new Layer(new Size(800, 600));

        if (fill != new Bgra(0, 0, 0, 0))
            layer.Drawing.SetTo(fill.ToScalar());

        if (order < 0 || order > lastIndex)
            layer.Order = lastIndex + 1;
        else
        {
            var layersToShift = _layers.Where(l => l.Order >= order);
            foreach (var l in layersToShift)
                l.Order++;
        }

        _layers.Add(layer);

        LayersChanged?.Invoke(this, EventArgs.Empty);
    }

    public Result Edit(Layer layer)
    {
        if (_layers.All(l => l.Id != layer.Id))
            return Result.Failure("Cannot find layer to replace");

        var existingLayer = _layers.First(l => l.Id == layer.Id);
        _layers.Remove(existingLayer);
        _layers.Add(layer);

        LayerChanged?.Invoke(this, layer.Id);
        return Result.Success();
    }

    public Layer? GetLayerById(Guid id) => _layers.FirstOrDefault(l => l.Id == id);

    public void RemoveLayer(Guid id)
    {
        var layer = _layers.FirstOrDefault(l => l.Id == id);

        if (layer == null)
            return;

        _layers.Remove(layer);

        var layersToShift = _layers.Where(l => l.Order > layer.Order);
        foreach (var l in layersToShift)
            l.Order--;

        LayersChanged?.Invoke(this, EventArgs.Empty);
    }
}
