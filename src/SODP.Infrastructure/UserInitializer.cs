using Microsoft.AspNetCore.Identity;
using SODP.Domain.Entities;

namespace SODP.Infrastructure;

public class UserInitializer : IDisposable
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public UserInitializer(RoleManager<Role> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public void UserInit()
    {
        CreateRoleIfNotExistAsync( "Administrator").Wait();
        CreateRoleIfNotExistAsync("User").Wait();
        CreateRoleIfNotExistAsync("ProjectManager").Wait();

        CreateUserIfNotExistAsync("Administrator", "Administrator").Wait();
        AddToRoleIfNotExistAsync("Administrator", "Administrator").Wait();

        CreateUserIfNotExistAsync("PManager", "PManager").Wait();
        AddToRoleIfNotExistAsync("PManager", "ProjectManager").Wait();
    }

    private async Task<bool> CreateRoleIfNotExistAsync(string role)
    {
        var result = await _roleManager.RoleExistsAsync(role);

        if (!result)
        {
            var roleResult = await _roleManager.CreateAsync(new Role(role));
            result = roleResult.Succeeded;
        }

        return result;
    }

    private async Task<bool> CreateUserIfNotExistAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            user = new User(userName); 
            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded;
        }

        return true;
    }

    private async Task<bool> AddToRoleIfNotExistAsync(string userName, string role)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (!await _userManager.IsInRoleAsync(user, role))
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        return true;
    }

    public void Dispose()
    {
    }
}
