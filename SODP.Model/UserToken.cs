using Microsoft.AspNetCore.Identity;

namespace SODP.Model;

public class UserToken : IdentityUserToken<int>
{
    public UserToken() : base() { }
}
