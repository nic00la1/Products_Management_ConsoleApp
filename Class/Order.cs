using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products_Management_ConsoleApp.Class
{
    public class Order : Product
    {
        public List<Product>? Products { get; set; }
        public string? ClientName { get; set; }
        public string? ClientSurname { get; set; }
        public string? ClientAdress { get; set; }
        public DeliveryMethod Client_DeliveryMethod { get; set; }
        public PaymentMethod Client_PaymentMethod { get; set; }
        public enum DeliveryMethod
        {
            PersonalPickup,
            Courier
        }

        public enum PaymentMethod
        {
            Card,
            Cash
        }

        // Add property for DeliveryCost
        public decimal DeliveryCost
        {
            get
            {
                if (Client_DeliveryMethod == DeliveryMethod.PersonalPickup)
                {
                    return 0m;
                }
                else if (Client_DeliveryMethod == DeliveryMethod.Courier)
                {
                    return 20m;
                }
                else
                {
                    throw new InvalidOperationException("Błędny sposób dostawy.");
                }
            }
        }

        // Add property for PaymentCost
        public decimal PaymentCost
        {
            get
            {
                if (Client_PaymentMethod == PaymentMethod.Card)
                {
                    return 2m;
                }
                else if (Client_PaymentMethod == PaymentMethod.Cash)
                {
                    return 0m;
                }
                else
                {
                    throw new InvalidOperationException("Błędny sposób płatności.");
                }
            }
        }

        // Add property for TotalAmount
        public decimal TotalAmount
        {
            get
            {
                decimal total = 0m;
                if (Products != null)
                {
                    foreach (var product in Products)
                    {
                        total = decimal.Add(total, decimal.Multiply((decimal)product.Price, product.Quantity));
                    }
                }
                return decimal.Add(decimal.Add(total, DeliveryCost), PaymentCost);
            }
        }
    }
}
