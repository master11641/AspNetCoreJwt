using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public  ActionResult Create(int TgoodsId, int Count, int[] AttributeValueIds, int? AddressId, string longitude, string latitude, string FullAddress)
        {
            Address address = new Address();
            List<TgoodsAttributeValue> values = new List<TgoodsAttributeValue>();
            Tgoods currentGoods = _context.Tgoodses.Include(x => x.TgoodsAttributes).FirstOrDefault(x => x.Id == TgoodsId);
            if (currentGoods == null)
            {
                return  StatusCode(StatusCodes.Status404NotFound);
            }
            var attributes = currentGoods.TgoodsAttributes.ToList();
            if (attributes.Any())
            {
                if ((AttributeValueIds == null) || AttributeValueIds.Length != attributes.Count())
                {
                    return  StatusCode(StatusCodes.Status400BadRequest);
                }
                values = attributes.SelectMany(x => x.TgoodsAttributeValues).Where(x => AttributeValueIds.Contains(x.Id)).ToList();
            }
            if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(longitude))
            {
                //var user = _userManager.GetCurrentUser();
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                address.Longitude = longitude;
                address.Latitude = latitude;
                address.MobileDeliver = userId;
                //address.NameDeliver = user.Name + " " + user.Family;
                address.UserId = 1070;
                // address.CityId = 10;
                address.PhoneDeliver = userId;
                // address.Sector = "1";
                address.FullAddress = (string.IsNullOrEmpty(FullAddress)) ? "" : FullAddress;
                // address.ApplicationUser = user;
                 _context.Addresses.Add(address);
                _context.SaveChanges();
            }
            var model = new Torder()
            {
                OrderDate = DateTime.UtcNow.AddHours(3.5),
                Count = Count,
                TgoodsId = TgoodsId,
                UserName = User.Identity.Name,
                TorderStatus = TorderStatus.Registered,
                AddressId = (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(longitude)) ? address.Id : AddressId
                // TgoodsAttributeValues =
            };
            if (values.Any())
            {
                model.TgoodsAttributeValues = values;
            }
            var result = _context.Torders.Add(model);
            _context.SaveChanges();
            //// string SendTokenToOwnerShopResponse = SendNotificationToShopOwner();
           // string tokenOwnerShop =
               // _userManager.FindByUserName(result.Tgoods.Tstore.UserOwner).PushNotificationToken ?? "";
            //bool SendTokenToOwnerShopResponse = await SendNotificationAsync(tokenOwnerShop, "سفارش جدید", "خرید جدیدی ثبت شده است", result.Id.ToString());
            return Json(new
            {
                model.Id,
                model.Count,
                model.OrderDate,
                model.TorderStatus,
                model.UserName,
                GoodsTitle = model.Tgoods.Title,
                GoodsId = model.Tgoods.Id,
                StoreTitle = model.Tgoods.Tstore.Title,
                StoreId = model.Tgoods.Tstore.Id,
                Attributes = model.TgoodsAttributeValues.Select(x => x.TgoodsAttribute.Name + " " + x.Value),
               // SendTokenToOwnerShopResponse,
            });
        }
}