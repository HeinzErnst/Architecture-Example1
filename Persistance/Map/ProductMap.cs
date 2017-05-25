using Domain.Products;
using FluentNHibernate.Mapping;

namespace Persistance.Map
{
    class ProductMap:ClassMap<Product>
    {
        public ProductMap()
        {
            Table("Product");
            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            Map(x => x.Descr);
            References(x => x.Category).Column("category_id");
        }
    }
}
