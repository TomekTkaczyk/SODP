using Microsoft.AspNetCore.Identity;
using SODP.Domain.ValueObjects;

namespace SODP.Domain.Entities;

public class User : IdentityUser<int>, IBaseEntity
{
    public User() : base() { }
    public User(string userName) : base(userName) { }
    public FirstName Firstname { get; private set; }
    public LastName Lastname { get; private set; }

	public override string ToString()
    {
        return Firstname.Value.Trim() + " " + Lastname.Value.Trim();
    }
}
