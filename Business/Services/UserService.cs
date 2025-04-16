using Business.Models;
using Data.Repositories;
using Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public interface IUserService
{
    Task<bool> CreateUserAsync(AddUserForm userForm);
    Task<IEnumerable<User>?> GetUsersAsync();
}

public class UserService(UserRepository userRepository, UserManager<UserEntity> userManager) : IUserService
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;

    public async Task<bool> CreateUserAsync(AddUserForm userForm)
    {
        if (userForm == null)
            return false;

        var exists = await _userRepository.ExistsAsync(x => x.Email == userForm.Email);
        if (exists)
            return false;

        var entity = new UserEntity
        {
            UserName = userForm.Email,
            FirstName = userForm.FirstName,
            LastName = userForm.LastName,
            Email = userForm.Email,
            Address = new UserAddressEntity()
        };

        var result = await _userManager.CreateAsync(entity, userForm.Password);
        return result.Succeeded;
    }

    public async Task<IEnumerable<User>?> GetUsersAsync()
    {
        var entities = await _userRepository.GetAllAsync();
        if (entities == null)
            return null;

        var users = entities.Select(x => new User
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
        });
        return users;
    }
}
