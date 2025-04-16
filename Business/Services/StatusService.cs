using Business.Models;
using Data.Repositories;

namespace Business.Services;

public interface IStatusService
{
    Task<IEnumerable<Status>?> GetStatusAsync();
}

public class StatusService(StatusRepository statusRepository) : IStatusService
{
    private readonly StatusRepository _statusRepository = statusRepository;

    public async Task<IEnumerable<Status>?> GetStatusAsync()
    {
        var entities = await _statusRepository.GetAllAsync();

        var status = entities.Select(entity => new Status
        {
            Id = entity.Id,
            StatusName = entity.StatusName
        });

        return status;
    }
}
