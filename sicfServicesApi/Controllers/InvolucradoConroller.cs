using Microsoft.AspNetCore.Mvc;

namespace sicfServicesApi.Controllers;

public class InvolucradoConroller : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}