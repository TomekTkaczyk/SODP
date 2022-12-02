using SODP.Model.Extensions;
using System.Collections.Generic;

namespace SODP.Model
{
    public class Designer : BaseEntity
    {
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool ActiveStatus { get; set; }
        public virtual ICollection<License> Licenses { get; set; }

        public override string ToString()
        {
            return Firstname.Trim() + " " + Lastname.Trim();
        }

        public void Normalize()
        {
            Firstname = Firstname.CapitalizeFirstLetter();
            Lastname = Lastname.CapitalizeFirstLetter();
        }
    }
}
