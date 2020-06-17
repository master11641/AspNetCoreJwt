using System.Collections.Generic;
using System.Linq;
using core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class StoresController : Controller {
    BloggingContext _context;

    public StoresController (BloggingContext context) {
        _context = context;
    }
    public IActionResult Index () {
        List<Tstore> stores = _context.Tstores.ToList();
        return Json(stores);
    }
    public IActionResult About () {
        return View ();
    }
    public IActionResult Contact () {
        return View ();
    }
    public IActionResult Error () {
        return View ();
    }
    public ActionResult GetStores(int itemsInPage, int PageNumber)
        {
            if (itemsInPage == 0 || PageNumber == 0)
            {
                return  StatusCode(StatusCodes.Status400BadRequest);
            }
            var lstTotal = _context.Tstores.OrderBy(x => x.Id);
            if (!lstTotal.Any())
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Json(new
            {
                hasMore = ((itemsInPage * (PageNumber - 1)) + itemsInPage < lstTotal.Count()) ? true : false,
                page = PageNumber,
                total = lstTotal.Count(),
                items = lstTotal.Skip(itemsInPage * (PageNumber - 1)).Take(itemsInPage).Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.ImageUrl,
                    x.Address,
                    x.IsFreeDeliveryExist,
                    x.TcategoryStoreId,
                }).ToList()
            });
        }
}