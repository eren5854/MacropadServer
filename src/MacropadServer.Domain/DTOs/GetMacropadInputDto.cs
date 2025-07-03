using MacropadServer.Domain.Enums;

namespace MacropadServer.Domain.DTOs;
public sealed record GetMacropadInputDto(
    Guid Id,
    string? InputName,
    int? InputIndex,
    int? ModIndex,
    string? InputBitMap,
    InputTypeEnum? InputType,
    string? Item1,
    string? Item2,
    string? Item3,
    string? Item4);