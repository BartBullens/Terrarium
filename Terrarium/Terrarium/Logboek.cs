using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    public class Logboek
    {
        public Logboek(Terrarium terrarium)
        {
            this.Terrarium = terrarium;
        }

        private string LogboekBuilder;
        private string ExtensiveLogboekBuilder;

        public Terrarium Terrarium
        {
            get;
            set;
        }

        public int numberOfPlants
        {
            get; set;
        }

        public int numberOfCarnivores
        {
            get; set;
        }

        public int numberOfHerbivores
        {
            get; set;
        }

        public int numberOfHumans
        {
            get;
            set;
        }

        public int EatenPlants
        {
            get;
            set;
        }

        public int EatenHerbivores
        {
            get; set;
        }

        public int BornHerbivores
        {
            get; set;
        }

        public int BornCarnivores
        {
            get;
            set;
        }

        public int BornHumans
        {
            get;
            set;
        }

        public int DiedCarnivores
        {
            get;
            set;
        }

        public int DiedHumans
        {
            get;
            set;
        }

        public int CreatedPlants
        {
            get;
            set;
        }

        public override string ToString()
        {
            LogboekBuilder = string.Empty;
            LogboekBuilder += $"Terrarium {Terrarium.Name} on day {Terrarium.Day}: \n";
            LogboekBuilder += $"{numberOfPlants} plants \n";
            LogboekBuilder += $"{numberOfHerbivores} herbivores \n";
            LogboekBuilder += $"{numberOfCarnivores} carnivores: \n";
            LogboekBuilder += $"{numberOfHumans} humans: \n";
            LogboekBuilder += $"added {CreatedPlants} plants \n";
            LogboekBuilder += $"{BornHerbivores} herbivores have been born \n";
            LogboekBuilder += $"{EatenPlants} plants have been eaten \n";
            LogboekBuilder += $"{EatenHerbivores} herbivores have been eaten \n";
            LogboekBuilder += $"{DiedCarnivores} carnivores have died \n";
            LogboekBuilder += $"{DiedHumans} humans have died \n";
            return LogboekBuilder;
        }

        public void Showlogboek()
        {
            Console.WriteLine(this.ToString());
            this.EatenPlants = 0;
            this.EatenHerbivores = 0;
            this.BornHerbivores = 0;
            this.DiedCarnivores = 0;
            this.DiedHumans = 0;
            this.CreatedPlants = 0;
        }

        public string ShowExtensiveLogboek(Organism[,] arrOrganisms)
        {
            int counter = 1;
            _LineDrawer LineDrawer = new _LineDrawer();
            ExtensiveLogboekBuilder = string.Empty;
            ExtensiveLogboekBuilder += $"Terrarium {Terrarium.Name} on day {Terrarium.Day}: \n";
            var TitleLength = ExtensiveLogboekBuilder.Length;
            ExtensiveLogboekBuilder += $"{LineDrawer.DrawLine(TitleLength, '*')}{LineDrawer.DrawLine(TitleLength, '*')}";

            // for each organism, list its properties and actions
            // this way is not very good, you need to instead go through the array once, and collect all the information in a list or something. Then you go through the list and show it
            // for each group of organisms

            // plants

            ExtensiveLogboekBuilder += $"Plants:\nCurrent: {numberOfPlants}\tAdded: {CreatedPlants}\tEaten: {EatenPlants}\n{LineDrawer.DrawLine(TitleLength, '*')}";
            ExtensiveLogboekBuilder += "counter\tID\t\tPosition\tLife\n";

            foreach (Organism anOrganism in arrOrganisms)
            {
                if (anOrganism is Plant)
                {
                    ExtensiveLogboekBuilder += $"{counter}\t{anOrganism.ID}\t{anOrganism.Position}\t{anOrganism.Life}\n"; /* How will this work, will it call the ToString() method of the coordinate class? Will we get both x and y positions?
                    It calls the ToString() method of the coordinate class yes!*/
                    counter++;
                }
            }
            ExtensiveLogboekBuilder += $"{LineDrawer.DrawLine(TitleLength, '*')}";
            counter = 1;
            // herbivores

            ExtensiveLogboekBuilder += $"Herbivores\nCurrent: {numberOfHerbivores}\tAdded: {BornHerbivores}\tEaten: {EatenHerbivores}\n{LineDrawer.DrawLine(TitleLength, '*')}";
            ExtensiveLogboekBuilder += "counter\tID\t\tPosition\tLife\tLast Move\n";

            foreach (Organism anOrganism in arrOrganisms)
            {
                if (anOrganism is Herbivore)
                {
                    ExtensiveLogboekBuilder += $"{counter}\t{anOrganism.ID}\t{anOrganism.Position}\t{anOrganism.Life}\t{anOrganism.LastMove}\n";
                    counter++;
                }
            }
            ExtensiveLogboekBuilder += $"{LineDrawer.DrawLine(TitleLength, '*')}";
            counter = 1;
            // Carnivores

            ExtensiveLogboekBuilder += $"Carnivores\nCurrent: {numberOfCarnivores}\tAdded: 0\tDied: {DiedCarnivores}\n{LineDrawer.DrawLine(TitleLength, '*')}";
            ExtensiveLogboekBuilder += "counter\tID\t\tPosition\tLife\tLast Move\n";

            foreach (Organism anOrganism in arrOrganisms)
            {
                if (anOrganism is Carnivore)
                {
                    ExtensiveLogboekBuilder += $"{counter}\t{anOrganism.ID}\t{anOrganism.Position}\t{anOrganism.Life}\t{anOrganism.LastMove}\n";
                    counter++;
                }
            }
            ExtensiveLogboekBuilder += $"{LineDrawer.DrawLine(TitleLength, '*')}{LineDrawer.DrawLine(TitleLength, '*')}";
            counter = 1;

            // humans

            ExtensiveLogboekBuilder += $"Humans\nCurrent: {numberOfHumans}\tAdded: 0\tDied: {DiedHumans}\n{LineDrawer.DrawLine(TitleLength, '*')}";
            ExtensiveLogboekBuilder += "counter\tID\t\tPosition\tLife\tLast Move\n";

            foreach (Organism anOrganism in arrOrganisms)
            {
                if (anOrganism is Human)
                {
                    ExtensiveLogboekBuilder += $"{counter}\t{anOrganism.ID}\t{anOrganism.Position}\t{anOrganism.Life}\t{anOrganism.LastMove}\n";
                    counter++;
                }
            }
            ExtensiveLogboekBuilder += $"{LineDrawer.DrawLine(TitleLength, '*')}{LineDrawer.DrawLine(TitleLength, '*')}";
            counter = 1;
            return ExtensiveLogboekBuilder;
        }
    }
}