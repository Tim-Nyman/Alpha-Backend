using Business.Models;
using Data.Repositories;

namespace Business.Services;

public interface IClientService
{
    Task<IEnumerable<Client>?> GetClientsAsync();
}

public class ClientService(ClientRepository clientRepository) : IClientService
{
    private readonly ClientRepository _clientRepository = clientRepository;

    public async Task<IEnumerable<Client>?> GetClientsAsync()
    {
        var entities = await _clientRepository.GetAllAsync();
        if (entities == null)
            return null;
        //Ingen factory då det är så lite som ska mappas, eller ska jag tänka annorlunda? Tänker att jag, om det i framtiden behövs, kan flytta ut mappningen till en Factory.
        var clients = entities.Select(entity => new Client
        {
            Id = entity.Id,
            ClientName = entity.ClientName
        });

        return clients;
    }
}
