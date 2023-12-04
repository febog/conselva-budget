using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Build.Framework;

namespace ConselvaBudget.TagHelpers
{
    public class TableButtonsTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            // These arranges the buttons side by side using Bootstrap's flexbox utilities.
            output.Attributes.Add("class", "d-flex gap-1 justify-content-center");
        }
    }
}
