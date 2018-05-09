using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DNSPromotionManager.App_Code
{
    static public class LinkHelper
    {
        static public Link BeginLink(
            this IHtmlHelper html,
            string controllerName,
            string actionName,
            object routeValues,
            object htmlAttributes)
        {
            IUrlHelperFactory urlHelperFactory = new UrlHelperFactory();

            string action;
            if (actionName == null && controllerName == null && routeValues == null)
            {
                var request = html.ViewContext.HttpContext.Request;
                action = request.PathBase + request.Path + request.QueryString;
            }
            else
            {

                var urlHelper = urlHelperFactory.GetUrlHelper(html.ViewContext);
                action = urlHelper.Action(action: actionName, controller: controllerName, values: routeValues);
            }

            var writer = html.ViewContext.Writer;

            TagBuilder linkCore = GenerateLinkCore(action, htmlAttributes);
            if (linkCore != null)
            {
                linkCore.TagRenderMode = TagRenderMode.StartTag;
                writer.WriteLine(linkCore);
            }

            return new Link(writer);
        }

        static private TagBuilder GenerateLinkCore(string action, object htmlAttributes)
        {
            var tagBuilder = new TagBuilder("a");

            tagBuilder.MergeAttributes(GetHtmlAttributeDictionaryOrNull(htmlAttributes));
            tagBuilder.MergeAttribute("href", action);

            return tagBuilder;
        }

        public static IDictionary<string, object> GetHtmlAttributeDictionaryOrNull(object htmlAttributes)
        {
            IDictionary<string, object> htmlAttributeDictionary = null;
            if (htmlAttributes != null)
            {
                htmlAttributeDictionary = htmlAttributes as IDictionary<string, object>;
                if (htmlAttributeDictionary == null)
                {
                    htmlAttributeDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                }
            }

            return htmlAttributeDictionary;
        }

        public class Link : IDisposable
        {
            private readonly TextWriter _writer;

            public Link(TextWriter writer)
            {
                _writer = writer;
            }

            public void Dispose()
            {
                _writer.Write("</a>");
            }
        }
    }
}
