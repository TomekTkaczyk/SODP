namespace SODP.UI.Pages.Shared.ViewModels;

public class BranchVM
{
    public int Id { get; set; }

    public string Sign { get; set; }

    public string Title { get; set; }

    public int Order { get; set; }

    public bool ActiveStatus { get; set; }

    public override string ToString()
    {
        return $"{Sign.Trim()}-{Title.Trim()}";
    }
}
