using System.Linq;
using core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProvincesController : Controller {
    BloggingContext _context;
    public ProvincesController (BloggingContext context) {
        _context = context;
    }
    public IActionResult GetList () {
        var provinces = _context.Provinces.Select(x=>new{
            x.Id,
            x.Name,            
            x.Center,            
        }).ToList ();
        return Json(provinces);
    }
}