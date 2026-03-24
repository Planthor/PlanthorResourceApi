using System;
using System.Collections.Generic;
using NodaTime;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Domain;

public class Goal : AggregateRoot<Guid>
{
    private readonly List<ActivityLog> _activityLogs = [];

    private Goal() { }

    /// <summary>Gets the display name of this goal.</summary>
    public string Name { get; private set; } = default!;

    /// <summary>
    /// Gets the unit of measurement for this goal's target and activity logs.
    /// </summary>
    /// <example>km, steps, hours</example>
    public string Unit { get; private set; } = default!;

    /// <summary>Gets the numeric target this goal aims to reach.</summary>
    public float Target { get; private set; }

    /// <summary>Gets the current aggregate value across all activity logs.</summary>
    public float CurrentValue { get; private set; }

    /// <summary>Gets the UTC instant at which this period starts.</summary>
    public Instant From { get; }

    /// <summary>Gets the UTC instant at which this period ends.</summary>
    public Instant To { get; }

    /// <summary>
    /// Gets the local start date as an ISO string.
    /// Used for boundary comparisons against activity local dates.
    /// </summary>
    public string StartDateLocal { get; }

    /// <summary>
    /// Gets the local end date as an ISO string.
    /// Used for boundary comparisons against activity local dates.
    /// </summary>
    public string EndDateLocal { get; }

    /// <summary>
    /// Gets the IANA timezone identifier snapshotted at creation time.
    /// </summary>
    public string Timezone { get; }

    /// <summary>
    /// Gets whether this goal has been completed.
    /// A goal is auto-completed when <see cref="CurrentValue"/> meets
    /// or exceeds <see cref="Target"/>.
    /// </summary>
    public bool Completed { get; private set; }

    /// <summary>
    /// Gets whether activity logging is enabled for this goal.
    /// Defaults to <c>true</c>.
    /// </summary>
    public bool EnableActivityLog { get; private set; } = true;

    /// <summary>
    /// Gets the current lifecycle status of this goal.
    /// </summary>
    public GoalStatus Status { get; private set; }

    /// <summary>
    /// Gets the total number of likes on this goal.
    /// Denormalised for fast read — incremented and decremented
    /// by the <see cref="Like"/> aggregate via domain events.
    /// </summary>
    public int LikeCount { get; private set; }

    /// <summary>
    /// Gets the sport-specific details if this is a sport goal.
    /// <c>null</c> if this is a generic (non-sport) goal.
    /// </summary>
    public SportGoalDetails? SportGoalDetails { get; private set; }

    /// <summary>Gets all activity logs recorded against this goal.</summary>
    public IReadOnlyList<ActivityLog> ActivityLogs => _activityLogs.AsReadOnly();

    public override ValidationResult Validate()
    {
        throw new NotImplementedException();
    }
}
