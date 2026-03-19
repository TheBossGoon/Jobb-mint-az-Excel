using System;
using System.Collections.Generic;
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

        public void FelVetel(string neve, long berlesAra, bool vanBerelve, DateTime kiberlesKezdete, DateTime kiberlesVege, bool vanBiztositas)
        {
            Auto auto = new Auto(neve, berlesAra, vanBerelve, kiberlesKezdete, kiberlesVege, vanBiztositas);
            if (berlesAra > 0 && PartnerAutoMarkakListaja.Contains(neve))
            {
                autok.Add(auto);
            }
            else
            {
                Console.WriteLine("Nulla vagy alacsonyabb a bérlési ára vagy pedig nincs ilyen nevű kocsi a partner márkák listájában.");
            }
        }
    }
}
