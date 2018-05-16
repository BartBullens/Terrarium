using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    public class VolcanicEruption : Organism
    {
        private static Random random = new Random();

        public VolcanicEruption(int xposition, int yposition, int moves) : base(xposition, yposition)
        {
            this.Sprite = "V";
            this.IsWalkable = false;
            this.Duration = random.Next(5, 11);
            this.Expansion = 40m;
            this.Moves = 1;
        }

        public int Duration
        {
            get;
            set;
        }

        public decimal Expansion
        {
            get;
            set;
        }

        public void Spread(ref Organism[,] arrOrganisms)
        {
            if (Duration > 0 && Moves > 0)
            {
                // start with listing 4 locations the volcano can spread to
                var xPosition = this.Position.xPosition;
                var yPosition = this.Position.yPosition;

                var newxPosition = xPosition;
                var newyPosition = yPosition;

                List<Organism> Neighbours = new List<Organism>();

                // generate neighbouring coordinates, but only new ones

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 1; j >= -1; j -= 2)
                    {
                        newxPosition = (xPosition + i * j) % (arrOrganisms.GetLength(0));
                        newyPosition = (yPosition + (i + j) % 2) % (arrOrganisms.GetLength(1));
                        if (newxPosition < 0)
                            newxPosition = arrOrganisms.GetLength(0) - 1;
                        if (newyPosition < 0)
                            newyPosition = arrOrganisms.GetLength(1) - 1;
                        if (!(arrOrganisms[newxPosition, newyPosition] is VolcanicEruption))
                        {
                            Neighbours.Add(arrOrganisms[newxPosition, newyPosition]);
                        }
                    }
                }

                // now create volcanoes at those spots with a certain chance (expansion value) with moves 0 and origin of the orginal volcano

                for (int i = 0; i < Neighbours.Count; i++)
                {
                    int SpreadChance = random.Next(1, 101);
                    if (SpreadChance <= Expansion)
                    {
                        arrOrganisms[Neighbours[i].Position.xPosition, Neighbours[i].Position.yPosition] = new VolcanicEruption(Neighbours[i].Position.xPosition, Neighbours[i].Position.yPosition, 0);
                    }
                }
            }
            Moves--;
            Duration--;
        }

        // disappearing of volcano

        public void Destroy(ref Organism[,] arrOrganisms)
        {
            if (Duration <= 0)
            {
                foreach (Organism anOrganism in arrOrganisms)
                    if (anOrganism is VolcanicEruption)
                    {
                        arrOrganisms[anOrganism.Position.xPosition, anOrganism.Position.yPosition] = new Terrain(anOrganism.Position.xPosition, anOrganism.Position.yPosition);
                    }
            }
        }
    }
}