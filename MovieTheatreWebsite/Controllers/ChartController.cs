using System.Data;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

public class ChartPage : Controller
{
    // GET: Home
    public IActionResult Index()
    {
        return View();
    }


}