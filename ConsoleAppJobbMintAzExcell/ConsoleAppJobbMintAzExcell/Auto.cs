using System;
using System.Text;

namespace ConsoleAppJobbMintAzExcell
{
    internal class Auto
    {
        public string Rendszam { get; private set; }
        public string Marka { get; private set; }
        public long BerlesAra { get; private set; }
        public bool VanBerelve { get; private set; }
        public DateTime KiberlesKezdete { get; private set; }
        public DateTime KiberlesVege { get; private set; }
        public bool VanBiztositas { get; private set; }


        public Auto(string rendszam, string marka, long berlesAra, bool vanBerelve, DateTime kiberlesKezdete, DateTime kiberlesVege, bool vanBiztositas)
        {
            Rendszam = rendszam;
            Marka = marka;
            BerlesAra = berlesAra;
            VanBerelve = vanBerelve;
            KiberlesKezdete = kiberlesKezdete;
            KiberlesVege = kiberlesVege;
            VanBiztositas = vanBiztositas;
        }

        public void RendszamValtoztatas(string ujRendszam)
        {
            if (string.IsNullOrWhiteSpace(ujRendszam) || ujRendszam.Contains(";") || ujRendszam.Contains(":"))
            {
                Console.WriteLine("Érvénytelen rendszám.");
                return;
            }

            Rendszam = ujRendszam.Trim();
        }
        public void BerlesAranakValtoztatas(string ujAr)
        {
            try
            {
                BerlesAranakValtoztatas(long.Parse(ujAr));
            }
            catch (FormatException)
            {
                Console.WriteLine("Nem megfelelő árat adott meg!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Nem megfelelő árat adott meg!");
            }
        }
        public void BerlesAranakValtoztatas(long ujAr)
        {
            if (ujAr >= 0)
            {
                BerlesAra = ujAr;
                Console.WriteLine("Bér megváltoztatása sikeres!");
            }
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
            if (VanBiztositas == false)
            {
                VanBiztositas = true;
                Console.WriteLine("Autó biztosítása sikeres!");
            }
            else Console.WriteLine("Már van rajta biztosítás!");
        }
        public void Berles(DateTime berlesVege)
        {
            if (!VanBerelve)
            {
                if (berlesVege.Date < DateTime.Now.Date.AddDays(1))
                {
                    Console.WriteLine("A bérlés vége legalább 1 nappal későbbi legyen.");
                    return;
                }

                VanBerelve = true;  
                KiberlesKezdete = DateTime.Now;
                KiberlesVege = berlesVege;
            }
            else Console.WriteLine("Már bérelve van");
        }
        public void BerlesTorlese()
        {
            if (VanBerelve)
            {
                VanBerelve = false;
                KiberlesKezdete = DateTime.Now;
                KiberlesVege = DateTime.MinValue;
            }
            else Console.WriteLine("Ez az autó nincs bérelve, azaz nincs mit törölni!");

        }

        public override string ToString()
        {
            StringBuilder tostring = new StringBuilder($"Autó rendszáma:{Rendszam};\n\t Márka:{Marka};\n\t ");
            if (BerlesAra != -1) tostring.Append($"Bérlés Ára:{BerlesAra};\n\t ");
            if (VanBerelve) tostring.Append($"Van-e Bérelve:Van;\n\t Bérlés Kezdete:{KiberlesKezdete};\n\t Bérlés Vége:{KiberlesVege};\n\t ");
            else tostring.Append("Van-e Bérelve:Nincs\n\t ");
            if (VanBiztositas) tostring.Append($"Van-e Biztosítás:Van;");
            else tostring.Append($"Van-e Biztosítás:Nincs;");
            return tostring.ToString();
        }

    }
}
