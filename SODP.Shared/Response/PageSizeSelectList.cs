using System.Collections.Generic;

namespace SODP.Shared.Response
{
    public class PageSizeSelectList
    {
        public int PageSize { get; set; }
        public static List<int> PageSizeList = new List<int> { 15, 50, 100 };

        public PageSizeSelectList() : this(PageSizeSelectList.PageSizeList[1]) { }

        public PageSizeSelectList(int pageSize)
        {
            PageSize = pageSize;
        }
    }
}
