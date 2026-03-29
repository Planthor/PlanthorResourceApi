using System;
using Backend.Application.Dtos;
using Backend.Application.Shared;

namespace Backend.Application.Members.Queries.PersonalGoalDetails;

public sealed record PersonalGoalDetailsQuery(Guid MemberId, Guid GoalId) : IQuery<PersonalPlanDto>;
