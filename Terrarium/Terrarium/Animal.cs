using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    public class Animal : Organism, IBehavior, IProcreateTogether
    {
        private static Random random = new Random();

        public Animal(int xposition, int yposition) : base(xposition, yposition)
        {
            this.Life = 10;
            // random sex
            var SexChoice = random.Next(2);
            if (SexChoice == 1)
                this.Sex = Sex.Male;
            else
                this.Sex = Sex.Female;
        }

        public Animal(int xposition, int yposition, int moves) : base(xposition, yposition, moves)
        {
            this.Life = 10;
            // random sex
            var SexChoice = random.Next(2);
            if (SexChoice == 1)
                this.Sex = Sex.Male;
            else
                this.Sex = Sex.Female;
        }

        // property sex
        public Sex Sex
        {
            get;
            set;
        }

        public void Move(ref Organism[,] arrOrganism)
        {
            // start with listing 4 locations an organism can move to
            var xPosition = this.Position.xPosition;
            var yPosition = this.Position.yPosition;

            var newxPosition = xPosition;
            var newyPosition = yPosition;

            List<Organism> Neighbours = new List<Organism>();

            // generate neighbouring coordinates

            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j >= -1; j -= 2)
                {
                    newxPosition = (xPosition + i * j) % (arrOrganism.GetLength(0));
                    newyPosition = (yPosition + (i + j) % 2) % (arrOrganism.GetLength(1));
                    if (newxPosition < 0)
                        newxPosition = arrOrganism.GetLength(0) - 1;
                    if (newyPosition < 0)
                        newyPosition = arrOrganism.GetLength(1) - 1;
                    Neighbours.Add(arrOrganism[newxPosition, newyPosition]);
                }
            }

            // now check if these are valid moves

            List<Organism> ValidMoves = Neighbours.FindAll(FindIsWalkable);

            // generate random number based on how many valid moves there are

            int numberOfValidMoves = ValidMoves.Count;
            if (numberOfValidMoves != 0)
            {
                int index = random.Next(numberOfValidMoves);
                Organism ChosenMove = ValidMoves[index];
                this.MoveTo(ref arrOrganism, ChosenMove.Position.xPosition, ChosenMove.Position.yPosition);
                this.LastMove = OrganismMoves.Move;
            }
            else
            {
                this.LastMove = OrganismMoves.Nothing;
            }
            this.Moves -= 1;
        }

        // delegate findIsWalkable
        private static bool FindIsWalkable(Organism anOrganism)
        {
            if (anOrganism.IsWalkable == true)
                return true;
            else
                return false;
        }

        public void MoveTo(ref Organism[,] arrOrganisms, int xPosition, int yPosition)
        {
            // remember initial coordinates
            var terrainX = this.Position.xPosition;
            var terrainY = this.Position.yPosition;

            // take over coordinates of the opponent
            this.Position.xPosition = xPosition;
            this.Position.yPosition = yPosition;

            arrOrganisms[xPosition, yPosition] = this;

            // create terrain at old coordinates
            arrOrganisms[terrainX, terrainY] = new Terrain(terrainX, terrainY);
        }

        // IProcreateTogether method

        public void ProcreateTogether(ref Organism[,] arrOrganism, Organism rightNeighbor, ref Logboek logboek)
        {
            //lijst maken van mogelijke plaatsen om een baby te zetten
            List<Organism> listPossibleSpaces = new List<Organism>();

            for (int y = 0; y < arrOrganism.GetLength(1); y++)
            {
                for (int x = 0; x < arrOrganism.GetLength(0); x++)
                {
                    if (arrOrganism[x, y].IsWalkable == true)
                    {
                        listPossibleSpaces.Add(arrOrganism[x, y]);
                    }
                }
            }
            if (listPossibleSpaces.Count != 0)
            {
                //een random stukje grond nemen uit de mogelijke plaatsen
                var randomPlace = random.Next(0, listPossibleSpaces.Count);

                var xPosition = listPossibleSpaces[randomPlace].Position.xPosition;
                var yPosition = listPossibleSpaces[randomPlace].Position.yPosition;

                //Nieuw organism toevoegen op random locatie
                arrOrganism[xPosition, yPosition] = null;
                if (this is Herbivore)
                {
                    arrOrganism[xPosition, yPosition] = new Herbivore(xPosition, yPosition, 0, OrganismMoves.Born);
                    logboek.BornHerbivores += 1;
                    logboek.numberOfHerbivores += 1;
                }
                else if (this is Carnivore)
                {
                    arrOrganism[xPosition, yPosition] = new Carnivore(xPosition, yPosition, 0, OrganismMoves.Born);
                    logboek.BornCarnivores += 1;
                    logboek.numberOfCarnivores += 1;
                }
                else
                // is human
                {
                    arrOrganism[xPosition, yPosition] = new Human(xPosition, yPosition, 0, OrganismMoves.Born);
                    logboek.BornHumans += 1;
                    logboek.numberOfHumans += 1;
                }
            }
            //de acties van beide organism gaan naar 0
            this.Moves--;
            rightNeighbor.Moves--;
            // de laatste move van beide organism op procreate zetten
            this.LastMove = OrganismMoves.Procreate;
            rightNeighbor.LastMove = OrganismMoves.Procreate;
        }
    }
}