using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SODP.Shared.Response;
using System;

namespace SODP.UI.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public string PageClass { get; set; }
        public string PageClassPrevNext { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            int left = Math.Max(PageModel.CurrentPage - 3, 1);
            int right = Math.Min(PageModel.TotalPages, PageModel.CurrentPage + 3);

            if(right > PageModel.TotalPages - 3)
            {
                right = PageModel.TotalPages - 1;
            }
            if(PageModel.CurrentPage > PageModel.TotalPages - 8)
            {
                left = PageModel.TotalPages - 10;
            }
            if (PageModel.CurrentPage > PageModel.TotalPages - 7)
            {
                left = PageModel.TotalPages - 9;
            }
            if (PageModel.CurrentPage > PageModel.TotalPages - 6)
            {
                left = PageModel.TotalPages - 8;
            }
            if (PageModel.CurrentPage < 6)
            {
                right = 9;
            }
            if (PageModel.TotalPages < 12)
            {
                right = PageModel.TotalPages-1;
            }
            if (left < 4 || PageModel.TotalPages < 12)
            {
                left = 2;
            }

            string url;
            TagBuilder tag;
            var tagBuilder = new TagBuilder("div");

            if (PageModel.CurrentPage > 1)
            {
                tag = new TagBuilder("a");
                url = PageModel.Url.Replace(":", (PageModel.CurrentPage - 1).ToString());
                tag.Attributes["href"] = url;
                tag.AddCssClass(PageClassPrevNext);
                tag.InnerHtml.Append("Poprzednia");
                tagBuilder.InnerHtml.AppendHtml(tag);
            }

            url = PageModel.Url.Replace(":", "1");
            tag = new TagBuilder("a");
            tag.Attributes["href"] = url;
            tag.AddCssClass(PageClass);
            tag.AddCssClass(1 == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
            tag.InnerHtml.Append("1");
            tagBuilder.InnerHtml.AppendHtml(tag);

            if (left > 3)
            {
                url = PageModel.Url.Replace(":", "...");
                tag = new TagBuilder("a");
                tag.Attributes["href"] = url;
                tag.AddCssClass(PageClass);
                tag.AddCssClass(PageClassNormal);
                tag.InnerHtml.Append("...");
                tagBuilder.InnerHtml.AppendHtml(tag);
            }

            for (int i=left;i<=right; i++)
            {
                url = PageModel.Url.Replace(":", i.ToString());
                tag = new TagBuilder("a");
                tag.Attributes["href"] = url;
                tag.AddCssClass(PageClass);
                tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                tag.InnerHtml.Append(i.ToString());
                tagBuilder.InnerHtml.AppendHtml(tag);
            }

            if (right < PageModel.TotalPages - 2)
            {
                url = PageModel.Url.Replace(":", "...");
                tag = new TagBuilder("a");
                tag.Attributes["href"] = url;
                tag.AddCssClass(PageClass);
                tag.AddCssClass(PageClassNormal);
                tag.InnerHtml.Append("...");
                tagBuilder.InnerHtml.AppendHtml(tag);
            }

            url = PageModel.Url.Replace(":", PageModel.TotalPages.ToString());
            tag = new TagBuilder("a");
            tag.Attributes["href"] = url;
            tag.AddCssClass(PageClass);
            tag.AddCssClass(PageModel.TotalPages == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
            tag.InnerHtml.Append(PageModel.TotalPages.ToString());
            tagBuilder.InnerHtml.AppendHtml(tag);

            if (PageModel.CurrentPage < PageModel.TotalPages)
            {
                tag = new TagBuilder("a");
                url = PageModel.Url.Replace(":", (PageModel.CurrentPage + 1).ToString());
                tag.Attributes["href"] = url;
                tag.AddCssClass(PageClassPrevNext);
                tag.InnerHtml.Append("Następna");
                tagBuilder.InnerHtml.AppendHtml(tag);
            }

            output.Content.AppendHtml(tagBuilder.InnerHtml);
        }
    }
}
