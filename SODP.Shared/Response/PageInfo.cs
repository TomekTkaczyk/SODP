using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace SODP.Shared.Response
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalItems, ItemsPerPage));
        public string RequestParameters => $"CurrentPage={CurrentPage}&PageSize={ItemsPerPage}";
        public string Url { get; set; }

        //public string GetRequestParameters()
        //{
        //    return $"CurrentPage={CurrentPage}&PageSize={ItemsPerPage}";
        //    if (!String.IsNullOrEmpty(SearchString) && !String.IsNullOrWhiteSpace(SearchString))
        //    {
        //        url += $"&SearchString={SearchString}";
        //    }
        //}
    }
}
