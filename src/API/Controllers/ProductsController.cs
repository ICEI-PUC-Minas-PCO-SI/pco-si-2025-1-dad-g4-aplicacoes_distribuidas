using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
