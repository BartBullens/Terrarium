using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    public class _LineDrawer
    {
        public string DrawLine(int length, char symbol)
        {
            string line = string.Empty;
            for (int i = 0; i < length; i++)
            {
                line += symbol;
            }
            line += "\n";
            return line;
        }
    }
}