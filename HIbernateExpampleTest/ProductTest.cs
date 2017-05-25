using System;
using System.Linq;
using Domain.Orders;
using Domain.Products;
using HIbernateExpampleTest.App_Start;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Linq;
using Persistance.Session;

namespace HIbernateExpampleTest
{
    [TestClass]
    public class ProductTest
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            NHibernateProfilerBootstrapper.PreStart();
        }

        [TestMethod]
        public void CanCreateProduct()
        {
            using (var session = new SessionFactoryManager().Instance.OpenSession())
            {
                try
                {
                    session.Transaction.Begin();
                    var category = CreateCategory(session, "Fluids");
                    var product = new Product { Descr = "TestProduct 1", Category = category };
                    session.SaveOrUpdate(product);

                    var category2 = CreateCategory(session, "Powders");
                    var product2 = new Product { Descr = "TestProduct 2", Category = category2 };
                    session.SaveOrUpdate(product2);

                    var product3 = new Product { Descr = "TestProduct 3"};
                    session.SaveOrUpdate(product3);

                    session.Transaction.Commit();
                    Assert.IsTrue(true);
                }
                catch (Exception ex)
                {
                    session.Transaction.Rollback();
                    Assert.IsFalse(true, ex.ToString());
                }
            }
        }

        [TestMethod]
        public void CanCreateOrderline()
        {
            CanCreateProduct();
            using (var session = new SessionFactoryManager().Instance.OpenSession())
            {
                try
                {
                    session.Transaction.Begin();
                    var order = new Order {OrderDate = DateTime.Now};
                    var orderline = new Orderline{ Product= session.Get<Product>(1), Quantity= 12, Order = order}; // It is important to assign order!

                    order.Orderlines.Add(orderline);
                    session.SaveOrUpdate(order);

                    var category2 = CreateCategory(session, "Powders");
                    var product2 = new Product { Descr = "TestProduct 2", Category = category2 };
                    session.SaveOrUpdate(product2);

                    var product3 = new Product { Descr = "TestProduct 3" };
                    session.SaveOrUpdate(product3);

                    session.Transaction.Commit();
                    Assert.IsTrue(true);
                }
                catch (Exception ex)
                {
                    session.Transaction.Rollback();
                    Assert.IsFalse(true, ex.ToString());
                }
            }
        }

        [TestMethod]
        public void CanLeftOuterJoin()
        {
            //http://www.ienablemuch.com/2013/05/limited-defaultifempty-is-supported-on.html
            CanCreateOrderline();
            using (var session = new SessionFactoryManager().Instance.OpenSession())
            {
                try
                {
                    session.Transaction.Begin();
                    var order = new Order {OrderDate = DateTime.Now};
                    session.SaveOrUpdate(order);

                    // Lamda
                    var ordersAndLines = session.Query<Order>()
                        .SelectMany(orders => orders.Orderlines.DefaultIfEmpty()
                            .Select(orderline => new {orders.OrderDate, orders.Id, ProductDescr = orderline.Product.Descr})
                        ).ToList();

                    var ordersAndLines1 = (from order1 in session.Query<Order>()
                            from orderline in order1.Orderlines.DefaultIfEmpty()
                            where (orderline.Product== null ||orderline.Product.Category.Id==1)
                            select new { order1.OrderDate, order1.Id,orderline.Product.Descr}).ToList();

                    session.Transaction.Commit();
                    Assert.IsTrue(ordersAndLines1.Count==2);
                }
                catch (Exception ex)
                {
                    session.Transaction.Rollback();
                    Assert.IsFalse(true, ex.ToString());
                }
            }
        }

        [TestMethod]
        public void CanInnerJoin()
        {
            using (var session = new SessionFactoryManager().Instance.OpenSession())
            {
                try
                {
                    session.Transaction.Begin();
                    var products = session.Query<Product>().FirstOrDefault(p => p.Category.Descr == "Fluids");

                    //var query = from t in Product
                    //        join l in session.Query<NameLinkToFiler>() on t.Filing.Filer.Id equals l.Filer.Id
                    //        join n in Session.Query<SearchName>() on l.SearchName.Id equals n.Id
                    //        where sn.Contains(request.FilerName)
                    //        select t;
                    Assert.IsTrue(true);
                }

                catch (Exception ex)
                {
                    session.Transaction.Rollback();
                    Assert.IsFalse(true, ex.ToString());
                }
            }
        }

        private static Category CreateCategory(ISession session, string descr)
        {
            var category = new Category {Descr = descr};
            session.SaveOrUpdate(category);
            return category;
        }
    }
}
