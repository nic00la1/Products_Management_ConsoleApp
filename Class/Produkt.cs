namespace Products_Management_ConsoleApp.Class
{
    internal class Produkt
    {
        public int Id { get; set; }
        public string? Nazwa { get; set; }
        public string? Producent { get; set; }
        public double Cena { get; set; }
        public string? Kategoria { get; set; }
        public int Ilosc { get; set; }
        public DateTime DataDostawy { get; set; }
    }

}
