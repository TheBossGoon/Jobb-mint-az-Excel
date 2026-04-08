using System;
using System.Collections.Generic;
using System.Linq;
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

            while (mukszik)
            {
                Console.WriteLine("Autókölcsönző");
                Console.WriteLine("-----------------------");
                Console.WriteLine("F1: Autókölcsönzö beolvasása");
                Console.WriteLine("F2: Autó kreálása");
                Console.WriteLine("F3: Autó Bérlése");
                Console.WriteLine("F4: Autó Bérlésének törlése");
                Console.WriteLine("F5: Autó nevének megvátoztása");
                Console.WriteLine("F6: Autó Bérlésének törlése");
                Console.WriteLine("F9: Kilépés.");
                ConsoleKeyInfo input = Console.ReadKey();
                Console.WriteLine();

                switch (input.Key)
                {
                    case ConsoleKey.F1:
                        Console.WriteLine("Írja be a .txt nevét!");
                        string BeolvasasInput = Console.ReadLine();
                        Autokolcsonzo.AutokBeolvasasa(BeolvasasInput);
                        break;
                    case ConsoleKey.F2:
                        string Autoneve, marka;
                        long berlesAra;
                        bool vanBerelve, vanbiztositva;

                        Console.WriteLine("Kérem adja meg az Autó nevét");
                        Autoneve = Console.ReadLine();
                        Console.WriteLine("Márkáját (Opel, Toyota, BYD, Volkswagen, Tesla, Honda, BMW, Hyundai, Ford, Mercedes-Benz, Geely Group, Kia, Nissan, Porsche, Subaru, General Motors, GM, Volvo, Audi, Mazda, Ferrari, Suziki)");
                        marka = Console.ReadLine();
                        Console.WriteLine("Van bérelve? (Igen/Nem)");
                        string berlesBevitel = Console.ReadLine();
                        if (berlesBevitel.ToLower() == "igen")
                        {
                            vanBerelve = true;

                        }
                        //Autokolcsonzo.FelVetel();
                        break;
                    case ConsoleKey.F3:
                        Console.WriteLine("Melyik autót szeretné kibérelni?");
                        Autokolcsonzo.AutokKiirasa();
                        Console.WriteLine("(Kérem írja az autó nevét)");
                        string kibereltAutoNeve = Console.ReadLine();
                        Console.WriteLine("Mikor legyen a bérlésnek vége? (Kérem az időt így formázza meg: ÉÉÉÉ/HH/NN)");
                        DateTime berlesVege = DateTime.Parse(Console.ReadLine());
                        try
                        {
                            Autokolcsonzo.AutoKereseseNevSzerint(kibereltAutoNeve).Berles(DateTime.Now, berlesVege);
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Ilyen kocsi nem létezik!");
                        }

                        break;
                    case ConsoleKey.F9:
                        mukszik = false;
                        Console.WriteLine("Köszönjük hogy a mi autókölcsönzőrendszerünket használta Excel helyett!");
                        break;

                }
            }
        }
    }
}
