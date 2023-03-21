using Microsoft.AspNetCore.Identity;

namespace SODP.Domain.Entities;

public class UserLogin : IdentityUserLogin<int>
{
    public UserLogin() : base() { }
}
