using System.Collections.Generic;

namespace SODP.Shared.DTO
{
    public class ProjectPartDTO  : BaseDTO
    {
        //public int ProjectId { get; set; }

		public string Sign { get; set; }
		
        public string Name { get; set; }

		public ICollection<PartBranchDTO> Branches { get; set; }

    }
}
