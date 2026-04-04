using Domain.Members;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class MemberRepository(PlanthorDbContext context) : BaseRepository<Member>(context), IMemberRepository;
