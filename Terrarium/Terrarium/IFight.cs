using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    internal interface IFight
    {
        void Fight(ref Organism[,] arrOrganism, Organism opponentOrganism, ref Logboek logbook);
    }
}