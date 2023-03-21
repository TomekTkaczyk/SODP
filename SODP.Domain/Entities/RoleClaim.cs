using Microsoft.AspNetCore.Identity;

namespace SODP.Domain.Entities;

public class RoleClaim : IdentityRoleClaim<int>
{
    public RoleClaim() : base() { }
}
