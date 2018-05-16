using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    public class Carnivore : Animal, IFight, IEat
    {
        public Carnivore(int xposition, int yposition) : base(xposition, yposition)
        {
            if (this.Sex == Sex.Male)
            {
                this.Sprite = "C";
            }
            else
            {
                this.Sprite = "c";
            }

            //this.Position = new Coordinate(xposition, yposition);
            this.ID = "Carnivore_" + Counter;
        }

        public Carnivore(int xposition, int yposition, int moves, OrganismMoves lastmove = OrganismMoves.Nothing) : base(xposition, yposition, moves)
        {
            if (this.Sex == Sex.Male)
            {
                this.Sprite = "C";
            }
            else
            {
                this.Sprite = "c";
            }
            this.LastMove = lastmove;
            //this.Position = new Coordinate(xposition, yposition);
            this.ID = "Carnivore_" + Counter;
        }

        public void Fight(ref Organism[,] arrOrganism, Organism opponentOrganism, ref Logboek logbook)
        {
            var xPosition = this.Position.xPosition;
            var yPosition = this.Position.yPosition;

            if (this.Life == opponentOrganism.Life)
            {
                if (opponentOrganism is Human)
                {
                    // defeat, add life to opponent
                    opponentOrganism.Life += this.Life;

                    // destroy yourself, make terrain at this location
                    arrOrganism[xPosition, yPosition] = new Terrain(xPosition, yPosition);

                    // opponent moves to this location
                    ((Animal)opponentOrganism).MoveTo(ref arrOrganism, xPosition, yPosition);
                    logbook.numberOfCarnivores--;
                    logbook.DiedCarnivores++;
                }
                // do nothing since opponent organism is carnivore
            }
            else if (this.Life > opponentOrganism.Life)
            {
                // victory
                // add opponent's life to yours
                this.Life += opponentOrganism.Life;

                // destroy opponent, make terrain at his location
                arrOrganism[opponentOrganism.Position.xPosition, opponentOrganism.Position.yPosition] = new Terrain(opponentOrganism.Position.xPosition, opponentOrganism.Position.yPosition);

                // move to opponent's location
                this.MoveTo(ref arrOrganism, opponentOrganism.Position.xPosition, opponentOrganism.Position.yPosition);

                // if carnivore, adjust logbook appropriately, if human, adjust appropriately
                if (opponentOrganism is Carnivore)
                {
                    logbook.numberOfCarnivores--;
                    logbook.DiedCarnivores++;
                }
                else
                // is human
                {
                    logbook.numberOfHumans--;
                    logbook.DiedHumans++;
                }
            }
            else
            {
                // lose, add life to opponent
                opponentOrganism.Life += this.Life;

                // destroy yourself, make terrain at this location
                arrOrganism[xPosition, yPosition] = new Terrain(xPosition, yPosition);

                // opponent moves to this location
                ((Animal)opponentOrganism).MoveTo(ref arrOrganism, xPosition, yPosition);
                logbook.numberOfCarnivores--;
                logbook.DiedCarnivores++;
            }

            // reduce moves by 1 for both organisms
            // set last move to fight for both organisms
            this.LastMove = OrganismMoves.Fight;
            opponentOrganism.LastMove = OrganismMoves.Fight;
            this.Moves--;
            opponentOrganism.Moves--;
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
            Console.ForegroundColor = ConsoleColor.Red;
            base.DisplayOrganism();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}