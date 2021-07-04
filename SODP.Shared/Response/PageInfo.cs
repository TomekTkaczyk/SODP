using System;

namespace SODP.Shared.Response
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalItems,ItemsPerPage));

        public string Url { get; set; }
    }
}
