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

            Autokolcsonzo.FelVetel("auto1", "Opel", 12331232, true, DateTime.Now, DateTime.Now, true);
            Autokolcsonzo.FelVetel("auto2", "BYD", 123, false, DateTime.Now, DateTime.Now, false);


            while (mukszik)
            {
                Console.WriteLine("Autókölcsönző");
                Console.WriteLine("-----------------------");
                Console.WriteLine("F1: Autókölcsönzö Beolvasása");
                Console.WriteLine("F2: Autó Kreálása");
                Console.WriteLine("F3: Autó Bérlése");
                Console.WriteLine("F4: Autó Bérlési Árának Megváltoztatása");
                Console.WriteLine("F5: Autó Nevének Megváltoztása");
                Console.WriteLine("F6: Autó Bérlésének Törlése");
                Console.WriteLine("F7: Autó Biztosításának hozzáadása");
                Console.WriteLine("F8: Autó Biztosításának Törlése a Rendszerből");
                Console.WriteLine("F9: Autó Törlése a Rendszerből");
                Console.WriteLine("F12: Kilépés.");
                ConsoleKeyInfo input = Console.ReadKey();
                Console.WriteLine();
                Console.Clear();

                switch (input.Key)
                {
                    case ConsoleKey.F1:
                        Console.WriteLine("Írja be a .txt nevét!");
                        string BeolvasasInput = Console.ReadLine();
                        Autokolcsonzo.AutokBeolvasasa(BeolvasasInput);
                        break;
                    case ConsoleKey.F2:
                        string Autoneve, marka;
                        long berlesAra = -1;
                        bool vanBerelve, vanbiztositva = false;

                        Console.WriteLine("Kérem adja meg az Autó nevét");
                        Autoneve = Console.ReadLine();
                        if (!CheckNameValidity(Autoneve))
                        {
                            Console.WriteLine("Ne használjon ");
                        }
                        Console.WriteLine("Márkáját (Opel, Toyota, BYD, Volkswagen, Tesla, Honda, BMW, Hyundai, Ford, Mercedes-Benz, Geely Group, Kia, Nissan, Porsche, Subaru, General Motors, GM, Volvo, Audi, Mazda, Ferrari, Suziki)");
                        marka = Console.ReadLine();
                        Console.WriteLine("Van bérelve? (Igen/Nem)");
                        string berlesBevitel = Console.ReadLine();
                        if (berlesBevitel.ToLower() == "igen" || berlesBevitel.ToLower() == "i" || berlesBevitel.ToLower() == "y" || berlesBevitel.ToLower() == "yes")
                        {
                            vanBerelve = true;
                            string berlesAraInput = Console.ReadLine();
                            if (long.TryParse(berlesAraInput, out berlesAra))
                            {
                                if (berlesAra < 0 ) 
                                { 
                                    Console.WriteLine("");
                                    break;
                                }
                            }
                            

                        }
                        //Autokolcsonzo.FelVetel();
                        break;
                    case ConsoleKey.F3:
                        Console.WriteLine("Melyik autót szeretné kibérelni?");
                        Autokolcsonzo.AutokKiirasa();
                        Console.WriteLine("(Kérem írja le az autó nevét)");
                        string kiberelendoAutoNeve = Console.ReadLine();
                        if (!CheckNameValidity(kiberelendoAutoNeve))
                        {
                            Console.WriteLine("Nem lehet \";\" vagy \";\" a névben!");
                            break;
                        }
                        Console.WriteLine("Mikor legyen a bérlésnek vége? (Kérem az időt így formázza meg: ÉÉÉÉ/HH/NN)");
                        DateTime berlesVege = DateTime.MinValue;
                        try
                        {
                            berlesVege = DateTime.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Nem megfelelő időt adott meg a formázás alapján!");
                        }
                        try
                        {
                            if (berlesVege == DateTime.MinValue)
                            {
                                Autokolcsonzo.AutoKereseseNevSzerint(kiberelendoAutoNeve).Berles(berlesVege);
                                Console.WriteLine("Kibérlés sikeres!");
                            }
                            else
                            {
                                Console.WriteLine("Nem adott meg egy helyesen megformázott dátumot!");
                                Console.WriteLine("Kibérlés sikertelen!");
                            }
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Ilyen kocsi nem létezik!");
                        }

                        break;
                    case ConsoleKey.F4:
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
                            Console.WriteLine("Bér megváltoztatása sikeres!");
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Ilyen kocsi nem létezik!");
                        }
                        
                        break;
                    case ConsoleKey.F5:

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
                            Console.WriteLine("Autó nevének megváltoztatása!");
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Ilyen kocsi nem létezik!");
                        }
                        break;
                    case ConsoleKey.F6:
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
                    case ConsoleKey.F7:
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
                            Console.WriteLine("Autó biztosítása sikeres!");
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
                        string biztositVeszitoAuto = Console.ReadLine();
                        if (!CheckNameValidity(biztositVeszitoAuto))
                        {
                            Console.WriteLine("Nem lehet \";\" vagy \";\" a névben!");
                            break;
                        }
                        try
                        {
                            Autokolcsonzo.AutoKereseseNevSzerint(biztositVeszitoAuto).BiztositasTorlese();
                            Console.WriteLine("Autó biztosításának törlése sikeres!");
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
                                Console.WriteLine("Autó törlése sikeres!");
                                break;
                            }
                            Console.WriteLine("Nincs ilyen autó a listában!");
                        }
                        break;
                    case ConsoleKey.F12:
                        mukszik = false;
                        Console.WriteLine("Köszönjük hogy a mi autókölcsönzőrendszerünket használta Excel helyett!");
                        break;

                }
            }
        }
        static bool CheckNameValidity (string input)
        {
            if (input.Contains(';') || input.Contains(':')) return false;
            return true;
        }
}
}
