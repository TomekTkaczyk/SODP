using System.Collections.Generic;

namespace SODP.Shared.DTO
{
    public class ProjectPartDTO  : BaseDTO
    {
		public string Sign { get; set; }
		
        public string Name { get; set; }

        public int Order { get; set; }

		public ICollection<PartBranchDTO> Branches { get; set; }

    }
}
