using System;
using System.Globalization;

namespace ConsoleAppJobbMintAzExcell
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AutokolcsonzoRendszer autokolcsonzo = new AutokolcsonzoRendszer();
            bool mukodik = true;

            while (mukodik)
            {
                MenuKiirasa();
                ConsoleKeyInfo input = Console.ReadKey();
                Console.WriteLine();
                Console.Clear();

                switch (input.Key)
                {
                    case ConsoleKey.F1:
                        Console.WriteLine("Írja be a .txt fájl nevét kiterjesztés nélkül!");
                        autokolcsonzo.AutokBeolvasasa(ReadRequiredText("Fájlnév"));
                        Console.WriteLine();
                        break;
                    case ConsoleKey.F2:
                        autokolcsonzo.AutokKiirasa();
                        Console.WriteLine();
                        break;
                    case ConsoleKey.F3:
                        AutoFelvetele(autokolcsonzo);
                        break;
                    case ConsoleKey.F4:
                        AutoBerlese(autokolcsonzo);
                        break;
                    case ConsoleKey.F5:
                        BerlesiArValtoztatasa(autokolcsonzo);
                        break;
                    case ConsoleKey.F6:
                        RendszamValtoztatasa(autokolcsonzo);
                        break;
                    case ConsoleKey.F7:
                        BerlesTorlese(autokolcsonzo);
                        break;
                    case ConsoleKey.F8:
                        BiztositasHozzaadasa(autokolcsonzo);
                        break;
                    case ConsoleKey.F9:
                        BiztositasTorlese(autokolcsonzo);
                        break;
                    case ConsoleKey.F10:
                        AutoTorlese(autokolcsonzo);
                        break;
                    case ConsoleKey.F12:
                        Console.WriteLine("Kérem adja meg a fájl nevét, amibe írni szeretne!");
                        if (autokolcsonzo.AutokListajanakKiirasaKulonFajlba(ReadRequiredText("Fájlnév")))
                        {
                            Console.WriteLine("Sikeres külön fájlba írás!");
                        }
                        break;
                    case ConsoleKey.Escape:
                        mukodik = false;
                        Console.WriteLine("Köszönjük, hogy a mi autókölcsönző rendszerünket használta Excel helyett!");
                        break;
                    default:
                        Console.WriteLine("Ismeretlen menüpont.");
                        break;
                }
            }
        }

        static void MenuKiirasa()
        {
            Console.WriteLine("Autókölcsönző");
            Console.WriteLine("-----------------------");
            Console.WriteLine("F1: Autókölcsönző beolvasása");
            Console.WriteLine("F2: Autók kiírása");
            Console.WriteLine("F3: Autó felvétele");
            Console.WriteLine("F4: Autó bérlése");
            Console.WriteLine("F5: Autó bérlési árának megváltoztatása");
            Console.WriteLine("F6: Autó rendszámának megváltoztatása");
            Console.WriteLine("F7: Autó bérlésének törlése");
            Console.WriteLine("F8: Autó biztosításának hozzáadása");
            Console.WriteLine("F9: Autó biztosításának törlése");
            Console.WriteLine("F10: Autó törlése");
            Console.WriteLine("F12: Autók külön fájlba írása");
            Console.WriteLine("Esc: Kilépés");
        }

        static void AutoFelvetele(AutokolcsonzoRendszer autokolcsonzo)
        {
            string rendszam = ReadRendszam("Kérem adja meg az autó rendszámát");
            if (autokolcsonzo.AutoKereseseRendszamSzerint(rendszam) != null)
            {
                Console.WriteLine("Ilyen rendszámú autó már szerepel a rendszerben!");
                return;
            }

            Console.WriteLine("Márka: Opel, Toyota, BYD, Volkswagen, Tesla, Honda, BMW, Hyundai, Ford, Mercedes-Benz, Geely Group, Kia, Nissan, Porsche, Subaru, General Motors, GM, Volvo, Audi, Mazda, Ferrari, Suzuki");
            string marka = ReadRequiredText("Márka");
            if (!AutokolcsonzoRendszer.PartnerAutoMarkakListaja.Contains(marka))
            {
                Console.WriteLine("Ez az autó nem szerepel a partnereink közt!");
                return;
            }

            long berlesAra = ReadLong("Mennyi legyen az autó bérlési ára?", 0);
            bool vanBerelve = ReadYesNo("Van bérelve? (Igen/Nem)");
            DateTime kiberlesKezdete = DateTime.Now;
            DateTime kiberlesVege = DateTime.MinValue;

            if (vanBerelve)
            {
                kiberlesVege = ReadDateAfter("Mikor legyen a bérlésnek vége? (ÉÉÉÉ.HH.NN)", kiberlesKezdete.AddDays(1));
            }

            bool vanBiztositas = ReadYesNo("Szeretne biztosítást? (Igen/Nem)");
            if (autokolcsonzo.FelVetel(rendszam, marka, berlesAra, vanBerelve, kiberlesKezdete, kiberlesVege, vanBiztositas))
            {
                Console.WriteLine("Autó sikeresen hozzáadva az adatbázishoz!");
            }
        }

        static void AutoBerlese(AutokolcsonzoRendszer autokolcsonzo)
        {
            Auto auto = AutoKivalasztasa(autokolcsonzo, "Melyik autót szeretné kibérelni?");
            if (auto == null) return;

            if (auto.VanBerelve)
            {
                Console.WriteLine("Ez az autó már bérelve van!");
                return;
            }

            DateTime berlesVege = ReadDateAfter("Mikor legyen a bérlésnek vége? (ÉÉÉÉ.HH.NN)", DateTime.Now.AddDays(1));
            auto.Berles(berlesVege);
            Console.WriteLine("Kibérlés sikeres!");
        }

        static void BerlesiArValtoztatasa(AutokolcsonzoRendszer autokolcsonzo)
        {
            Auto auto = AutoKivalasztasa(autokolcsonzo, "Melyik autó bérlési árát szeretné megváltoztatni?");
            if (auto == null) return;

            long ujBerlesiAr = ReadLong("Mennyi legyen az új bérlési ár?", 0);
            auto.BerlesAranakValtoztatas(ujBerlesiAr);
        }

        static void RendszamValtoztatasa(AutokolcsonzoRendszer autokolcsonzo)
        {
            Auto auto = AutoKivalasztasa(autokolcsonzo, "Melyik autó rendszámát szeretné megváltoztatni?");
            if (auto == null) return;

            string ujRendszam = ReadRendszam("Mire szeretné megváltoztatni az autó rendszámát?");
            if (autokolcsonzo.AutoKereseseRendszamSzerint(ujRendszam) != null)
            {
                Console.WriteLine("Ilyen rendszámú autó már szerepel a rendszerben!");
                return;
            }

            auto.RendszamValtoztatas(ujRendszam);
            Console.WriteLine("Autó rendszámának megváltoztatása sikeres!");
        }

        static void BerlesTorlese(AutokolcsonzoRendszer autokolcsonzo)
        {
            Auto auto = AutoKivalasztasa(autokolcsonzo, "Melyik autó bérlését szeretné törölni?");
            if (auto == null) return;

            bool eddigBerelveVolt = auto.VanBerelve;
            auto.BerlesTorlese();
            if (eddigBerelveVolt) Console.WriteLine("Bérlés törlése sikeres!");
        }

        static void BiztositasHozzaadasa(AutokolcsonzoRendszer autokolcsonzo)
        {
            Auto auto = AutoKivalasztasa(autokolcsonzo, "Melyik autónak szeretne biztosítást adni?");
            if (auto == null) return;

            auto.BiztositasHozzaadas();
        }

        static void BiztositasTorlese(AutokolcsonzoRendszer autokolcsonzo)
        {
            Auto auto = AutoKivalasztasa(autokolcsonzo, "Melyik autónak szeretné törölni a biztosítását?");
            if (auto == null) return;

            auto.BiztositasTorlese();
        }

        static void AutoTorlese(AutokolcsonzoRendszer autokolcsonzo)
        {
            Console.WriteLine("Melyik autót szeretné törölni?");
            autokolcsonzo.AutokKiirasa();
            string rendszam = ReadRendszam("Kérem írja le az autó rendszámát");
            autokolcsonzo.AutoTorleseRendszamSzerint(rendszam);
        }

        static Auto AutoKivalasztasa(AutokolcsonzoRendszer autokolcsonzo, string kerdes)
        {
            Console.WriteLine(kerdes);
            autokolcsonzo.AutokKiirasa();
            string rendszam = ReadRendszam("Kérem írja le az autó rendszámát");
            Auto auto = autokolcsonzo.AutoKereseseRendszamSzerint(rendszam);

            if (auto == null)
            {
                Console.WriteLine("Ilyen rendszámú autó nem létezik!");
            }

            return auto;
        }

        static string ReadRendszam(string kerdes)
        {
            while (true)
            {
                string rendszam = ReadRequiredText(kerdes);
                if (CheckNameValidity(rendszam)) return rendszam;

                Console.WriteLine("Nem lehet üres, illetve nem tartalmazhat \";\" vagy \":\" karaktert.");
            }
        }

        static string ReadRequiredText(string kerdes)
        {
            while (true)
            {
                Console.WriteLine(kerdes);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input)) return input.Trim();

                Console.WriteLine("Kérem, adjon meg egy értéket!");
            }
        }

        static bool ReadYesNo(string kerdes)
        {
            while (true)
            {
                Console.WriteLine(kerdes);
                string input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Kérem, válaszoljon igennel vagy nemmel!");
                    continue;
                }

                input = input.Trim().ToLower();
                if (input == "i" || input == "igen" || input == "y" || input == "yes") return true;
                if (input == "n" || input == "nem" || input == "no") return false;

                Console.WriteLine("Sajnálom, de ezt az utasítást nem értettem!");
            }
        }

        static long ReadLong(string kerdes, long minimum)
        {
            while (true)
            {
                Console.WriteLine(kerdes);
                long value;
                if (long.TryParse(Console.ReadLine(), out value) && value >= minimum) return value;

                Console.WriteLine("Kérem, adjon meg egy érvényes számot!");
            }
        }

        static DateTime ReadDateAfter(string kerdes, DateTime minimum)
        {
            while (true)
            {
                Console.WriteLine(kerdes);
                DateTime datum;
                if (TryParseDate(Console.ReadLine(), out datum) && datum.Date >= minimum.Date) return datum;

                Console.WriteLine("Nem megfelelő dátum. A dátum formátuma legyen ÉÉÉÉ.HH.NN, és legalább 1 nappal későbbi legyen a kezdésnél!");
            }
        }

        static bool TryParseDate(string input, out DateTime datum)
        {
            return DateTime.TryParseExact(input, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datum)
                || DateTime.TryParse(input, out datum);
        }

        static bool CheckNameValidity(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && !input.Contains(";") && !input.Contains(":");
        }
    }
}
