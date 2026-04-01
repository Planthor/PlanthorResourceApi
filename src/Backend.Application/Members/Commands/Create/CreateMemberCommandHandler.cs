using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Shared;
using Backend.Domain.Members;
using Backend.Domain.Shared;
using NodaTime;

namespace Backend.Application.Members.Commands.Create;

/// <summary>
/// Handles the creation of a new member, including identity provider mapping 
/// and social avatar orchestration (fetching and uploading to permanent blob storage).
/// </summary>
public class CreateMemberCommandHandler(
    IWriteRepository<Member> memberRepository,
    IAvatarFetchAdapter avatarFetchAdapter,
    IStorageService storageService,
    IClock clock) : ICommandHandler<CreateMemberCommand, Guid>
{
    public async Task<Guid> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        string? finalAvatarPath = request.PathAvatar;

        if (!string.IsNullOrWhiteSpace(request.PathAvatar) && 
            (request.PathAvatar.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || 
             request.PathAvatar.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
        {
            var (avatarStream, contentType) = await avatarFetchAdapter.FetchAvatarAsync(request.PathAvatar, cancellationToken);
            
            await using (avatarStream)
            {
                string fileName = $"avatars/{request.IdentifyName}-{Guid.NewGuid()}.jpg";
                finalAvatarPath = await storageService.UploadAvatarAsync(fileName, avatarStream, contentType, cancellationToken);
            }
        }

        var member = Member.Create(
            request.IdentifyName,
            request.FirstName,
            request.MiddleName ?? string.Empty,
            request.LastName,
            request.Description ?? string.Empty,
            finalAvatarPath ?? string.Empty,
            request.PreferredTimezone,
            clock);

        await memberRepository.AddAsync(member, cancellationToken);
        await memberRepository.SaveChangesAsync(cancellationToken);

        return member.Id;
    }
}
