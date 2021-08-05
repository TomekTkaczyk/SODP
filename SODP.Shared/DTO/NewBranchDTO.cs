using System.ComponentModel.DataAnnotations;

namespace SODP.Shared.DTO
{
    public class NewBranchDTO  : BaseDTO
    {
        public string Symbol { get; set; }

        public string Sign { get; set; }

        public string Title { get; set; }
    }
}
