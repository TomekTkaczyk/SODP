using System.Collections.Generic;

namespace SODP.UI.ViewModels
{
    public class PageSizeSelectListVM
    {
        public int PageSize { get; set; }
        public List<int> PageSizeList { get; set; }

        public PageSizeSelectListVM()
        {
            PageSize = 15;
            PageSizeList = new List<int> { 15,50,100 }; 
        }

        public PageSizeSelectListVM(int pageSize, List<int> list)
        {
            PageSize = pageSize;
            PageSizeList = list;
        }
    }
}
