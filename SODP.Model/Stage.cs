using SODP.Model.Extensions;
using System.ComponentModel.DataAnnotations;

namespace SODP.Model
{
    public class Stage : BaseEntity
    {
        public Stage(): this("","") { }

        public Stage(string sign): this(sign,"") { }

        public Stage(string sign, string title)
        {
            Sign = sign;
            Title = title;
        }

        public string Sign { get; set; }
        public string Title { get; set; }

        public void Normalize()
        {
            Sign = Sign.ToUpper();
            Title = Title.CapitalizeFirstLetter();
        }
    }
}
