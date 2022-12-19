using SODP.Model.Extensions;
using SODP.Model.Interfaces;
using System.Collections.Generic;

namespace SODP.Model
{
    public class Branch : BaseEntity, IActiveStatus
    {
        public int Order { get; set; }
        public string Sign { get; set; }
        public string Name { get; set; }
        public bool ActiveStatus { get; set; }
        public virtual ICollection<LicenseBranch> Licenses { get; set; }

        public void Normalize()
        {
            Sign = Sign.ToUpper();
            Name = Name.CapitalizeFirstLetter();
        }

        public override string ToString()
        {
            return $"{ Sign.Trim()} {Name.Trim()}";
        }
    }
}
