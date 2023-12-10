using System.Collections.Generic;

namespace SODP.UI.Pages.Users.ViewModels;

public class UserVM
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public bool ActiveStatus { get; set; }
    public IList<string> Roles { get; set; }

    public override string ToString()
    {
        return $"{(Firstname ?? "").Trim()} {(Lastname ?? "").Trim()}";
    }
}
