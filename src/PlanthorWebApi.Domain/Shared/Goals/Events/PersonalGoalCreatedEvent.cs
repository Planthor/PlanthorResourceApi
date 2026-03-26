using System;
using NodaTime;

namespace PlanthorWebApi.Domain.Shared.Goals.Events;

/// <summary>
/// Domain event raised when a member successfully links a goal
/// to their personal profile via a new <see cref="PersonalGoal"/>.
/// </summary>
/// <param name="personalGoalId">
/// The identifier of the newly created <see cref="PersonalGoal"/> aggregate.
/// </param>
/// <param name="memberId">
/// The identifier of the member who owns this personal goal.
/// </param>
/// <param name="goalId">
/// The identifier of the underlying <c>Goal</c> aggregate being linked.
/// </param>
/// <param name="displayOnProfile">
/// Whether this goal should appear on the member's public profile.
/// </param>
/// <param name="prioritize">
/// The display priority on the member's profile. Range: 0–99.
/// </param>
/// <param name="linkUserAdapter">
/// Whether Strava activity sync is enabled for this personal goal.
/// </param>
/// <param name="clock">
/// The system clock used to timestamp when this event occurred.
/// Injected from the aggregate — never resolved internally —
/// to keep time controllable in tests and time-travel scenarios.
/// </param>
/// <exception cref="ArgumentNullException">
/// Thrown when <paramref name="clock"/> is null.
/// </exception>
public sealed class PersonalGoalCreatedEvent(
    Guid personalGoalId,
    Guid memberId,
    Guid goalId,
    bool displayOnProfile,
    int prioritize,
    bool linkUserAdapter,
    IClock clock) : DomainEvent(clock)
{
    /// <summary>
    /// Gets the unique identifier of the <see cref="PersonalGoal"/>
    /// aggregate that was created.
    /// </summary>
    public Guid PersonalGoalId { get; } = personalGoalId;

    /// <summary>
    /// Gets the identifier of the member who claimed this goal
    /// as their personal goal.
    /// </summary>
    public Guid MemberId { get; } = memberId;

    /// <summary>
    /// Gets the identifier of the underlying <c>Goal</c> aggregate
    /// that this personal goal is linked to.
    /// </summary>
    public Guid GoalId { get; } = goalId;

    /// <summary>
    /// Gets whether this personal goal will be displayed on the
    /// member's public profile.
    /// </summary>
    /// <remarks>
    /// Included so the profile handler can act immediately without
    /// re-fetching the aggregate to check visibility preference.
    /// </remarks>
    public bool DisplayOnProfile { get; } = displayOnProfile;

    /// <summary>
    /// Gets the display priority of this goal on the member's profile.
    /// Lower values indicate higher priority. Range: 0–99.
    /// </summary>
    public int Prioritize { get; } = prioritize;

    /// <summary>
    /// Gets whether Strava activity sync is enabled for this personal goal.
    /// </summary>
    /// <remarks>
    /// When <c>true</c>, the Strava sync handler should begin attributing
    /// incoming Strava activities to this goal if they fall within the
    /// goal's period boundary and sport type filters.
    /// When <c>false</c>, no automatic attribution occurs.
    /// </remarks>
    public bool LinkUserAdapter { get; } = linkUserAdapter;
}
