using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.Enums;

namespace MacropadServer.Domain.Entities;
public sealed class MacropadModel : Entity
{
    public string ModelName { get; set; } = string.Empty;
    public string ModelSerialNo { get; set; } = string.Empty;
    public string? ModelVersion { get; set; }
    public string? ModelDescription { get; set; }
    public string? ModelImage { get; set; }
    public string? DeviceSupport { get; set; }
    public int ButtonCount { get; set; } = 0;
    public int ModCount { get; set; } = 0;
    public bool IsScreenExist { get; set; }
    public string? ScreenType { get; set; }
    public double? ScreenSize { get; set; }
    public MacropadConnectionTypeEnum? ConnectionType { get; set; }
    public string? MicrocontrollerType { get; set; }
    public PowerTypeEnum? PowerType { get; set; }
    public bool? Rechargeable { get; set; }
    public string? CaseColor { get; set; }
    public string? CaseMaterial { get; set; }
    public string? CaseDescription { get; set; }

}
