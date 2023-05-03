using Microsoft.AspNetCore.Identity;

namespace SODP.Domain.Entities;

public class Role : IdentityRole<int>, IBaseEntity
{
    public Role() : base() { }

    public Role(string roleName) : base(roleName) { }
}
