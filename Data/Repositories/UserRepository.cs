using Data.Contexts;
using Infrastructure.Data.Entities;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context)
{

}
