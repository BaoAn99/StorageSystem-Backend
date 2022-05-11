using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StorageSystem.Application.ProductAppService;
using StorageSystem.Application.ProductAppService.Dtos;
using StorageSystem.Models.Catalog.Products;

namespace StorageSystem.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductAppService _productAppService;

        public List<GetProductForView> productList { set; get; }

        public IndexModel(ILogger<IndexModel> logger, IProductAppService productAppService)
        {
            _logger = logger;
            _productAppService = productAppService;
        }

        public IActionResult OnGet()
        {
            productList = _productAppService.GetAll().ToList();
            return Page();
        }
    }
}