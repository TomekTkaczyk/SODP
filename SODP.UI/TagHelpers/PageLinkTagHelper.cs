using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SODP.Shared.Response;
using SODP.UI.Services;

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
        public string PageClassjakisniewiadomo_znacznik { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var _paginationCalculator = new PaginationCalculator(PageModel.TotalPages, 5, PageModel.CurrentPage);
            
            var tagBuilder = new TagBuilder("div");

            if (PageModel.CurrentPage > 1)
            {
                tagBuilder.InnerHtml.AppendHtml(GetTag(PageModel.CurrentPage - 1, "Poprzednia", PageClassPrevNext));
            }

            tagBuilder.InnerHtml.AppendHtml(GetTag(1, 1.ToString(), 1 == PageModel.CurrentPage ? PageClassSelected : PageClassNormal));

            if (_paginationCalculator.Left > 3)
            {
                tagBuilder.InnerHtml.AppendHtml(GetTag(0, "...", PageClassPrevNext));
            }

            for (int i= _paginationCalculator.Left; i<= _paginationCalculator.Right; i++)
            {
                tagBuilder.InnerHtml.AppendHtml(GetTag(i, i.ToString(), i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal));
            }

            if (_paginationCalculator.Right < PageModel.TotalPages - 2)
            {
                tagBuilder.InnerHtml.AppendHtml(GetTag(0, "...", PageClassPrevNext));
            }

            tagBuilder.InnerHtml.AppendHtml(GetTag(PageModel.TotalPages, PageModel.TotalPages.ToString(), PageModel.TotalPages == PageModel.CurrentPage ? PageClassSelected : PageClassNormal));

            if (PageModel.CurrentPage < PageModel.TotalPages)
            {
                tagBuilder.InnerHtml.AppendHtml(GetTag(PageModel.CurrentPage + 1, "Następna", PageClassPrevNext));
            }

            output.Content.AppendHtml(tagBuilder.InnerHtml);
        }

        private TagBuilder GetTag(int page, string label, string css = "")                       
        {
            if(page < 1)
            {
                page = 1;
            }
            var tag = new TagBuilder("a");
            var url = PageModel.Url.Replace(":", page.ToString());
            tag.Attributes["href"] = url;
            tag.AddCssClass(PageClass);
            tag.AddCssClass(css);
            tag.InnerHtml.Append(label);
            
            return tag; 
        }
    }
}
