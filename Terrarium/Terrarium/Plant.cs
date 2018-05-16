using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    public class Plant : Organism
    {
        public Plant(int xposition, int yposition) : base(xposition, yposition)
        {
            this.Sprite = "P";
            this.Life = 1;
            this.Position = new Coordinate(xposition, yposition);
            this.ID = "Plant_" + Counter;
        }

        public override void DisplayOrganism()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            base.DisplayOrganism();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}