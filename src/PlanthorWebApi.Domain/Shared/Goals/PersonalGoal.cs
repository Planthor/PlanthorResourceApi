using System;
using System.Collections.Generic;
using NodaTime;
using PlanthorWebApi.Domain.Shared.Exceptions;
using PlanthorWebApi.Domain.Shared.Goals.Events;

namespace PlanthorWebApi.Domain.Shared.Goals;

/// <summary>
/// Aggregate root representing a member's personal relationship to a goal.
/// </summary>
/// <remarks>
/// Acts as a join aggregate between <c>Member</c> and <c>Goal</c>,
/// enriched with member-specific goal preferences such as display order,
/// profile visibility, and Strava sync opt-in.
/// </remarks>
public class PersonalGoal(
    Guid memberId,
    Guid goalId,
    bool displayOnProfile,
    int prioritize,
    bool linkUserAdapter
    ) : AggregateRoot<Guid>
{
    private const int MaxPrioritize = 999;

    /// <summary>
    /// Gets the ID of the member who owns this personal goal.
    /// </summary>
    public Guid MemberId { get; } = memberId;

    /// <summary>
    /// Gets the ID of the underlying goal.
    /// </summary>
    public Guid GoalId { get; } = goalId;

    /// <summary>
    /// Gets whether this goal is displayed on the member's public profile.
    /// Defaults to <c>true</c>.
    /// </summary>
    public bool DisplayOnProfile { get; } = displayOnProfile;

    /// <summary>
    /// Gets the display priority of this goal on the member's profile.
    /// Lower values indicate higher priority. Range: 0–99.
    /// </summary>
    public int Prioritize { get; } = prioritize;

    /// <summary>
    /// Gets whether Strava activity sync is enabled for this goal.
    /// When <c>true</c>, incoming Strava activities are automatically
    /// recorded as activity logs against this goal if they fall within
    /// the goal's period boundary and sport type filters.
    /// Defaults to <c>true</c>.
    /// </summary>
    public bool LinkUserAdapter { get; } = linkUserAdapter;

    public static PersonalGoal Create(
        Guid memberId,
        Guid goalId,
        bool displayOnProfile,
        int prioritize,
        bool linkUserAdapter,
        Guid createdBy,
        IClock clock)
    {
        var personalGoal = new PersonalGoal(
            memberId,
            goalId,
            displayOnProfile,
            prioritize,
            linkUserAdapter
        )
        {
            Id = Guid.NewGuid()
        };

        personalGoal.StampCreatedAudit(createdBy, clock);

        var result = personalGoal.Validate();

        if (!result.IsValid)
        {
            throw new DomainValidationException(result);
        }

        personalGoal.RaiseDomainEvent(
            new PersonalGoalCreatedEvent(
                personalGoal.Id,
                memberId,
                goalId,
                displayOnProfile,
                prioritize,
                linkUserAdapter,
                clock));

        return personalGoal;
    }

    /// <inheritdoc/>
    public override ValidationResult Validate()
    {
        var errors = new List<ValidationError>();

        if (MemberId == Guid.Empty)
        {
            errors.Add(new ValidationError(
                "memberId", "Member ID is required.", "REQUIRED_MEMBER_ID"));
        }

        if (GoalId == Guid.Empty)
        {
            errors.Add(new ValidationError(
                "goalId", "Goal ID is required.", "REQUIRED_GOAL_ID"));
        }

        if (Prioritize is < 0 or > MaxPrioritize)
        {
            errors.Add(new ValidationError(
                "prioritize", "Priority must be between 0 and 99.", "INVALID_PRIORITY"));
        }

        return errors.Count == 0
            ? new ValidationResult(new List<ValidationError>().AsReadOnly())
            : new ValidationResult(new List<ValidationError>(errors).AsReadOnly());
    }
}
