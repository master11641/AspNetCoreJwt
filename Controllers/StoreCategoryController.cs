using System.Linq;
using core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class StoreCategoryController : Controller {
    BloggingContext _context;
    public StoreCategoryController (BloggingContext context) {
        _context = context;
    }
    public IActionResult GetStoreFirstPage(){
        var catIds = _context.Set<hierarchyDTO>()
            .FromSqlRaw ("Sp_GetStoreIdForFirstPage").AsNoTracking().Select(x=>x.Item_id)
            .ToList ();
        var storesIds = _context.TcategoryStores.ToList().Where(x=>catIds.Contains(x.Id) ).Select(x=>new{
            x.Id,
            x.Title
        }).ToList();
        return Json(storesIds);
    }
}