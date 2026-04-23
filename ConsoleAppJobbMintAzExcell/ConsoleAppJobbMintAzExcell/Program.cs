using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppJobbMintAzExcell
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AutokolcsonzoRendszer Autokolcsonzo = new AutokolcsonzoRendszer();

            bool mukszik = true;

            //Autokolcsonzo.FelVetel("auto1", "Opel", 12331232, true, DateTime.Now, DateTime.Now, true);
            //Autokolcsonzo.FelVetel("auto2", "BYD", 123, false, DateTime.Now, DateTime.Now, false);


            while (mukszik)
            {
                Console.WriteLine("Autókölcsönző");
                Console.WriteLine("-----------------------");
                Console.WriteLine("F1: Autókölcsönzö Beolvasása");
                Console.WriteLine("F2: Autók Kiírása");
                Console.WriteLine("F3: Autó Kreálása");
                Console.WriteLine("F4: Autó Bérlése");
                Console.WriteLine("F5: Autó Bérlési Árának Megváltoztatása");
                Console.WriteLine("F6: Autó Nevének Megváltoztása");
                Console.WriteLine("F7: Autó Bérlésének Törlése");
                Console.WriteLine("F8: Autó Biztosításának hozzáadása");
                Console.WriteLine("F9: Autó Biztosításának Törlése a Rendszerből");
                Console.WriteLine("F10: Autó Törlése a Rendszerből");
                Console.WriteLine("F12: Autók Külön Fájlba Írása");
                Console.WriteLine("Esc: Kilépés.");
                ConsoleKeyInfo input = Console.ReadKey();
                Console.WriteLine();
                Console.Clear();

                switch (input.Key)
                {
                    case ConsoleKey.F1:
                        Console.WriteLine("Írja be a .txt nevét!");
                        string BeolvasasInput = Console.ReadLine();
                        Autokolcsonzo.AutokBeolvasasa(BeolvasasInput);
                        Console.WriteLine("\n");
                        break;
                    case ConsoleKey.F2:
                        Autokolcsonzo.AutokKiirasa();
                        Console.WriteLine("\n");
                        break;
                    case ConsoleKey.F3:
                        string Autoneve, marka;
                        long berlesAra = -1;
                        bool vanBerelve = false;
                        bool vanbiztositva = false;
                        DateTime kiberlesKezdete = DateTime.Now;
                        DateTime kiberlesVege = DateTime.MinValue;

                        Console.WriteLine("Kérem adja meg az Autó nevét");
                        Autoneve = Console.ReadLine();
                        if (!CheckNameValidity(Autoneve))
                        {
                            Console.WriteLine("Nem lehet \";\" vagy \";\" a névben!");
                            break;
                        }
                        Console.WriteLine("Márkáját (Opel, Toyota, BYD, Volkswagen, Tesla, Honda, BMW, Hyundai, Ford, Mercedes-Benz, Geely Group, Kia, Nissan, Porsche, Subaru, General Motors, GM, Volvo, Audi, Mazda, Ferrari, Suziki)");
                        marka = Console.ReadLine();
                        if (!AutokolcsonzoRendszer.PartnerAutoMarkakListaja.Contains(marka))
                        {
                            Console.WriteLine("Ez az autó nem szerepel a partnereink közt!");
                            break;
                        }
                        Console.WriteLine("Van bérelve? (Igen/Nem)");
                        string berlesBevitel = Console.ReadLine();

                        bool helyesBerlesValasz = false;
                        do
                        {
                            if (berlesBevitel.ToLower() == "n" || berlesBevitel.ToLower() == "nem" || berlesBevitel.ToLower() == "no")
                            {
                                helyesBerlesValasz = true;
                            }
                            else if (berlesBevitel.ToLower() == "igen" || berlesBevitel.ToLower() == "i" || berlesBevitel.ToLower() == "y" || berlesBevitel.ToLower() == "yes")
                            {
                                vanBerelve = true;
                                Console.WriteLine("Mennyiért lehessen bérelni az autót?");
                                string berlesAraInput = "";


                                berlesAraInput = Console.ReadLine();
                                if (long.TryParse(berlesAraInput, out berlesAra))
                                {
                                    if (berlesAra < 0)
                                    {
                                        Console.WriteLine("Bérlés ára nem lehet kisebb 0-nál!");
                                        continue;
                                    }
                                    Console.WriteLine("Mikor legyen a bérlésnek vége? (Kérem az időt így formázza meg: ÉÉÉÉ.HH.NN)");
                                    try
                                    {
                                        kiberlesVege = DateTime.Parse(Console.ReadLine());
                                        if (kiberlesVege <= kiberlesKezdete + TimeSpan.FromHours(24))
                                        {
                                            Console.WriteLine("a kibérlés legalább 1 nappal a kibérlés kezdete után kell hogy legyen!");
                                            continue;
                                        }
                                        helyesBerlesValasz = true;
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Nem megfelelő a formázása a megadott idő az kért formázás alapján!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Csak számokból állhat az ár!");
                                    berlesBevitel = Console.ReadLine();
                                }
                                //Console.WriteLine($"\nberles ara {berlesAraInput}\n\n");

                            }
                            else
                            {
                                Console.WriteLine("Sajnálom, de ezt az utasítást nem értettem!");
                                berlesBevitel = Console.ReadLine();
                            }
                        } while (!helyesBerlesValasz);
                        Console.WriteLine("Szeretne Biztosítást? (Igen/Nem)");
                        bool helyesbiztositasInput = false;
                        do
                        {
                            string biztositasInput = Console.ReadLine();
                            if (biztositasInput.ToLower() == "n" || berlesBevitel.ToLower() == "nem" || berlesBevitel.ToLower() == "no")
                            {
                                helyesbiztositasInput = true;
                            }
                            if (biztositasInput.ToLower() == "igen" || biztositasInput.ToLower() == "i" || biztositasInput.ToLower() == "y" || biztositasInput.ToLower() == "yes")
                            {
                                helyesbiztositasInput = true;
                                vanbiztositva = true;
                            }
                            else
                            {
                                Console.WriteLine("Sajnálom, de ezt az utasítást nem értettem!");
                            }
                        } while (!helyesbiztositasInput);

                        Autokolcsonzo.FelVetel(Autoneve, marka, berlesAra, vanBerelve, kiberlesKezdete, kiberlesVege, vanbiztositva);
                        Console.WriteLine("Autó sikeresen hozzáadva az adatbázishoz!");
                        break;
                    case ConsoleKey.F4:
                        Console.WriteLine("Melyik autót szeretné kibérelni?");
                        Autokolcsonzo.AutokKiirasa();
                        Console.WriteLine("(Kérem írja le az autó nevét)");
                        string kiberelendoAutoNeve = Console.ReadLine();
                        if (!CheckNameValidity(kiberelendoAutoNeve))
                        {
                            Console.WriteLine("Nem lehet \";\" vagy \";\" a névben!");
                            break;
                        }
                        try
                        {
                            if (Autokolcsonzo.AutoKereseseNevSzerint(kiberelendoAutoNeve).VanBerelve)
                            {
                                Console.WriteLine("Ez az autó már bérelve van!");
                                break;
                            }
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Ilyen kocsi nem létezik!");
                            break;
                        }
                        Console.WriteLine("Mikor legyen a bérlésnek vége? (Kérem az időt így formázza meg: ÉÉÉÉ.HH.NN)");
                        DateTime berlesVege = DateTime.MinValue;
                        try
                        {
                            berlesVege = DateTime.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Nem megfelelő időt adott meg a formázás alapján!");
                            break;
                        }

                        if (berlesVege > DateTime.Now + TimeSpan.FromDays(1))
                        {
                            Autokolcsonzo.AutoKereseseNevSzerint(kiberelendoAutoNeve).Berles(berlesVege);
                            Console.WriteLine("Kibérlés sikeres!");
                        }
                        else
                        {
                            Console.WriteLine("a kibérlés legalább 1 nappal a kibérlés kezdete után kell hogy legyen!");
                            Console.WriteLine("Kibérlés sikertelen!");
                            break;
                        }



                        break;
                    case ConsoleKey.F5:
                        Console.WriteLine("Melyik autót szeretné kibérelni?");
                        Autokolcsonzo.AutokKiirasa();
                        Console.WriteLine("(Kérem írja le az autó nevét)");
                        string berMegvaltatoztatandoNev = Console.ReadLine();
                        if (!CheckNameValidity(berMegvaltatoztatandoNev))
                        {
                            Console.WriteLine("Nem lehet \";\" vagy \";\" a névben!");
                            break;
                        }
                        Console.WriteLine("Mennyi legyen az új bérlési ár?");
                        long ujBerlesiAr = -1;
                        try
                        {
                            ujBerlesiAr = long.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Nem számokat írt be új bérlési árnak!");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Túl nagy számot írt be!");
                        }
                        try
                        {
                            Autokolcsonzo.AutoKereseseNevSzerint(berMegvaltatoztatandoNev).BerlesAranakValtoztatas(ujBerlesiAr);
                            
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Ilyen kocsi nem létezik!");
                        }

                        break;
                    case ConsoleKey.F6:

                        Console.WriteLine("Melyik autó nevét szeretné megváltoztatni?");
                        Autokolcsonzo.AutokKiirasa();
                        Console.WriteLine("(Kérem írja le az autó nevét)");
                        string megvaltatoztandoAuto = Console.ReadLine();
                        if (!CheckNameValidity(megvaltatoztandoAuto))
                        {
                            Console.WriteLine("Nem lehet \";\" vagy \";\" a névben!");
                            break;
                        }
                        Console.WriteLine("Mire szeretné megváltoztatni az autó nevét?");
                        string amireValtozik = Console.ReadLine();
                        if (!CheckNameValidity(amireValtozik))
                        {
                            Console.WriteLine("Nem lehet \";\" vagy \";\" a névben!");
                            break;
                        }
                        try
                        {
                            Autokolcsonzo.AutoKereseseNevSzerint(megvaltatoztandoAuto).NevValtoztatas(amireValtozik);
                            Console.WriteLine("Autó nevének megváltoztatása sikeres!");
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Ilyen kocsi nem létezik!");
                        }
                        break;
                    case ConsoleKey.F7:
                        Console.WriteLine("Melyik autó bérlését szeretné törölni?");
                        Autokolcsonzo.AutokKiirasa();
                        Console.WriteLine("(Kérem írja le az autó nevét)");
                        string kibereltAutoNeve = Console.ReadLine();
                        if (!CheckNameValidity(kibereltAutoNeve))
                        {
                            Console.WriteLine("Nem lehet \";\" vagy \";\" a névben!");
                            break;
                        }
                        try
                        {
                            Autokolcsonzo.AutoKereseseNevSzerint(kibereltAutoNeve).BerlesTorlese();
                            Console.WriteLine("Bérlés törlése sikeres!");
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Ilyen kocsi nem létezik!");
                        }
                        break;
                    case ConsoleKey.F8:
                        Console.WriteLine("Melyik autónak szeretne biztosítst adni?");
                        Autokolcsonzo.AutokKiirasa();
                        Console.WriteLine("(Kérem írja le az autó nevét)");
                        string biztositandoAuto = Console.ReadLine();
                        if (!CheckNameValidity(biztositandoAuto))
                        {
                            Console.WriteLine("Nem lehet \";\" vagy \";\" a névben!");
                            break;
                        }
                        try
                        {
                            Autokolcsonzo.AutoKereseseNevSzerint(biztositandoAuto).BiztositasHozzaadas();
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Ilyen kocsi nem létezik!");
                        }
                        break;
                    case ConsoleKey.F9:
                        Console.WriteLine("Melyik autónak szeretne biztosítst adni?");
                        Autokolcsonzo.AutokKiirasa();
                        Console.WriteLine("(Kérem írja le az autó nevét)");
                        string biztositVeszitoAuto = Console.ReadLine();
                        if (!CheckNameValidity(biztositVeszitoAuto))
                        {
                            Console.WriteLine("Nem lehet \";\" vagy \";\" a névben!");
                            break;
                        }
                        try
                        {
                            Autokolcsonzo.AutoKereseseNevSzerint(biztositVeszitoAuto).BiztositasTorlese();
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Ilyen kocsi nem létezik!");
                        }
                        break;
                    case ConsoleKey.F10:
                        Console.WriteLine("Melyik autónak szeretne biztosítst adni?");
                        Autokolcsonzo.AutokKiirasa();
                        Console.WriteLine("(Kérem írja le az autó nevét)");
                        string torlendoAuto = Console.ReadLine();
                        if (!CheckNameValidity(torlendoAuto))
                        {
                            Console.WriteLine("Nem lehet \";\" vagy \";\" a névben!");
                            break;
                        }

                        for (int i = 0; i < Autokolcsonzo.Autok.Count; i++)
                        {
                            if (Autokolcsonzo.Autok[i].Nev == torlendoAuto)
                            {
                                Autokolcsonzo.AutoTorlese(i);
                                break;
                            }
                        }
                        break;
                    case ConsoleKey.F12:
                        Console.WriteLine("Kérem adja meg a fájl nevét amibe írni szeretne!");
                        string outputFileNeve = Console.ReadLine();
                        Autokolcsonzo.AutokListajanakKiirasaKulonFajlba(outputFileNeve);
                        Console.WriteLine("Sikeres külön fájlba írás!");
                        break;
                    case ConsoleKey.Escape:
                        mukszik = false;
                        Console.WriteLine("Köszönjük, hogy a mi autókölcsönzőrendszerünket használta Excel helyett!");
                        break;

                }
            }
        }
        static bool CheckNameValidity(string input)
        {
            if (input.Contains(';') || input.Contains(':')) return false;
            return true;
        }
    }
}
