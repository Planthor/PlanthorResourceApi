using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Application.Shared;

/// <summary>
/// Port for uploading and managing files in external storage (e.g., Azure Blob Storage).
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Uploads an avatar image stream and returns the public URI.
    /// </summary>
    /// <param name="fileName">The desired name of the file to store.</param>
    /// <param name="content">The stream containing the image data.</param>
    /// <param name="contentType">The MIME type of the image (e.g., "image/jpeg").</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The fully qualified URL to the uploaded avatar.</returns>
    Task<string> UploadAvatarAsync(
        string fileName,
        Stream content,
        string contentType,
        CancellationToken cancellationToken);
}
