using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Application.Shared;

/// <summary>
/// Port for fetching social provider profile avatars (e.g., from Google or Facebook).
/// </summary>
public interface IAvatarFetchAdapter
{
    /// <summary>
    /// Downloads the avatar image from a remote URL into a stream.
    /// </summary>
    /// <param name="avatarUrl">The remote URL pointing to the user's social profile image.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A tuple containing the stream of the image data and its MIME content type.</returns>
    Task<(Stream Content, string ContentType)> FetchAvatarAsync(
        string avatarUrl,
        CancellationToken cancellationToken);
}
