using Microsoft.AspNetCore.Identity;

namespace SODP.Model;

public class RoleClaim : IdentityRoleClaim<int>
{
    public RoleClaim() : base() { }
}
