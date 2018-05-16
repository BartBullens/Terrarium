using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    public class Terrain : Organism, IProcreateAlone
    {
        public Terrain(int xposition, int yposition) : base(xposition, yposition)
        {
            this.Sprite = ".";
            //this.Position = new Coordinate(xposition, yposition);
            this.IsWalkable = true;
            this.ID = "Terrain_" + Counter;
        }

        private static Random random = new Random();

        public void ProcreateAlone(ref Organism[,] arrOrganism, ref Logboek logboek)
        {
            //Aanpasbare kans op groei van een plant
            var chance = 10m;
            var randomChance = random.Next(1, 101);

            if (randomChance <= chance)
            {
                //Wordt gegeven grond een plant
                var xPosition = this.Position.xPosition;
                var yPosition = this.Position.yPosition;

                arrOrganism[xPosition, yPosition] = null;
                arrOrganism[xPosition, yPosition] = new Plant(xPosition, yPosition);
                logboek.numberOfPlants += 1;
                logboek.CreatedPlants += 1;
            }
        }

        public override void DisplayOrganism()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            base.DisplayOrganism();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}