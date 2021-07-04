using System.Collections.Generic;

namespace SODP.Shared.DTO
{
    public class UserUpdateDTO
    {
        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public IList<string> Roles { get; set; }
    }
}
