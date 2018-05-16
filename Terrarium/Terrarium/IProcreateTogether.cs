using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    internal interface IProcreateTogether
    {
        void ProcreateTogether(ref Organism[,] arrOrganism, Organism neighborOrganism, ref Logboek logboek);
    }
}