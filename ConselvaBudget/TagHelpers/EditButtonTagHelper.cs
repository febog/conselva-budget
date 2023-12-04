using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ConselvaBudget.TagHelpers
{
    public class EditButtonTagHelper : AnchorTagHelper
    {
        public EditButtonTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            output.TagName = "a";

            var a = new TagBuilder("a");
            a.Attributes.Add("title", "Edit");
            a.Attributes.Add("aria-label", "Edit");
            a.Attributes.Add("class", "btn btn-secondary btn-sm");
            output.MergeAttributes(a);

            output.Content.SetHtmlContent(@"<i class=""bi bi-pencil-square""></i>");
        }
    }
}
