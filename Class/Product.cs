
namespace Products_Management_ConsoleApp.Class
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public double Price { get; set; }
        public string? Category { get; set; }
        public int Quantity { get; set; }
        public DateOnly DeliveryDate { get; set; }

        public Product() { }

        public Product(int id, string? name, string? brand, double price, string? category, int quantity, DateOnly deliveryDate)
        {
            Id = id;
            Name = name;
            Brand = brand;
            Price = price;
            Category = category;
            Quantity = quantity;
            DeliveryDate = deliveryDate;
        }
    }
}
