using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace OnlyFacts.Web.Infrastructure.TagHelpers.PagedListTagHelper
{
    [HtmlTargetElement("pager", Attributes = PagerListPageIndexAttributeName)]
    [HtmlTargetElement("pager", Attributes = PagerListPageSizeAttributeName)]
    [HtmlTargetElement("pager", Attributes = PagerListTotalCountAttributeName)]
    [HtmlTargetElement("pager", Attributes = PagerListActionAttributeName)]
    [HtmlTargetElement("pager", Attributes = HostAttributeName)]
    [HtmlTargetElement("pager", Attributes = FragmentAttributeName)]
    [HtmlTargetElement("pager", Attributes = PagedListRouteAttributeName)]
    [HtmlTargetElement("pager", Attributes = PagedListRouteDataAttributeName)]
    [HtmlTargetElement("pager", Attributes = ProtocolAttributeName)]
    [HtmlTargetElement("pager", Attributes = ControllerAttributeName)]
    public class PagedListTagHelper: TagHelper
    {
        private readonly IPagerTagHelperService _tagHelperService;
        private readonly IDictionary<string, string> _routeValues = new Dictionary<string, string>();

        protected IHtmlGenerator Generator { get; set; }

        public PagedListTagHelper(IPagerTagHelperService tagHelperService, IHtmlGenerator htmlGenerator)
        {
            _tagHelperService = tagHelperService;
            Generator = htmlGenerator;
        }

        
        private const string PagerListPageIndexAttributeName = "asp-paged-list-page-index";
        private const string PagerListPageSizeAttributeName = "asp-paged-list-page-size";
        private const string PagerListTotalCountAttributeName = "asp-paged-list-total-count";
        private const string PagerListActionAttributeName = "asp-paged-list-url";
        private const string HostAttributeName = "asp-host";
        private const string FragmentAttributeName = "asp-fragment";
        private const string PagedListRouteAttributeName = "asp-route-parameter";
        private const string PagedListRouteDataAttributeName = "asp-route-data";
        private const string ProtocolAttributeName = "asp-protocol";
        private const string ControllerAttributeName = "asp-controller";


        [ViewContext] 
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(PagerListPageIndexAttributeName)]
        public int PagedListIndex { get; set; }

        [HtmlAttributeName(PagerListPageSizeAttributeName)]
        public int PagedListSize { get; set; }

        [HtmlAttributeName(PagerListTotalCountAttributeName)]
        public int PagedListTotalCount { get; set; }

        [HtmlAttributeName(PagerListActionAttributeName)]
        public string ActionName { get; set; }

        [HtmlAttributeName(PagedListRouteAttributeName)]
        public string RouteParameter { get; set; }

        [HtmlAttributeName(PagedListRouteDataAttributeName)]
        public object? RouteParameters { get; set; }

        [HtmlAttributeName(FragmentAttributeName)]
        public string Fragment { get; set; }

        [HtmlAttributeName(ProtocolAttributeName)]
        public string Protocol { get; set; }

        [HtmlAttributeName(HostAttributeName)]
        public string Host { get; set; }

        [HtmlAttributeName(ControllerAttributeName)]
        public string Controller { get; set; }

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

            var ul = new TagBuilder("ul");
            ul.AddCssClass(RootTagCss);

            var pagerContext = _tagHelperService.GetPagerContext(PagedListIndex, PagedListSize, PagedListTotalCount, VisibleGroupCount);
            var pages = _tagHelperService.GetPages(pagerContext);

            foreach (var page in pages)
            {
                var li = new TagBuilder("li");
                li.AddCssClass(PageItemCss);

                if (page.IsActive)
                {
                    li.AddCssClass(ActiveTagCss);
                }

                if (page.IsDisabled)
                {
                    li.AddCssClass(DisableCss);
                }

                li.InnerHtml.AppendHtml(GenerateLink(page.Title, page.Value.ToString()));
                ul.InnerHtml.AppendHtml(li);
            }

            output.Content.AppendHtml(ul);

            base.Process(context, output);
        }

        private TagBuilder GenerateLink(string linkText, string routeValue)
        {
            var routeValues = new RouteValueDictionary(_routeValues) {{RouteParameter, routeValue}};

            if (RouteParameters != null)
            {
                var values = RouteParameters.GetType().GetProperties();
                if (values.Any())
                {
                    foreach (var propertyInfo in values)
                    {
                        routeValues.Add(propertyInfo.Name, propertyInfo.GetValue(RouteParameters));
                    }
                }
            }

            return Generator.GenerateActionLink(
                viewContext: ViewContext,
                actionName: ActionName,
                controllerName: Controller,
                routeValues: routeValues,
                hostname: Host,
                linkText: linkText,
                fragment: Fragment,
                htmlAttributes: new {@class = PageLinkCss},
                protocol: Protocol);
        }
    }
}