using System.Collections.Generic;
using System.Linq;
using core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class OrdersController : Controller
{
    BloggingContext _context;
     public OrdersController (BloggingContext context) {
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }
    public IActionResult Error()
    {
        return View();
    }
}