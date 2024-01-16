namespace SODP.UI.Pages.Shared.ViewModels
{
    public class BranchVM
    {
        public int Id { get; set; }

        public string Sign { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            return $"{Title.ToUpper()}";
        }
    }

}
