using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Shared;

namespace Infrastructure;

public class AzureBlobAvatarStorageService : IAvatarStorageService
{
    private const string ContainerName = "avatars";

    public Task DeleteAvatarAsync(string blobUri, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> UploadAvatarAsync(
        Guid memberId,
        Stream fileStream,
        string contentType,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


