using Microsoft.AspNetCore.Identity;

namespace SODP.Domain.Entities;

public class UserToken : IdentityUserToken<int>
{
    public UserToken() : base() { }
}
