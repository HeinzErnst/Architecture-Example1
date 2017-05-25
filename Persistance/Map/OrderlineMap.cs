using Domain.Orders;
using FluentNHibernate.Mapping;

namespace Persistance.Map
{
    class OrderlineMap: ClassMap<Orderline>
    {
        public OrderlineMap()
        {
            Table("Orderline");
            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            References(x => x.Product);
            References(x => x.Order);
            Map(x => x.Quantity);
        }
    }
}
