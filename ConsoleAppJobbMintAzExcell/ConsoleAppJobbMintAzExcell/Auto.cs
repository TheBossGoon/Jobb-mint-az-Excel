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
        public string Marka { get; private set; }
        public long BerlesAra { get; private set; }
        public bool VanBerelve { get; private set; }
        public DateTime KiberlesKezdete { get; private set; }
        public DateTime KiberlesVege { get; private set; }
        public bool VanBiztositas { get; private set; }


        HashSet<string> LehetsegesAutoMarkak = new HashSet<string>{"Opel", "Toyota", "BYD", "Volkswagen", "Tesla",
            "Honda", "BMW", "Hyundai", "Ford", "Mercedes-Benz", "Geely Group", "Kia",
            "Nissan", "Porsche", "Subaru", "General Motors", "GM", "Volvo", "Audi","Mazda",
            "Ferrari", "Suziki"};

        public Auto(string Nev, string Marka, long BerlesAra, bool VanBerelve, DateTime KiberlesKezdete, DateTime KiberlesVege, bool VanBiztositas)
        {
            this.Nev = Nev;
            this.Marka = Marka;
            this.BerlesAra = BerlesAra;
            this.VanBerelve = VanBerelve;
            this.KiberlesKezdete = KiberlesKezdete;
            this.KiberlesVege = KiberlesVege;
            this.VanBiztositas = VanBiztositas;
        }

        public void NevValtoztatas(string ujNev)
        {
            if (!LehetsegesAutoMarkak.Contains(ujNev)) Nev = ujNev;
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
            else Console.WriteLine("Kérem adjon meg egy normális értéket!");
        }
        public void BiztositasTorlese()
        {
            if (VanBiztositas == true)
            {
                VanBiztositas = false;
                Console.WriteLine("Autó biztosításának törlése sikeres!");
            }
            else Console.WriteLine("Nincs biztosítás amit törölni lehetne!");
        }
        public void BiztositasHozzaadas()
        {
            if (VanBiztositas == false) VanBiztositas = true;
            else Console.WriteLine("Már van rajta biztosítás!");
        }
        public void Berles(DateTime BerlesVeg)
        {
            if (!VanBerelve)
            {
                KiberlesKezdete = DateTime.Now;
                KiberlesVege = BerlesVeg;
            }
            else Console.WriteLine("Már bérelve van");
        }
        public void BerlesTorlese()
        {
            if (VanBerelve)
            {
                VanBerelve = false;
                KiberlesKezdete = new DateTime(0);
                KiberlesVege = new DateTime(0);
            }
            else Console.WriteLine("Ez az autó nincs bérelve, azaz nincs mit törölni!");

        }

        public override string ToString()
        {
            StringBuilder tostring = new StringBuilder($"Autó Neve:{this.Nev};\n\t Márka:{this.Marka};\n\t ");
            if (this.BerlesAra != -1) tostring.Append($"Bérlés Ára:{this.BerlesAra};\n\t Van-e Bérelve:Van;\n\t Kiberlés Kezdete:{this.KiberlesKezdete};\n\t Kibérlés Vége:{this.KiberlesVege};\n\t ");
            else tostring.Append("Van-e Bérelve:Nincs\n\t");
            if (this.VanBiztositas) tostring.Append($"Van-e Biztosítás:Van;");
            else tostring.Append($"Van-e Biztosítás:Nincs");
            return tostring.ToString();
        }

    }
}
