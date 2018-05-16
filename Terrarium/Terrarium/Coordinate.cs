using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    public class Coordinate
    {
        // constructor
        public Coordinate(int xposition, int yposition)
        {
            xPosition = xposition;
            yPosition = yposition;
        }

        // X position in array

        public int xPosition
        {
            get;
            set;
        }

        // Y position in array

        public int yPosition
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"X: {xPosition} Y: {yPosition}";
        }
    }
}