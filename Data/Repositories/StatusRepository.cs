using Data.Contexts;
using Infrastructure.Data.Entities;

namespace Data.Repositories;

public class StatusRepository(DataContext context) : BaseRepository<StatusEntity>(context)
{

}
