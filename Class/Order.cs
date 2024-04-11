namespace Products_Management_ConsoleApp.Class
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }
        public string? CustomerAddress { get; set; }
        public int DeliveryOption { get; set; }
        public int PaymentOption { get; set; }
        public double TotalPrice { get; set; }
        public DateOnly OrderDate { get; set; }

        public Order(int id, List<Product> products, string? customerName, string? customerSurname, string? customerAddress, int deliveryOption, int paymentOption, double totalPrice, DateOnly orderDate)
        {
            Id = id;
            Products = products ?? new List<Product>();
            CustomerName = customerName;
            CustomerSurname = customerSurname;
            CustomerAddress = customerAddress;
            DeliveryOption = deliveryOption;
            PaymentOption = paymentOption;
            TotalPrice = totalPrice;
            OrderDate = orderDate;
        }

        public Order(int v)
        {
            Products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public double CalculateTotalCost()
        {
            double totalCost = 0;

            switch (DeliveryOption)
            {
                case 1: // odbior osobisty
                    totalCost += 0;
                    break;
                case 2: // paczkomat
                    totalCost += 10;
                    break;
                case 3: // kurier
                    totalCost += 20;
                    break;
            }

            switch (PaymentOption)
            {
                case 1: // karta
                    totalCost += 2;
                    break;
                case 2: // gotowka
                    totalCost += 0;
                    break;
            }

            foreach (var product in Products) // Dla każdego produktu w zamówieniu dodajemy jego cenę do całkowitej ceny zamówienia
            {
                totalCost += product.Price;
            }

            totalCost = Math.Round(totalCost, 2); // Zaokrąglamy ostateczną cenę do dwóch miejsc po przecinku

            return totalCost;
        }
    }
}
