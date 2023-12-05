using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ConselvaBudget.TagHelpers
{
    public class DetailsButtonTagHelper : AnchorTagHelper
    {
        public DetailsButtonTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            output.TagName = "a";

            var a = new TagBuilder("a");
            a.Attributes.Add("title", "Details");
            a.Attributes.Add("aria-label", "Details");
            a.Attributes.Add("class", "btn btn-success btn-sm");
            output.MergeAttributes(a);

            output.Content.SetHtmlContent(@"<i class=""bi bi-box-arrow-up-right""></i>");
        }
    }
}
