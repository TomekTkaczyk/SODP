using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class ProjectBranchDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DesignerId { get; set; }
        public string DesignerName { get; set; }
        public int CheckerId { get; set; }
        public string CheckerName { get; set; }
    }
}
