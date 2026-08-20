using Microsoft.AspNetCore.Mvc;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
