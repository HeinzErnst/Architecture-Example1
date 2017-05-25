using Domain.Orders;
using FluentNHibernate.Mapping;

namespace Persistance.Map
{
    public class OrderMap:ClassMap<Order>
    {
        public OrderMap()
        {
            Table("Orders");
            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            Map(x => x.OrderDate);
            HasMany(x => x.Orderlines).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
