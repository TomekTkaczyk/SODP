using Microsoft.AspNetCore.Identity;

namespace SODP.Model;

public class UserLogin : IdentityUserLogin<int>
{
    public UserLogin() : base() { }
}
