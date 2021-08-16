using System.Collections.Generic;

namespace SODP.Shared.DTO
{
    public class UserDTO : BaseDTO
    {
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
}
