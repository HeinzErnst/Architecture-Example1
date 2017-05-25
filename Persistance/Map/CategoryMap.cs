using Domain.Products;
using FluentNHibernate.Mapping;

namespace Persistance.Map
{
    class CategoryMap:ClassMap<Category>
    {
        public CategoryMap()
        {
            Table("Category");
            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            Map(x => x.Descr);
            //References(x => x.Category).Column("category_id");
        }
    }
}
