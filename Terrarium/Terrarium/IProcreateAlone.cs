using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    internal interface IProcreateAlone
    {
        void ProcreateAlone(ref Organism[,] arrOrganism, ref Logboek logboek);
    }
}