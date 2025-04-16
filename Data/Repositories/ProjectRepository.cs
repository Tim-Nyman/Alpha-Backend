using Data.Contexts;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context)
{
    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        var entities = await _dbSet
            .Include(x => x.Client)
            .Include(x => x.Status)
            .Include(x => x.User)
                .ThenInclude(x => x.Address)
            .ToListAsync();

        return entities;
    }

    public override async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _dbSet
            .Include(x => x.Client)
            .Include(x => x.Status)
            .Include(x => x.User)
                .ThenInclude(x => x.Address)
            .FirstOrDefaultAsync(expression);

        return entity;
    }
}
