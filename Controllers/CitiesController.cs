using System.Linq;
using core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CitiesController : Controller {
    BloggingContext _context;
    public CitiesController (BloggingContext context) {
        _context = context;
    }
    public IActionResult GetListByProvinceId (int id) {
        var cities = _context.Cities.Select(x=>new{
            x.Id,
            x.Name,
            x.ProvinceId,            
        }).Where(x=>x.ProvinceId==id);
        return Json(cities);
    }
}