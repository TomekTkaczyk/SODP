using SODP.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace SODP.UI.Pages.ArchiveProjects.ViewModels
{
    public class ProjectVM
    {
        public int Id { get; set; }
 
        public string Number { get; set; }
        
        public int StageId { get; set; }
        
        public string StageSign { get; set; }
        
        public string StageName { get; set; }
        
        public string Stage { get { return $"({StageSign}) {StageName}"; } }
        
        public string Name { get; set; }
                                                 
        public string Title { get; set; }

        public string Address { get; set; }
        
        public string LocationUnit { get; set; }

        public string BuildingCategory { get; set; }

        public string Investor { get; set; }

        public string BuildingPermit { get; set; }
        
        public string Description { get; set; }

		public string DevelopmentDate { get; set; }

		public ProjectStatus Status { get; set; }
        
        public BranchesVM ProjectBranches { get; set; }
        
        public AvailableBranchesVM AvailableBranches { get; set; }


    }
}
