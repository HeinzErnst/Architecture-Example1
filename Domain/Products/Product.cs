namespace Domain.Products
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Descr{ get; set; }
        public virtual Category Category { get; set; }
        public virtual decimal Price { get; set; }

    }
}
