using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    public class Herbivore : Animal, IEat
    {
        public Herbivore(int xposition, int yposition) : base(xposition, yposition)
        {
            if (this.Sex == Sex.Male)
            {
                this.Sprite = "H";
            }
            else
            {
                this.Sprite = "h";
            }
            this.ID = "Herbivore_" + Counter;
        }

        public Herbivore(int xposition, int yposition, int moves, OrganismMoves lastmove = OrganismMoves.Nothing) : base(xposition, yposition, moves)
        {
            if (this.Sex == Sex.Male)
            {
                this.Sprite = "H";
            }
            else
            {
                this.Sprite = "h";
            }
            this.LastMove = lastmove;
            this.ID = "Herbivore_" + Counter;
        }

        public void Eat(ref Organism[,] arrOrganism, Organism opponentOrganism)
        {
            var xPosition = opponentOrganism.Position.xPosition;
            var yPosition = opponentOrganism.Position.yPosition;

            // add to life
            this.Life += opponentOrganism.Life;

            // destroy opponent organism, replace by terrain
            arrOrganism[xPosition, yPosition] = new Terrain(xPosition, yPosition);

            // move to new location
            this.MoveTo(ref arrOrganism, xPosition, yPosition);

            // set moves on zero
            this.Moves--;
            opponentOrganism.Moves--;

            // set last move to eat
            this.LastMove = OrganismMoves.Eat;
        }

        public override void DisplayOrganism()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            base.DisplayOrganism();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}