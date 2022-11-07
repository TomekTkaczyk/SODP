using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class ProjectBranchDTO
    {
        public int Id { get; set; }
        public BranchDTO Branch { get; set; }
        public DesignerDTO Designer { get; set; }
        public DesignerDTO Checker { get; set; }
    }
}
