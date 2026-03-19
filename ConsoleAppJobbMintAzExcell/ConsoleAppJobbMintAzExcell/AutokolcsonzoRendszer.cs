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

        public AutokolcsonzoRendszer()
        {
            this.autok = new List<Auto>();
        }

        public void FelVetel(string neve, long berlesAra, bool vanbiztositasa)
        {
            Auto auto = new Auto(neve, berlesAra, vanbiztositasa);
            if (berlesAra > 0)
            {
                autok.Add(auto);
            }
        }
    }
}
