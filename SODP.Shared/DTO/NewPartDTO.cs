using System.ComponentModel.DataAnnotations;

namespace SODP.Shared.DTO
{
    public class NewPartDTO  : BaseDTO
    {
        public string Sign { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Sign.Trim()} {Name.Trim()}";
        }
    }
}
