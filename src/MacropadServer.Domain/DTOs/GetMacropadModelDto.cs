using MacropadServer.Domain.Enums;

namespace MacropadServer.Domain.DTOs;
public sealed record GetMacropadModelDto(
    string ModelName,
    string ModelSerialNo,
    string? ModelVersion,
    string? ModelDescription,
    string? ModelImage,
    string? DeviceSupport,
    int ButtonCount,
    int ModCount,
    bool IsScreenExist,
    string? ScreenType,
    double? ScreenSize,
    MacropadConnectionTypeEnum? ConnectionType,
    string? MicrocontrollerType,
    PowerTypeEnum? PowerType,
    bool? Rechargeable,
    string? CaseColor,
    string? CaseMaterial,
    string? CaseDescription);
