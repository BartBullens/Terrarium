using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarium
{
    internal class _Program
    {
        private static void Main(string[] args)
        {
            //object met al de functies in voor main program
            var programFunctions = new _ProgramFunctions();
            var lineDrawer = new _LineDrawer();

            //schrijven van de titel
            var title = "TERRARIUM";
            Console.WriteLine(title);
            Console.Write(lineDrawer.DrawLine(title.Length, '*'));

            //Grootte terrarium vragen met controle input
            //Hier later mogelijk om meerdere terrariums aan te maken via for lus
            //als we input vragen over aantal terrariums te maken
            var sizeTerrarium = programFunctions.AskSizeTerrarium();
            var terrarium = new Terrarium(sizeTerrarium);
            Logboek logboek = new Logboek(terrarium);
            terrarium.LogBook = logboek;

            // ask user for starting values for Organisms

            double[] StartingValuesOrganisms = programFunctions.AskStartingValuesOrganisms();

            //array aanmaken met first day values
            Console.Clear();
            var arrTerrarium = terrarium.FirstDay(StartingValuesOrganisms);
            var day = 1;

            // ask type of logbook
            string logbookChoice = programFunctions.AskTypeLog(terrarium, logboek, arrTerrarium);

            //Terrarium tonen op het scherm (first day)
            programFunctions.DrawTitelWithLine(day);
            terrarium.Display(arrTerrarium);
            if (logbookChoice == "simple")
            {
                terrarium.LogBook.Showlogboek();
            }
            else if (logbookChoice == "detailed")
            {
                Console.Write(terrarium.LogBook.ShowExtensiveLogboek(arrTerrarium));
            }
            else
            {
                // no logbook
            }

            //Terrarium tonen op het scherm (first day)

            //infinite loop as long program runs
            while (true)
            {
                programFunctions.AskNextMove(terrarium, arrTerrarium, logboek, logbookChoice);
            }
        }
    }
}