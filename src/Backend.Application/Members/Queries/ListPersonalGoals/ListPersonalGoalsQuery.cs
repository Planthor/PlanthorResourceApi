using System;
using System.Collections.Generic;
using Backend.Application.Dtos;
using Backend.Application.Shared;

namespace Backend.Application.Members.Queries.ListPersonalGoals;

public sealed record ListPersonalGoalsQuery(Guid MemberId) : IQuery<IEnumerable<PersonalPlanDto>>;
