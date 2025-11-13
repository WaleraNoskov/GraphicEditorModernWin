namespace GraphicEditorModernWin.Core.ValueTypes;

public record struct ColorPalette(Guid Id, IReadOnlyCollection<Bgra> Colors);