using System.Collections.Generic;
using Backend.Application.Dtos;
using Backend.Application.Shared;

namespace Backend.Application.Members.Queries.Lists;

public sealed record ListMembersQuery : IQuery<IEnumerable<MemberDto>>;
