using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppJobbMintAzExcell
{
    internal class Auto
    {
        public string Nev { get; }
        public long BerlesAra { get; }
        public bool VanBerelve { get; }
        public DateTime KiberlesKezdete { get; }
        public DateTime KiberlesVege { get; }
        public bool VanBiztositas { get; }


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

        public override string ToString()
        {
            return $"jelenleg nincs nev";
        }
    }
}
