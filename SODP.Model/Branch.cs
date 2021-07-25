using SODP.Model.Extensions;
using System.Collections.Generic;

namespace SODP.Model
{
    public class Branch : BaseEntity
    {
        public string Sign { get; set; }
        public string Title { get; set; }
        public bool ActiveStatus { get; set; }
        public virtual ICollection<Licence> Licenses { get; set; }

        public void Normalize()
        {
            Sign = Sign.ToUpper();
            Title = Title.CapitalizeFirstLetter();
        }

        public override string ToString()
        {
            return $"({ Sign.Trim()}) { Title.Trim()}";
        }
    }
}
