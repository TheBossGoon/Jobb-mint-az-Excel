using System;
using System.Collections.Generic;
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
            if (berlesAra > 0 && PartnerAutoMarkakListaja.Contains(neve))
            {
                autok.Add(auto);
            }
            else
            {
                Console.WriteLine("Nulla vagy alacsonyabb a bérlési ára vagy pedig nincs ilyen nevű kocsi a partner márkák listájában.");
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
                        FelVetel(sor[0], sor[1], long.Parse(sor[2]), Convert.ToBoolean(sor[3]), Convert.ToDateTime(sor[4]), Convert.ToDateTime(sor[5]), Convert.ToBoolean(sor[6]));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nem létező fájl nevet adott meg próbálja meg újra!");
            }
        }
    }
}
