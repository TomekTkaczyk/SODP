using SODP.UI.Api;
using System.Collections.Generic;

namespace SODP.UI.Pages.Designers.ViewModels;

public class DesignersVM
{
    public IReadOnlyCollection<DesignerVM> Designers { get; set; }
    public PageInfo PageInfo { get; set; }
}
