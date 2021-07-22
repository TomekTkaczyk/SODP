using SODP.Model.Extensions;

namespace SODP.Model
{
    public class Branch : BaseEntity
    {
        public string Sign { get; set; }
        public string Title { get; set; }
        public bool ActiveStatus { get; set; }

        public void Normalize()
        {
            Sign = Sign.ToUpper();
            Title = Title.CapitalizeFirstLetter();
        }
    }
}
