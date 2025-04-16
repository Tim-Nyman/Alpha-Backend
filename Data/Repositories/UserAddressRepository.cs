using Data.Contexts;
using Infrastructure.Data.Entities;

namespace Data.Repositories;

public class UserAddressRepository(DataContext context) : BaseRepository<UserAddressEntity>(context)
{

}
