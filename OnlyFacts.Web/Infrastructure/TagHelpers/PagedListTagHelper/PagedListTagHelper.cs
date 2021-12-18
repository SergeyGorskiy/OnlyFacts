using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OnlyFacts.Web.Infrastructure.TagHelpers.PagedListTagHelper
{
    [HtmlTargetElement("pager", Attributes = PagerListPageIndexAttributeName)]
    [HtmlTargetElement("pager", Attributes = PagerListPageSizeAttributeName)]
    [HtmlTargetElement("pager", Attributes = PagerListTotalCountAttributeName)]
    public class PagedListTagHelper: TagHelper
    {
        private readonly IPagerTagHelperService _tagHelperService;

        public PagedListTagHelper(IPagerTagHelperService tagHelperService)
        {
            _tagHelperService = tagHelperService;
        }


        private const string PagerListPageIndexAttributeName = "asp-paged-list-page-index";
        private const string PagerListPageSizeAttributeName = "asp-paged-list-page-size";
        private const string PagerListTotalCountAttributeName = "asp-paged-list-total-pages";

        [HtmlAttributeName(PagerListPageIndexAttributeName)]
        public int PagedListIndex { get; set; }

        [HtmlAttributeName(PagerListPageSizeAttributeName)]
        public int PagedListSize { get; set; }

        [HtmlAttributeName(PagerListTotalCountAttributeName)]
        public int PagedListTotalCount { get; set; }


        private string DisableCss => "disabled";
        private string PageLinkCss => "page-link";
        private string RootTagCss => "pagination";
        private string ActiveTagCss => "active";
        private string PageItemCss => "page-item";
        private byte VisibleGroupCount => 10;


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (PagedListTotalCount <= 1)
            {
                return;
            }

            var pagerContext = _tagHelperService.GetPagerContext(PagedListIndex, PagedListSize, PagedListTotalCount, VisibleGroupCount);
            var pages = _tagHelperService.GetPages(pagerContext);

            
            base.Process(context, output);
        }
    }
}