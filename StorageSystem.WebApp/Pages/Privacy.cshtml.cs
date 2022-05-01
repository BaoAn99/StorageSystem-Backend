using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StorageSystem.WebApp.Views.Shared.Components;

namespace StorageSystem.WebApp.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return ViewComponent("MessagePage", new MessagePage.Message
            {
                title = "Thông báo quan trọng",
                htmlcontent = "Đây là <strong>Nội dung HTML</strong>",
                secondwait = 5,
                urlredirect = "/"
            });
        }
    }
}