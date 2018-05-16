using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    public class Terrarium
    {
        public Terrarium(int nSize, string name = "Herbivore Park", bool VolcanicEruption = true)
        {
            this.Size = nSize;
            this.Name = name;
            this.Day = 1;
            this.LogBook = new Logboek(this);
            this.VolcanicEruptionTurnedOn = VolcanicEruption;
        }

        // array size

        public int Size
        {
            get;
            set;
        }

        // track day
        // readonly property

        public int Day
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public Logboek LogBook
        {
            get;
            set;
        }

        public bool VolcanicEruptionTurnedOn
        {
            get;
            set;
        }

        //Display method, show the terrarium array(after each day)
        // bad, we're not displaying organisms based on coordinates, but instead just the array

        public void Display(Organism[,] organism)
        {
            for (int y = 0; y < organism.GetLength(1); y++)
            {
                for (int x = 0; x < organism.GetLength(0); x++)
                {
                    organism[x, y].DisplayOrganism();
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        // constructor only or constructor => first day?
        // FirstDay method
        public Organism[,] FirstDay(double[] StartingValuesOrganisms)
        {
            // create the array
            Organism[,] arrOrganisms = new Organism[Size, Size];

            // adhere to percentages of plants/animals: 10% Carnivores, 10% Humans, 20% Herbivores, 30% plants
            // hardcoded, ask values from user
            var percentageCarnivores = StartingValuesOrganisms[2]/*10m*/;
            var percentageHerbivores = StartingValuesOrganisms[1]/*20m*/;
            var percentagePlants = StartingValuesOrganisms[0]/*30m*/;
            var percentageHumans = StartingValuesOrganisms[3]/*10m*/;

            var numberGridpoints = Size * Size;

            // numberOfPlants + numberOfCarnivores + numberOfHerbivores (+ numberOfTerrain) <= numberGridPoints
            var numberOfPlants = Math.Round((percentagePlants * numberGridpoints / 100));
            var numberOfCarnivores = Math.Round((percentageCarnivores * numberGridpoints / 100));
            var numberOfHerbivores = Math.Round((percentageHerbivores * numberGridpoints / 100));
            var numberOfHumans = Math.Round((percentageHumans * numberGridpoints / 100));

            this.LogBook.numberOfPlants = (int)numberOfPlants;
            this.LogBook.numberOfCarnivores = (int)numberOfCarnivores;
            this.LogBook.numberOfHerbivores = (int)numberOfHerbivores;
            this.LogBook.numberOfHumans = (int)numberOfHumans;

            var sum = numberOfPlants + numberOfHerbivores + numberOfCarnivores + numberOfHumans;
            while (sum > numberGridpoints)
            {
                numberOfPlants -= 1;
            }
            int counter = 1;

            // add plants
            Random random = new Random();
            while (counter <= numberOfPlants)
            {
                // create random coordinates
                // check if coordinate is empty

                var xCoordinate = random.Next(Size);
                var yCoordinate = random.Next(Size);

                while (arrOrganisms[xCoordinate, yCoordinate] != null)
                {
                    xCoordinate = random.Next(Size);
                    yCoordinate = random.Next(Size);
                }
                // fill the array
                arrOrganisms[xCoordinate, yCoordinate] = new Plant(xCoordinate, yCoordinate);
                counter += 1;
            }

            // add carnivores

            counter = 1;
            while (counter <= numberOfCarnivores)
            {
                // create random coordinates
                // check if coordinate is empty

                var xCoordinate = random.Next(Size);
                var yCoordinate = random.Next(Size);

                while (arrOrganisms[xCoordinate, yCoordinate] != null)
                {
                    xCoordinate = random.Next(Size);
                    yCoordinate = random.Next(Size);
                }
                // fill the array
                arrOrganisms[xCoordinate, yCoordinate] = new Carnivore(xCoordinate, yCoordinate);
                counter += 1;
            }

            // add herbivores

            counter = 1;
            while (counter <= numberOfHerbivores)
            {
                // create random coordinates
                // check if coordinate is empty

                var xCoordinate = random.Next(Size);
                var yCoordinate = random.Next(Size);

                while (arrOrganisms[xCoordinate, yCoordinate] != null)
                {
                    xCoordinate = random.Next(Size);
                    yCoordinate = random.Next(Size);
                }
                // fill the array
                arrOrganisms[xCoordinate, yCoordinate] = new Herbivore(xCoordinate, yCoordinate);
                counter += 1;
            }

            // add humans

            counter = 1;
            while (counter <= numberOfHumans)
            {
                // create random coordinate
                // check if coordinate is empty

                var xCoordinate = random.Next(Size);
                var yCoordinate = random.Next(Size);

                while (arrOrganisms[xCoordinate, yCoordinate] != null)
                {
                    xCoordinate = random.Next(Size);
                    yCoordinate = random.Next(Size);
                }
                // fill the array
                arrOrganisms[xCoordinate, yCoordinate] = new Human(xCoordinate, yCoordinate);
                counter += 1;
            }

            // add terrain

            for (int y = 0; y < arrOrganisms.GetLength(1); y++)
            {
                for (int x = 0; x < arrOrganisms.GetLength(0); x++)
                {
                    if (arrOrganisms[x, y] == null)
                        arrOrganisms[x, y] = new Terrain(x, y);
                }
            }
            return arrOrganisms;
        }

        // hardcoded chance of disaster
        private decimal DisasterChance = 10m;

        private static Random random = new Random();

        public Organism[,] NextDay(ref Organism[,] arrOrganisms, ref Logboek logboek)
        {
            // roll for disaster if turned on
            if (VolcanicEruptionTurnedOn == true)
            {
                int DisasterRoll = random.Next(100);
                if (DisasterRoll <= DisasterChance)
                {
                    int xPosition = random.Next(arrOrganisms.GetLength(0));
                    int yPosition = random.Next(arrOrganisms.GetLength(1));
                    arrOrganisms[xPosition, yPosition] = new VolcanicEruption(xPosition, yPosition, 1);
                    // voor meerdere malen te kunnen laten gebeuren
                    // VolcanicEruptionTurnedOn = false;
                }
            }

            // go through array row per row
            // first add plants
            for (int y = 0; y < arrOrganisms.GetLength(1); y++)
            {
                for (int x = 0; x < arrOrganisms.GetLength(0); x++)
                {
                    // which organism?
                    var currentOrganism = arrOrganisms[x, y];
                    // use switch on currentorganism, not possible, can't be done on an object
                    // == also can't be used? what is the difference with "is"
                    if (currentOrganism is Terrain)
                    {
                        ((Terrain)currentOrganism).ProcreateAlone(ref arrOrganisms, ref logboek);
                        currentOrganism.LastMove = OrganismMoves.Procreate;
                    }
                }
            }

            // go through array row per row
            // now check for herbivores, carnivores and humans
            //// take out check last column + double moves
            for (int y = 0; y < arrOrganisms.GetLength(1); y++)
            {
                for (int x = 0; x < arrOrganisms.GetLength(0); x++)
                {
                    var currentOrganism = arrOrganisms[x, y];

                    // check right neighbour
                    // if part of last column, check neighbours in first column
                    Organism rightNeighbourOrganism;

                    if ((x == (arrOrganisms.GetLength(0) - 1)))
                    {
                        rightNeighbourOrganism = arrOrganisms[0, y];
                    }
                    else
                    {
                        rightNeighbourOrganism = arrOrganisms[x + 1, y];
                    }

                    if (currentOrganism is Herbivore && currentOrganism.Moves != 0)
                    {
                        // different action based on type of neighbour
                        if (rightNeighbourOrganism is Carnivore || rightNeighbourOrganism is Human || rightNeighbourOrganism is Terrain || rightNeighbourOrganism is VolcanicEruption)
                        {
                            ((Animal)currentOrganism).Move(ref arrOrganisms);
                        }
                        else if (rightNeighbourOrganism is Plant)
                        {
                            ((Herbivore)currentOrganism).Eat(ref arrOrganisms, rightNeighbourOrganism);
                            logboek.EatenPlants += 1;
                            logboek.numberOfPlants -= 1;
                        }
                        else /*rightNeighbourOrganism is Herbivore*/
                        {
                            // different sex => procreate
                            if (((Herbivore)currentOrganism).Sex != ((Herbivore)rightNeighbourOrganism).Sex)
                            {
                                ((Herbivore)currentOrganism).ProcreateTogether(ref arrOrganisms, rightNeighbourOrganism, ref logboek);
                            }
                            else
                            // same sex => move
                            {
                                ((Animal)currentOrganism).Move(ref arrOrganisms);
                            }
                        }
                    }
                    else if (currentOrganism is Carnivore && currentOrganism.Moves != 0)
                    {
                        // different action based on type of neighbour
                        if (rightNeighbourOrganism is Carnivore || rightNeighbourOrganism is Human)
                        {
                            if (rightNeighbourOrganism is Human)
                            {
                                ((Carnivore)currentOrganism).Fight(ref arrOrganisms, rightNeighbourOrganism, ref logboek);
                            }
                            else
                            // rightneighboorOrganism is Carnivore
                            {
                                // different sex => procreate
                                if (((Carnivore)currentOrganism).Sex != ((Carnivore)rightNeighbourOrganism).Sex)
                                {
                                    ((Carnivore)currentOrganism).ProcreateTogether(ref arrOrganisms, rightNeighbourOrganism, ref logboek);
                                }
                                else
                                // same sex => fight
                                {
                                    ((Carnivore)currentOrganism).Fight(ref arrOrganisms, rightNeighbourOrganism, ref logboek);
                                }
                            }
                        }
                        else if (rightNeighbourOrganism is Plant || rightNeighbourOrganism is Terrain || rightNeighbourOrganism is VolcanicEruption)
                        {
                            ((Animal)currentOrganism).Move(ref arrOrganisms);
                        }
                        else /*(rightNeighbourOrganism is Herbivore)*/
                        {
                            ((Carnivore)currentOrganism).Eat(ref arrOrganisms, rightNeighbourOrganism);
                            logboek.EatenHerbivores += 1;
                            logboek.numberOfHerbivores -= 1;
                        }
                    }
                    else if (currentOrganism is Human && currentOrganism.Moves != 0)
                    {
                        // different action based on type of neighbour
                        if (rightNeighbourOrganism is Carnivore)
                        {
                            ((Human)currentOrganism).Fight(ref arrOrganisms, rightNeighbourOrganism, ref logboek);
                        }
                        else /*(rightNeighbourOrganism is Human || rightNeighbourOrganism is Plant || rightNeighbourOrganism is Herbivore || rightNeighbourOrganism is Terrain)*/
                        {
                            // if human of different sex => procreate
                            if (rightNeighbourOrganism is Human && ((Human)currentOrganism).Sex != ((Human)rightNeighbourOrganism).Sex)
                            {
                                ((Human)currentOrganism).ProcreateTogether(ref arrOrganisms, rightNeighbourOrganism, ref logboek);
                            }
                            else
                            // if human of same sex, or plant, herbivore or terrain
                            {
                                ((Human)currentOrganism).Move(ref arrOrganisms);
                            }
                        }
                    }
                    else if (currentOrganism is VolcanicEruption)
                    {
                        ((VolcanicEruption)currentOrganism).Spread(ref arrOrganisms);
                        ((VolcanicEruption)currentOrganism).Destroy(ref arrOrganisms);
                    }
                    else
                    {
                        // do nothing for plants and terrain
                    }
                }
            }

            foreach (Organism organism in arrOrganisms)
            {
                organism.Moves = 1;
            }

            // reduce life points of each animal with one
            // if life points is zero, kill it, and replace with ground

            foreach (Organism animal in arrOrganisms)
            {
                if (animal is Animal)
                {
                    animal.Life--;
                    if (animal.Life == 0)
                    {
                        if (animal is Carnivore)
                        {
                            logboek.numberOfCarnivores--;
                            logboek.DiedCarnivores++;
                        }
                        if (animal is Herbivore)
                        {
                            logboek.numberOfHerbivores--;
                        }
                        if (animal is Human)
                        {
                            logboek.numberOfHumans--;
                        }

                        arrOrganisms[animal.Position.xPosition, animal.Position.yPosition] = null;
                        arrOrganisms[animal.Position.xPosition, animal.Position.yPosition] =
                            new Terrain(animal.Position.xPosition, animal.Position.yPosition);
                    }
                }
            }
            this.Day++;
            return arrOrganisms;
        }
    }
}