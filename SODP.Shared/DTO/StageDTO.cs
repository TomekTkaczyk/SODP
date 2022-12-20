using System.ComponentModel.DataAnnotations;

namespace SODP.Shared.DTO
{
    public class StageDTO  : BaseDTO
    {
        //[Required]
        //[MinLength(2)]
        public string Sign { get; set; }

        //[Required]
        public string Name { get; set; }

        public bool ActiveStatus { get; set; }

        public override string ToString()
        {
            return $"({Sign.Trim()}) {Name.Trim()}";
        }
    }
}
