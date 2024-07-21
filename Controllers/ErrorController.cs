using Microsoft.AspNetCore.Mvc;
namespace test1.Controllers;

[Route("Error")]
public class ErrorController : Controller
{
    [Route("page/was/not/found")]
    public IActionResult NotFoundPage()
    {
        Console.WriteLine("Hello error");
        return View();
    }
}