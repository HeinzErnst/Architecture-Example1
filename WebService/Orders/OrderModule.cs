using System.Collections.Generic;
using System.Linq;
using Domain.Orders;
using Domain.Products;
using Nancy;
using NHibernate.Linq;
using Persistance.Session;

namespace WebService.Orders
{
    public class OrderModule : NancyModule
    {
        public OrderModule()
        {
            Get["orders"] = parameters =>
            {
                using (var session = new SessionFactoryManager().Instance.OpenSession())
                {
                    var ordersAndLines = session.Query<Order>()
                        .SelectMany(orders => orders.Orderlines.DefaultIfEmpty()
                            .Select(orderline => new { orders.OrderDate, orders.Id, ProductDescr = orderline.Product.Descr })
                        ).ToList();

                    return Response.AsJson(ordersAndLines);
                }
            };

            Get["order/{id:int}"] = parameters =>
            {
                var id = (int)parameters.id;
                using (var session = new SessionFactoryManager().Instance.OpenSession())
                {
                        var ordersAndLines = session.Query<Order>().Where(o=>o.Id==id)
                            .SelectMany(orders => orders.Orderlines.DefaultIfEmpty()
                                .Select(orderline => new { orders.OrderDate, orders.Id, ProductDescr = orderline.Product.Descr })
                            ).ToList();
                        return Response.AsJson(ordersAndLines.FirstOrDefault());
                }
            };
        }
    }
}
