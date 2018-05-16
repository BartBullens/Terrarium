using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Terrarium
{
    public class _ProgramFunctions
    {
        public int AskSizeTerrarium()
        {
            //Veranderingen min-max size terrarium mogelijk
            var minSize = 6;
            var maxSize = 50;
            var sizeTerrarium = 0;

            Console.WriteLine("Give size for the terrarium (number between {0} - {1})", minSize, maxSize);

            while (sizeTerrarium == 0)
            {
                try
                {
                    sizeTerrarium = int.Parse(Console.ReadLine());

                    if (sizeTerrarium < minSize || sizeTerrarium > maxSize)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Give an integer number between {0} - {1}", minSize, maxSize);
                    sizeTerrarium = 0;
                }
            }

            return sizeTerrarium;
        }

        // ask starting values for organisms

        public double[] AskStartingValuesOrganisms()
        {
            string[] StartingParametersTerrariumNames = { "Plants", "Herbivores", "Carnivores", "Humans" };
            string[] StartingParametersTerrarium = new string[StartingParametersTerrariumNames.Length];
            string AskParameters;

            AskParameters = "Give values for the starting number of ";
            foreach (String Parameter in StartingParametersTerrariumNames)
            {
                if (Parameter != StartingParametersTerrariumNames[StartingParametersTerrariumNames.Length - 2])
                {
                    AskParameters += Parameter + ", ";
                }
                else if (Parameter == StartingParametersTerrariumNames[StartingParametersTerrariumNames.Length - 2])
                {
                    AskParameters += Parameter + " and ";
                }
                else
                {
                    AskParameters += Parameter;
                }
            }
            Console.WriteLine(AskParameters);
            double number;
            double[] StartingValuesOrganisms = new double[StartingParametersTerrariumNames.Length];

            for (int i = 0; i < StartingParametersTerrariumNames.Length; i++)
            {
                bool ParseSuccess = double.TryParse(Console.ReadLine(), out number);
                while (ParseSuccess == false && number < 0)
                {
                    Console.WriteLine("Passed an invalid value. Give a new value.");
                    ParseSuccess = double.TryParse(Console.ReadLine(), out number);
                }
                StartingValuesOrganisms[i] = number;
            }
            return StartingValuesOrganisms;
        }

        // ask whether to show limited log, extensive log or not log

        public string AskTypeLog(Terrarium terrarium, Logboek logboek, Organism[,] arrOrganism)
        {
            string consoleInput;
            Console.WriteLine("Show simple, detailed, or no log? (\"simple\", \"detailed\", \"no\")");
            consoleInput = Console.ReadLine();

            while (consoleInput != "simple" && consoleInput != "detailed" && consoleInput != "no")
            {
                Console.WriteLine("Invalid input. Choose between \"simple\", \"detailed\", or \"no\")");
                consoleInput = Console.ReadLine();
            }
            return consoleInput;
        }

        // ask whether to turn on diasters

        //

        private int day = 1;

        public void AskNextMove(Terrarium terrarium, Organism[,] arrTerrarium, Logboek logboek, string logbookchoice)
        {
            var input = string.Empty;
            var skipDays = 100;

            Console.WriteLine("Press 'v' and ENTER to show the next day in the terrarium.");
            Console.WriteLine("Press 's' and ENTER to stop the program.");
            Console.WriteLine("Press 'f' and ENTER for fast forward {0} days.", skipDays);
            Console.WriteLine("Press 't' and ENTER for playing a timelapse of {0} days.", skipDays);

            //alles naar upper om zo later klein- en hoofdletter te lezen
            input = Console.ReadLine().ToUpper();

            //switch gebruiken om zo later makkelijker opties toe te voegen
            switch (input)
            {
                case "S":
                    Environment.Exit(0);
                    break;

                case "V":
                    day++;
                    DrawTitelWithLine(day);
                    terrarium.NextDay(ref arrTerrarium, ref logboek);
                    terrarium.Display(arrTerrarium);
                    if (logbookchoice == "simple")
                    {
                        terrarium.LogBook.Showlogboek();
                    }
                    else if (logbookchoice == "detailed")
                    {
                        Console.Write(terrarium.LogBook.ShowExtensiveLogboek(arrTerrarium));
                    }
                    else
                    {
                        // no logbook
                    }
                    break;

                case "F":
                    for (int i = 0; i < skipDays; i++)
                    {
                        day++;
                        terrarium.NextDay(ref arrTerrarium, ref logboek);
                    }
                    DrawTitelWithLine(day);
                    terrarium.Display(arrTerrarium);
                    if (logbookchoice == "simple")
                    {
                        terrarium.LogBook.Showlogboek();
                    }
                    else if (logbookchoice == "detailed")
                    {
                        Console.Write(terrarium.LogBook.ShowExtensiveLogboek(arrTerrarium));
                    }
                    else
                    {
                        // no logbook
                    }
                    break;

                case "T":
                    // periode van 1 seconden tussen elke dag
                    var milliseconds = 1000;
                    for (int i = 0; i < skipDays; i++)
                    {
                        Thread.Sleep(milliseconds);
                        Console.Clear();
                        day++;
                        terrarium.NextDay(ref arrTerrarium, ref logboek);
                        DrawTitelWithLine(day);
                        terrarium.Display(arrTerrarium);
                        if (logbookchoice == "simple")
                        {
                            terrarium.LogBook.Showlogboek();
                        }
                        else if (logbookchoice == "detailed")
                        {
                            Console.Write(terrarium.LogBook.ShowExtensiveLogboek(arrTerrarium));
                        }
                        else
                        {
                            // no logbook
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Wrong input");
                    break;
            }
        }

        public void DrawTitelWithLine(int day)
        {
            var lineDrawer = new _LineDrawer();
            var dayTitel = "DAY : " + day;

            Console.WriteLine(dayTitel);
            lineDrawer.DrawLine(dayTitel.Length, '*');
        }
    }
}