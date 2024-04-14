namespace Products_Management_ConsoleApp.Class
{
    internal class Zamowienie
    {
        public int Id { get; set; }
        public List<Produkt> Produkty { get; set; }
        public Klient DaneKupujacego { get; set; }
        public string SposobDostawy { get; set; }
        public string SposobPlatnosci { get; set; }
        public double KwotaCalkowita { get; set; }

        public Zamowienie(int id, List<Produkt> produkty, Klient daneKupujacego, string sposobDostawy, string sposobPlatnosci, double kwotaCalkowita)
        {
            Id = id;
            Produkty = produkty ?? new List<Produkt>();
            DaneKupujacego = daneKupujacego;
            SposobDostawy = sposobDostawy;
            SposobPlatnosci = sposobPlatnosci;
            KwotaCalkowita = kwotaCalkowita;
        }

        public void DodajProdukt(Produkt produkt)
        {
            Produkty.Add(produkt);
        }

        public void Oblicz_Kwote_Calkowita()
        {
            double cena = 0;
            if (SposobDostawy == "kurier")
                cena += 20;

            if (SposobPlatnosci == "karta")
                cena += 2;

            foreach (Produkt p in Produkty)
            {
                cena += p.Cena;
            }
            KwotaCalkowita = Math.Round(cena, 2);
            Console.WriteLine($"\nKwota całkowita zamówienia: {KwotaCalkowita}zł");
        }
    }

    internal class Klient
    {
        public string? Imie { get; set; }
        public string? Nazwisko { get; set; }
        public string? Adres { get; set; }
    }




}
