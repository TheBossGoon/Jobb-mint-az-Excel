using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ConsoleAppJobbMintAzExcell
{
    internal class AutokolcsonzoRendszer
    {
        private readonly List<Auto> autok;

        public List<Auto> Autok { get => new List<Auto>(autok); }

        internal static HashSet<string> PartnerAutoMarkakListaja = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Opel", "Toyota", "BYD", "Volkswagen", "Tesla", "Honda", "BMW", "Hyundai",
            "Ford", "Mercedes-Benz", "Geely Group", "Kia", "Nissan", "Porsche", "Subaru",
            "General Motors", "GM", "Volvo", "Audi", "Mazda", "Ferrari", "Suzuki"
        };

        public AutokolcsonzoRendszer()
        {
            autok = new List<Auto>();
        }

        public bool FelVetel(string rendszam, string marka, long berlesAra, bool vanBerelve, DateTime kiberlesKezdete, DateTime kiberlesVege, bool vanBiztositas)
        {
            if (string.IsNullOrWhiteSpace(rendszam))
            {
                Console.WriteLine("A rendszám nem lehet üres.");
                return false;
            }

            if (AutoKereseseRendszamSzerint(rendszam) != null)
            {
                Console.WriteLine("Ilyen rendszámú autó már szerepel a rendszerben.");
                return false;
            }

            if (!PartnerAutoMarkakListaja.Contains(marka))
            {
                Console.WriteLine("Nincs ilyen márkájú kocsi a partner márkák listájában.");
                return false;
            }

            if (berlesAra < 0)
            {
                Console.WriteLine("A bérlési ár nem lehet negatív.");
                return false;
            }

            if (vanBerelve && kiberlesVege.Date < kiberlesKezdete.Date.AddDays(1))
            {
                Console.WriteLine("A bérlés vége legalább 1 nappal a kezdés után lehet.");
                return false;
            }

            autok.Add(new Auto(rendszam.Trim(), marka, berlesAra, vanBerelve, kiberlesKezdete, kiberlesVege, vanBiztositas));
            return true;
        }

        public void AutokBeolvasasa(string beolvasandoFajl)
        {
            try
            {
                using (StreamReader sr = new StreamReader(beolvasandoFajl + ".txt"))
                {
                    sr.ReadLine();
                    int beolvasottSorokSzama = 0;
                    int hibasSorokSzama = 0;

                    while (!sr.EndOfStream)
                    {
                        Auto auto;
                        if (TryAutoBeolvasasaSorbol(sr.ReadLine(), out auto)
                            && FelVetel(auto.Rendszam, auto.Marka, auto.BerlesAra, auto.VanBerelve, auto.KiberlesKezdete, auto.KiberlesVege, auto.VanBiztositas))
                        {
                            beolvasottSorokSzama++;
                        }
                        else
                        {
                            hibasSorokSzama++;
                        }
                    }

                    Console.WriteLine($"Sikeres beolvasás: {beolvasottSorokSzama} autó.");
                    if (hibasSorokSzama > 0) Console.WriteLine($"Kihagyott hibás vagy duplikált sorok száma: {hibasSorokSzama}.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Nem létező fájl nevet adott meg, próbálja meg újra!");
            }
            catch (IOException)
            {
                Console.WriteLine("A fájl beolvasása közben hiba történt.");
            }
        }

        public void AutoTorlese(int bekertIndex)
        {
            if (bekertIndex < autok.Count && bekertIndex >= 0)
            {
                autok.RemoveAt(bekertIndex);
                Console.WriteLine("Autó törlése sikeres!");
            }
            else
            {
                Console.WriteLine("Az autó nincs benne a listában!");
            }
        }

        public bool AutokListajanakKiirasaKulonFajlba(string kiirtFajlNeve)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(kiirtFajlNeve + ".txt"))
                {
                    sw.WriteLine("Autó rendszáma; Márka; Bérlési ára; Bérelve van-e; Bérlés kezdete; Bérlés vége; Biztosítás van-e;");
                    foreach (Auto auto in autok)
                    {
                        sw.WriteLine($"{auto.Rendszam.Trim()};{auto.Marka};{auto.BerlesAra};{auto.VanBerelve};{auto.KiberlesKezdete:yyyy.MM.dd};{auto.KiberlesVege:yyyy.MM.dd};{auto.VanBiztositas};");
                    }
                }

                return true;
            }
            catch (IOException)
            {
                Console.WriteLine("A fájl írása közben hiba történt.");
                return false;
            }
        }

        public Auto AutoKereseseRendszamSzerint(string eztKeresd)
        {
            if (string.IsNullOrWhiteSpace(eztKeresd)) return null;

            for (int i = 0; i < autok.Count; i++)
            {
                if (autok[i].Rendszam.Equals(eztKeresd.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return autok[i];
                }
            }

            return null;
        }

        public void AutoTorleseRendszamSzerint(string rendszam)
        {
            Auto torlendoAuto = AutoKereseseRendszamSzerint(rendszam);
            if (torlendoAuto == null)
            {
                Console.WriteLine("Ilyen rendszámú autó nem létezik!");
                return;
            }

            autok.Remove(torlendoAuto);
            Console.WriteLine("Autó törlése sikeres!");
        }

        public void AutokKiirasa()
        {
            if (autok.Count == 0)
            {
                Console.WriteLine("Nincs autó a rendszerben.");
                return;
            }

            for (int i = 0; i < autok.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] - {autok[i]}");
            }
        }

        private bool TryAutoBeolvasasaSorbol(string sor, out Auto auto)
        {
            auto = null;
            if (string.IsNullOrWhiteSpace(sor)) return false;

            string[] adatok = sor.Split(';');
            if (adatok.Length < 7) return false;

            string rendszam = adatok[0].Trim();
            string marka = adatok[1].Trim();
            long berlesAra;
            bool vanBerelve;
            DateTime kiberlesKezdete;
            DateTime kiberlesVege;
            bool vanBiztositas;

            if (!long.TryParse(adatok[2], out berlesAra)) return false;
            if (!bool.TryParse(adatok[3], out vanBerelve)) return false;
            if (!TryParseDate(adatok[4], out kiberlesKezdete)) return false;
            if (!TryParseDate(adatok[5], out kiberlesVege)) return false;
            if (!bool.TryParse(adatok[6], out vanBiztositas)) return false;

            auto = new Auto(rendszam, marka, berlesAra, vanBerelve, kiberlesKezdete, kiberlesVege, vanBiztositas);
            return true;
        }

        private bool TryParseDate(string input, out DateTime datum)
        {
            return DateTime.TryParseExact(input, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datum)
                || DateTime.TryParse(input, out datum);
        }
    }
}
