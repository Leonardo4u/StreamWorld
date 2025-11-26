using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamWorld.Controllers.Admin;

[Authorize(Policy = "AdminOnly")]
public class AdminHomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
