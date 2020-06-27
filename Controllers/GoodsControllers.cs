using System.Linq;
using core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class GoodsController : Controller
{
        BloggingContext _context;
     public GoodsController (BloggingContext context) {
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
    public ActionResult GetGoodsByStoreId(int itemsInPage, int pageNumber, int storeId)
        {
            if (itemsInPage == 0 || pageNumber == 0 || storeId == 0)
            {
                return  StatusCode(StatusCodes.Status400BadRequest);
            }
            var lstTotal = _context.Tstores.Where(x => x.Id == storeId).SelectMany(x => x.Tgoodses)
                .Include(x => x.TgoodsPrices).Include(x => x.TgoodsImages).Include(x => x.TgoodsAttributes).OrderByDescending(x => x.Id);
            if (!lstTotal.Any())
            {
                 return StatusCode(StatusCodes.Status404NotFound);
            }
            return Json(new
            {
                hasMore = ((itemsInPage * (pageNumber - 1)) + itemsInPage < lstTotal.Count()) ? true : false,
                page = pageNumber,
                total = lstTotal.Count(),
                items = lstTotal.Skip(itemsInPage * (pageNumber - 1)).Take(itemsInPage).Select(x => new
                {
                    x.Id,
                    StoreTitle = x.Tstore.Title,
                    x.Description,
                    x.GoodsStatus,
                    x.IsConfirm,
                    x.Abstract,
                    x.GoodsType,
                    x.Title,
                    x.IsFreeDeliveryExist,
                    Attributes = x.TgoodsAttributes.Select(y => new
                    {
                        y.Name,
                        y.TattributeType,
                        y.Required,
                        AttributeId = y.Id,
                        AttributeValues = y.TgoodsAttributeValues.Select(z => new { z.Value, ValueId = z.Id, z.Caption })
                    }),
                    TgoodsImages = x.TgoodsImages.Select(y => new { y.ImageUrl }),
                    Prices = x.TgoodsPrices.Select(y => new { y.Price, y.Date })
                }).Where(x => x.IsConfirm == true).ToList().Select(x => new
                {
                    x.Id,
                    x.StoreTitle,
                    x.Title,
                    x.Description,
                    x.GoodsStatus,
                    x.GoodsType,
                    x.Abstract,
                    x.TgoodsImages,
                    x.IsFreeDeliveryExist,
                    x.Prices,
                    x.Attributes,
                    Price = (x.Prices.Any()) ? x.Prices.OrderBy(y => y.Date).Last().Price : 0
                })
            });
        }
}