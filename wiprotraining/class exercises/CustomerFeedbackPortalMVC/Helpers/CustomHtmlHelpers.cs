using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerFeedbackPortalMVC.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlContent CustomInput(this IHtmlHelper htmlHelper, string name, string placeholder)
        {
            return new HtmlString($"<input name='{name}' placeholder='{placeholder}' class='custom-input' />");
        }
    }
}
