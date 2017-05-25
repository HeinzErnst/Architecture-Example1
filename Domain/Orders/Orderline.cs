using Domain.Products;

namespace Domain.Orders
{
    public class Orderline
    {
        public virtual int Id { get; set; }
        public virtual  Product Product { get; set; }
        public virtual Order Order { get; set; }
        public virtual int Quantity { get; set; }
    }
}