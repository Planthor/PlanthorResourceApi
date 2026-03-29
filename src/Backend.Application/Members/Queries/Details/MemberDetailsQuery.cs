using System;
using Backend.Application.Dtos;
using Backend.Application.Shared;

namespace Backend.Application.Members.Queries.Details;

public sealed record MemberDetailsQuery(Guid Id) : IQuery<MemberDto>;
