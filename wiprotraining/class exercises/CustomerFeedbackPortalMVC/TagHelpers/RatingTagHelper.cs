using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CustomerFeedbackPortalMVC.TagHelpers
{
    [HtmlTargetElement("rating-stars")]
    public class RatingTagHelper : TagHelper
    {
        public int Stars { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div"; // the final HTML element will be <div>
            output.Attributes.SetAttribute("class", "rating-stars"); // add a CSS class
            string starsHtml = "";
            for(int i = 0; i < Stars; i++)
            {
                starsHtml += "★"; // add a star for each rating
            }
            output.Content.SetHtmlContent(starsHtml);
        }
    }
}
