using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppJobbMintAzExcell
{
    internal class AutokolcsonzoRendszer
    {
        List<Auto> autok;

        public List<Auto> Autok { get => new List<Auto>(autok); }

        HashSet<string> PartnerAutoMarkakListaja = new HashSet<string>{"Opel", "Toyota", "BYD", "Volkswagen", "Tesla",
            "Honda", "BMW", "Hyundai", "Ford", "Mercedes-Benz", "Geely Group", "Kia",
            "Nissan", "Porsche", "Subaru", "General Motors", "GM", "Volvo", "Audi","Mazda",
            "Ferrari", "Suziki"};

        public AutokolcsonzoRendszer()
        {
            this.autok = new List<Auto>();
        }

        public void FelVetel(string neve, string marka, long berlesAra, bool vanBerelve, DateTime kiberlesKezdete, DateTime kiberlesVege, bool vanBiztositas)
        {

            Auto auto = new Auto(neve, marka, berlesAra, vanBerelve, kiberlesKezdete, kiberlesVege, vanBiztositas);
            if (PartnerAutoMarkakListaja.Contains(marka))
            {
                autok.Add(auto);
            }
            else
            {
                Console.WriteLine("Nincs ilyen nevű kocsi a partner márkák listájában.");
            }

        }

        public void AutokBeolvasasa(string beolvasandoFajl)
        {
            try
            {
                using (StreamReader sr = new StreamReader(beolvasandoFajl))
                {
                    string header = sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        string[] sor = sr.ReadLine().Split(';');
                        string[] tarto = new string[6];

                        for (int i = 0; i < 8; i++)
                        {
                            string[] belsoSor = sor[i].Split(':');
                            tarto[i] = belsoSor[i];
                        }
                        FelVetel(tarto[0], tarto[1], long.Parse(tarto[2]), Convert.ToBoolean(tarto[3]), Convert.ToDateTime(tarto[4]), Convert.ToDateTime(tarto[5]), Convert.ToBoolean(tarto[6]));
                    }
                }
                Console.WriteLine("Sikeres volt a fájl beolvasás.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Nem létező fájl nevet adott meg, próbálja meg újra!");
            }
        }

        public void AutoTorlese(int bekertIndex)
        {
            if (bekertIndex < autok.Count && bekertIndex >= 0)
            {
                autok.RemoveAt(bekertIndex);
            }
            else
            {
                Console.WriteLine("Az autó nincs benne a listában!");
            }
        }

        public void AutokListajanakKiirasaKulonFajlba(string kiirtFajlNeve)
        {
            using (StreamWriter sw = new StreamWriter(kiirtFajlNeve))
            {
                foreach (Auto auto in autok)
                {
                    sw.WriteLine($"{auto.Nev.Trim()};{auto.Marka};{auto.BerlesAra};{auto.VanBerelve};{auto.KiberlesKezdete};{auto.KiberlesVege};{auto.VanBiztositas};");
                }
            }
        }

        public Auto AutoKereseseNevSzerint(string eztKeresd)
        {
            for (int i = 0; i < autok.Count; i++)
            {
                if (autok[i].Nev == eztKeresd)
                {
                    return autok[i];
                }
            }
            return null;
        }

        public void AutokKiirasa()
        {
            for (int i = 0; i < autok.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] - {autok[i].ToString()}");
            }
        }

    }
}
