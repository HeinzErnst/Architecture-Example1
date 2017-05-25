using System;
using System.Collections.Generic;

namespace Domain.Orders
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual IList<Orderline> Orderlines { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public Order()
        {
            Orderlines= new List<Orderline>();
        }
    }
}
