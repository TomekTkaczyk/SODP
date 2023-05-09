namespace SODP.UI.Pages.Branches.ViewModels
{
    public record BranchVM : NewBranchVM
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
