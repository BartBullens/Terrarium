using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    internal class Human : Animal, IFight
    {
        // constructors
        public Human(int xposition, int yposition) : base(xposition, yposition)
        {
            if (this.Sex == Sex.Male)
            {
                this.Sprite = "M";
            }
            else
            {
                this.Sprite = "m";
            }
            this.ID = "Human_" + Counter;
        }

        public Human(int xposition, int yposition, int moves, OrganismMoves lastmove = OrganismMoves.Nothing) : base(xposition, yposition, moves)
        {
            if (this.Sex == Sex.Male)
            {
                this.Sprite = "M";
            }
            else
            {
                this.Sprite = "m";
            }
            this.LastMove = lastmove;
            this.ID = "Human_" + Counter;
        }

        // interface method Fight
        public void Fight(ref Organism[,] arrOrganism, Organism opponentOrganism, ref Logboek logbook)
        {
            var xPosition = this.Position.xPosition;
            var yPosition = this.Position.yPosition;

            if (this.Life == opponentOrganism.Life)
            {
                // win
                this.Life += opponentOrganism.Life;

                // destroy opponent, make terrain at this location
                arrOrganism[opponentOrganism.Position.xPosition, opponentOrganism.Position.yPosition] = new Terrain(opponentOrganism.Position.xPosition, opponentOrganism.Position.yPosition);

                // move to this location
                ((Animal)this).MoveTo(ref arrOrganism, opponentOrganism.Position.xPosition, opponentOrganism.Position.yPosition);
                logbook.numberOfCarnivores--;
                logbook.DiedCarnivores++;
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

                // humans can only fight carnivores
                logbook.numberOfCarnivores--;
                logbook.DiedCarnivores++;
            }
            else
            {
                // lose, add life to opponent
                opponentOrganism.Life += this.Life;

                // destroy yourself, make terrain at this location
                arrOrganism[xPosition, yPosition] = new Terrain(xPosition, yPosition);

                // opponent moves to this location
                ((Animal)opponentOrganism).MoveTo(ref arrOrganism, xPosition, yPosition);
                logbook.numberOfHumans--;
                logbook.DiedHumans++;
            }
            // reduce moves by 1 for both organisms
            // set last move to fight for both organisms
            this.LastMove = OrganismMoves.Fight;
            opponentOrganism.LastMove = OrganismMoves.Fight;
            this.Moves--;
            opponentOrganism.Moves--;
        }
    }
}