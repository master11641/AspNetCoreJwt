using System.Collections.Generic;
using System.Linq;
using core;
using Core.LinqExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class StoresController : Controller {
    BloggingContext _context;
    List<int> _CategoryStoreIdsTemp = new List<int> ();
    public StoresController (BloggingContext context) {
        _context = context;
    }
    public IActionResult Index () {
        List<Tstore> stores = _context.Tstores.ToList ();
        return Json (stores);
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
    public ActionResult GetStores (int itemsInPage, int PageNumber) {
        if (itemsInPage == 0 || PageNumber == 0) {
            return StatusCode (StatusCodes.Status400BadRequest);
        }
        var lstTotal = _context.Tstores.OrderBy (x => x.Id);
        if (!lstTotal.Any ()) {
            return StatusCode (StatusCodes.Status404NotFound);
        }
        return Json (new {
            hasMore = ((itemsInPage * (PageNumber - 1)) + itemsInPage < lstTotal.Count ()) ? true : false,
                page = PageNumber,
                total = lstTotal.Count (),
                items = lstTotal.Skip (itemsInPage * (PageNumber - 1)).Take (itemsInPage).Select (x => new {
                    x.Id,
                        x.Title,
                        x.ImageUrl,
                        x.Address,
                        x.LogoImageUrl,
                        x.IsFreeDeliveryExist,
                        x.TcategoryStoreId,
                }).ToList ()
        });
    }

    public ActionResult GetStoresByCategoryId (int itemsInPage, int PageNumber, int cityId, int categoryId) {
        if (itemsInPage == 0 || PageNumber == 0) {
            return StatusCode (StatusCodes.Status400BadRequest);
        }
        var catIds = _context.Set<hierarchyDTO>()
            .FromSqlRaw ("Sp_StoreCategoryRecursive @RootId={0}", categoryId).AsNoTracking().Select(x=>x.Item_id)
            .ToList ();
         catIds.Add(categoryId);
        var lstTotal = _context.Tstores.Where (x => x.CityId == cityId).OrderBy (x => x.Id).ToList ().Where (x => catIds.Contains(x.TcategoryStoreId)).ToList ();
        if (!lstTotal.Any ()) {
            return StatusCode (StatusCodes.Status404NotFound);
        }
        return Json (new {
            hasMore = ((itemsInPage * (PageNumber - 1)) + itemsInPage < lstTotal.Count ()) ? true : false,
                page = PageNumber,
                total = lstTotal.Count (),
                items = lstTotal.Skip (itemsInPage * (PageNumber - 1)).Take (itemsInPage).Select (x => new {
                    x.Id,
                        x.Title,
                        x.ImageUrl,
                        x.Address,
                        x.LogoImageUrl,
                        x.IsFreeDeliveryExist,
                        x.TcategoryStoreId,
                }).ToList ()
        });
    }
    // private async Task<List<TcategoryStore>> GetRepliesAsync(List<TcategoryStore> comments)
    // {
    //     foreach (var comment in comments)
    //     {
    //         var replies = await GetFromParentIdAsync(comment.Id);
    //         if (replies != null)
    //         {
    //             comment.Replies = await GetRepliesAsync(replies.ToList());
    //         }
    //     }

    //     return comments;
    // }
    // public List<int> GetNestedChildCategorie (int parentId) {
    //     _CategoryStoreIdsTemp.Add (parentId);
    //     var result = _context.TcategoryStores.Where (x => x.Id == parentId).SelectMany (x => x.Children);

    // }
}