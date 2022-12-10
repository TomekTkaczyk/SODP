using SODP.Model.Extensions;
using System.ComponentModel.DataAnnotations;

namespace SODP.Model
{
    public class Stage : BaseEntity
    {
        public Stage(): this("","") { }

        public Stage(string sign): this(sign,"") { }

        public Stage(string sign, string name)
        {
            Sign = sign;
            Name = name;
        }

        public bool ActiveStatus { get; set; }
        public string Sign { get; set; }
        public string Name { get; set; }

        public void Normalize()
        {
            Sign = Sign.ToUpper();
            Name = Name.CapitalizeFirstLetter();
        }
    }
}
