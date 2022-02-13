using Basket.Data;
using Basket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basket.Components
{
    public class BasketComponent : ViewComponent
    {
        private MyContext _context;

        public BasketComponent(MyContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ShowBasketViewModel> _list = new List<ShowBasketViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                string currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var order = _context.Orders.SingleOrDefault(o => o.UserId == currentUserId && !o.IsFinally);

                if (order != null)
                {
                    var details = _context.OrderDetails
                        .Where(o => o.OrderId == order.OrderId).ToList();
                    foreach (var item in details)
                    {
                        var product = _context.Products.Find(item.ProductId);

                        _list.Add(new ShowBasketViewModel()
                        {
                            Count = item.Count,
                            ImageName = product.ImageName,
                            Title = product.Title,

                        });
                    }
                }
            }
            return View("/views/shared/_ShowBasket.cshtml", _list);
        }
    }
}
