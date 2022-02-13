using Basket.Data;
using Basket.Models;
using Basket.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Basket.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private MyContext _context;

        public BasketController(MyContext context)
        {
            _context = context;
        }

        public IActionResult AddToCart(int id)
        {
            string CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order order = _context.Orders.SingleOrDefault(o => o.UserId == CurrentUserId && !o.IsFinally);

            if (order == null)
            {
                order = new Order();
                order.UserId = CurrentUserId;
                order.CreateDate = DateTime.Now;
                order.IsFinally = false;
                order.Sum = 0;
                _context.Orders.Add(order);
                _context.OrderDetails.Add(new OrderDetails()
                {
                    Count = 1,
                    OrderId = order.OrderId,
                    Price = _context.Products.Find(id).Price,
                    ProductId = id,
                });
                _context.SaveChanges();
            }
            else
            {
                var details =
                    _context.OrderDetails.SingleOrDefault(o => o.OrderId == order.OrderId && o.ProductId == id);
                if (details == null)
                {
                    _context.OrderDetails.Add(new OrderDetails()
                    {
                        Count = 1,
                        OrderId = order.OrderId,
                        Price = _context.Products.Find(id).Price,
                        ProductId = id,
                    });

                }
                else
                {
                    details.Count += 1;
                    _context.Update(details);
                }

                _context.SaveChanges();
            }

            UpdateSum(order.OrderId);
            return Redirect("/");
        }


        public IActionResult ShowOrder()
        {
            string currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = _context.Orders.SingleOrDefault(o => o.UserId == currentUserId && !o.IsFinally);
            List<ShowListOrderViewModel> list = new List<ShowListOrderViewModel>();

            if (order != null)
            {
                var details = _context.OrderDetails
                    .Where(o => o.OrderId == order.OrderId).ToList();
                foreach (var item in details)
                {
                    var product = _context.Products.Find(item.ProductId);
                    list.Add(new ShowListOrderViewModel()
                    {
                        Count = item.Count,
                        ImageName = product.ImageName,
                        OrderDetailsId = item.OrderDetailsId,
                        Price = item.Price,
                        Sum = item.Price * item.Count,
                        Title = product.Title,
                    });

                }
            }
            return View(list);
        }

        public IActionResult Delete(int id)
        {
            var orderDetalseId = _context.OrderDetails.Find(id);
            _context.Remove(orderDetalseId);
            _context.SaveChanges();
            return RedirectToAction("ShowOrder");
        }

        public IActionResult Command(int id, string command)
        {
            var orderDetalseId = _context.OrderDetails.Find(id);
            switch (command)
            {
                case "up":
                    {
                        orderDetalseId.Count += 1;
                        _context.Update(orderDetalseId);
                        break;
                    }
                case "down":
                    {
                        orderDetalseId.Count -= 1;
                        if (orderDetalseId.Count == 0)
                        {
                            _context.OrderDetails.Remove(orderDetalseId);
                        }
                        else
                        {
                            _context.Update(orderDetalseId);
                        }
                        break;
                    }
            }
            _context.SaveChanges();
            return RedirectToAction("ShowOrder");
        }
        public void UpdateSum(int orderid)
        {
            var order = _context.Orders.Find(orderid);
            order.Sum = _context.OrderDetails.Where(o => o.OrderId == orderid)
                .Select(d => d.Count * d.Price).Sum();
            _context.Update(order);
            _context.SaveChanges();
        }
    }

}
