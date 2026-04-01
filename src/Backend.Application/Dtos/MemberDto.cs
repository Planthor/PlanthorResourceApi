using System;

namespace Backend.Application.Dtos;

public record MemberDto(
    Guid Id,
    string FirstName,
    string MiddleName,
    string LastName,
    string? Description,
    string PathAvatar);
