using Microsoft.AspNetCore.Identity;

namespace SODP.Domain.Entities;

public class Role : IdentityRole<int>
{
    public Role() : base() { }

    public Role(string roleName) : base(roleName) { }
}
