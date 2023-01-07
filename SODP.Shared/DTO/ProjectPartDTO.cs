using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class ProjectPartDTO  : BaseDTO
    {
        public ProjectDTO Project { get; set; }
		public PartDTO Part { get; set; }

		public ICollection<PartBranchDTO> Branches { get; set; }

    }
}
