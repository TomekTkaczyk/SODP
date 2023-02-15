using Microsoft.AspNetCore.Identity;

namespace SODP.Model;

public class Role : IdentityRole<int>
{
    public Role() : base() { }

    public Role(string roleName) : base(roleName) { }
}
