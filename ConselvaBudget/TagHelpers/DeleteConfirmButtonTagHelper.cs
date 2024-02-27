using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace ConselvaBudget.TagHelpers
{
    public class DeleteConfirmButtonTagHelper : TagHelper
    {
        private readonly IStringLocalizer<Resources.SharedResource> _localizer;

        public DeleteConfirmButtonTagHelper(IStringLocalizer<Resources.SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.Attributes.Add("class", "btn btn-danger");
            output.Attributes.Add("type", "submit");
            output.Attributes.Add("value", _localizer["BUTTON_DELETE"]);
        }
    }
}
