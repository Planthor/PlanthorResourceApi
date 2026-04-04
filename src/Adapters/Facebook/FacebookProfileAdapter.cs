using Adapters.Abstraction;

namespace Adapters.Facebook;

public class FacebookProfileAdapter : ISocialProfileAdapter
{
    public string ProviderId => "Facebook";

    public Task<Stream?> GetProfilePictureStreamAsync(string externalPath, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
