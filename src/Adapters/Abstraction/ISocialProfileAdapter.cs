namespace Adapters.Abstraction;

/// <summary>
///
/// </summary>
public interface ISocialProfileAdapter
{
    /// <summary>
    /// The external provider this adapter serves (matches <c>ExternalProvider.Id</c>).
    /// </summary>
    /// <example>"Facebook" | "Google"</example>
    string ProviderId { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="externalPath"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Stream?> GetProfilePictureStreamAsync(string externalPath, CancellationToken cancellationToken);
}
