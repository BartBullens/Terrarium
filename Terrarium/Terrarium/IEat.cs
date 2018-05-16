using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    internal interface IEat
    {
        void Eat(ref Organism[,] arrOrganism, Organism opponentOrganism);
    }
}