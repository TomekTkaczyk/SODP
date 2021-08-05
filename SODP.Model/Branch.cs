using SODP.Model.Extensions;
using System.Collections.Generic;

namespace SODP.Model
{
    public class Branch : BaseEntity
    {
        public string Symbol { get; set; }
        public string Sign { get; set; }
        public string Title { get; set; }
        public bool ActiveStatus { get; set; }
        public virtual ICollection<BranchLicense> Licenses { get; set; }

        public void Normalize()
        {
            Sign = Sign.ToUpper();
            Title = Title.CapitalizeFirstLetter();
        }

        public override string ToString()
        {
            return $"{Symbol.Trim()}:{ Sign.Trim()} {Title.Trim()}";
        }
    }
}
