using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace ConselvaBudget.TagHelpers
{
    public class CancelButtonTagHelper : AnchorTagHelper
    {
        private readonly IStringLocalizer<Resources.SharedResource> _localizer;

        public CancelButtonTagHelper(IHtmlGenerator generator,
                                     IStringLocalizer<Resources.SharedResource> localizer) : base(generator)
        {
            _localizer = localizer;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            var label = _localizer["BUTTON_CANCEL"];

            output.TagName = "a";

            var a = new TagBuilder("a");
            a.Attributes.Add("class", "btn btn-secondary");
            output.MergeAttributes(a);

            output.Content.SetHtmlContent(label);
        }
    }
}
