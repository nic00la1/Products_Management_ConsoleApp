using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
