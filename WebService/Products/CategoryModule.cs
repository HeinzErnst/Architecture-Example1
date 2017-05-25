using System.Collections.Generic;
using System.Linq;
using Domain.Products;
using Nancy;
using NHibernate.Linq;
using Persistance.Session;

namespace WebService.Products
{
    public class CategoryModule : NancyModule
    {
        public CategoryModule()
        {
            Get["categories"] = parameters =>
            {
                using (var session = new SessionFactoryManager().Instance.OpenSession())
                {
                    var categories = session.Query<Category>().ToList();
                    return Response.AsJson(categories);
                }
            };

            Get["category/{id:int}"] = parameters =>
            {
                var id = (int)parameters.id;
                using (var session = new SessionFactoryManager().Instance.OpenSession())
                {
                    var category = session.Get<Category>(id);
                    return Response.AsJson(category);
                }
            };
        }
    }
}
