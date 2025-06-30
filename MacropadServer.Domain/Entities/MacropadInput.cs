using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.Enums;
using System.Text.Json.Serialization;

namespace MacropadServer.Domain.Entities;
public sealed class MacropadInput : Entity
{
    public string? InputName { get; set; }
    public int? InputIndex { get; set; }
    public int? ModIndex { get; set; }
    public string? InputBitMap { get; set; }
    public InputTypeEnum? InputType { get; set; }
    public string? Item1 { get; set; }
    public string? Item2 { get; set; }
    public string? Item3 { get; set; }
    public string? Item4 { get; set; }

    [JsonIgnore]
    public Guid MacropadId { get; set; }
    [JsonIgnore]
    public Macropad Macropad { get; set; } = default!;
}
