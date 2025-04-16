using Data.Contexts;
using Infrastructure.Data.Entities;

namespace Data.Repositories;

public class ClientRepository(DataContext context) : BaseRepository<ClientEntity>(context)
{

}
