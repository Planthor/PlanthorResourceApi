using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Members.Commands.UploadAvatar;

public class UploadAvatarCommandHandler : IRequestHandler<UploadAvatarCommand, string>
{
    public Task<string> Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
