using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppJobbMintAzExcell
{
    internal class Auto
    {
        public string Nev { get; private set; }
        public long BerlesAra { get; private set; }
        public bool VanBerelve { get; private set; }
        public DateTime KiberlesKezdete { get; private set; }
        public DateTime KiberlesVege { get; private set; }
        public bool VanBiztositas { get; private set; }


        HashSet<string> LehetsegesAutoMarkak = new HashSet<string>{"Opel", "Toyota", "BYD", "Volkswagen", "Tesla",
            "Honda", "BMW", "Hyundai", "Ford", "Mercedes-Benz", "Geely Group", "Kia",
            "Nissan", "Porsche", "Subaru", "General Motors", "GM", "Volvo", "Audi","Mazda",
            "Ferrari", "Suziki"};

        public Auto(string Nev, long BerlesAra, bool VanBerelve, DateTime KiberlesKezdete, DateTime KiberlesVege, bool VanBiztositas)
        {
            this.Nev = Nev;
            this.BerlesAra = BerlesAra;
            this.VanBerelve = VanBerelve;
            this.KiberlesKezdete = KiberlesKezdete;
            this.KiberlesVege = KiberlesVege;
            this.VanBiztositas = VanBiztositas;
        }

        public void NevValtoztatas(string ujNev)
        {
            if (!LehetsegesAutoMarkak.Contains(ujNev))  Nev = ujNev;
            else Console.WriteLine("Ilyen automárka nem létezik!");          
        }
        public void BerlesAranakValtoztatas(string ujAr)
        {
            try
            {
                BerlesAranakValtoztatas(long.Parse(ujAr));
            }   
            catch 
            {
                Console.WriteLine("Nem megfelelo elnevezes!");
            }
        }
        public void BerlesAranakValtoztatas(long ujAr)
        {
            if (ujAr >= 0) BerlesAra = ujAr;
            else Console.WriteLine("Az ára nem lehet kisebb mint 0!");
        }

        public void BiztositasTorlese()
        {
            if (VanBiztositas == true) VanBiztositas = false;
            else Console.WriteLine("Nincs biztosítás amit törölni lehetne!");
        }

        public void BiztositasHozzaadas()
        {
            if (VanBiztositas == false) VanBiztositas = true;
            else Console.WriteLine("Már van rajta biztosítás!");
        }
        public override string ToString()
        {
            return $"jelenleg nincs nev";
        }

    }
}
