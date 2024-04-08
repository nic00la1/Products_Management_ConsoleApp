namespace Products_Management_ConsoleApp.Class
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }
        public string? CustomerAddress { get; set; }
        public string? DeliveryOption { get; set; }
        public string? PaymentOption { get; set; }
        public double TotalPrice { get; set; }
        public DateOnly OrderDate { get; set; }

        public Order(int id, List<Product> products, string? customerName, string? customerSurname, string? customerAddress, string? deliveryOption, string? paymentOption, double totalPrice, DateOnly orderDate)
        {
            Id = id;
            Products = products;
            CustomerName = customerName;
            CustomerSurname = customerSurname;
            CustomerAddress = customerAddress;
            DeliveryOption = deliveryOption;
            PaymentOption = paymentOption;
            TotalPrice = totalPrice;
            OrderDate = orderDate;
        }


        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void CalculateTotalCost()
        {
            double totalCost = 0;

            if (DeliveryOption == "odbior osobisty") totalCost += 0;
            if (DeliveryOption == "paczkomat") totalCost += 10;
            if (DeliveryOption == "kurier") totalCost += 20;

            if (PaymentOption == "karta") totalCost += 2;
            if (PaymentOption == "gotowka") totalCost += 0;

            foreach (var product in Products) // Dla każdego produktu w zamówieniu dodajemy jego cenę do całkowitej ceny zamówienia
            {
                totalCost += product.Price;
            }

            totalCost = Math.Round(totalCost, 2); // Zaokrąglamy ostateczną cenę do dwóch miejsc po przecinku
        }
    }
}
