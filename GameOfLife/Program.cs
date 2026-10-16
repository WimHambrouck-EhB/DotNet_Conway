using System.Text;

namespace GameOfLife
{
    internal class Program
    {
        private const char ActiveCell = '#';
        private const char InactiveCell = '.';

        private const int StartDelay = 100;
        private const int MinDelay = 0;
        private const int MaxDelay = StartDelay + 100;        
        private const int DelayStep = 10;
        private static int CurrentDelay = StartDelay;
        
        private static readonly Game Game = new();
        private static readonly StringBuilder GridOutput = new();

        static void Main()
        {
            Console.CursorVisible = false;
            Console.Clear();

            PrintMenu();

            while (HandleUserInput())
            {
                PrintGrid();
                Game.UpdateGrid();

                Thread.Sleep(CurrentDelay); // Pauze voor duidelijkheid
            }

            Console.Clear();
        }

        /// <summary>
        /// Controleert of er input van de gebruiker is en handelt deze af.
        /// </summary>
        /// <returns>False als gebruiker Escape ("stop simulatie") ingeeft, anders True.</returns>
        private static bool HandleUserInput()
        {
            // Check of een toets is ingedrukt
            if (Console.KeyAvailable)
            {
                // Lees de toets in zonder deze weer te geven
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        // <ESC>: stop simulatie
                        return false;
                    case ConsoleKey.OemPlus:
                    case ConsoleKey.Add:
                        // verhoog snelheid (dus minder delay)
                        CurrentDelay = Math.Clamp(CurrentDelay - DelayStep, MinDelay, MaxDelay);
                        PrintMenu();
                        break;
                    case ConsoleKey.OemMinus:
                    case ConsoleKey.Subtract:
                        // verlaag snelheid (dus meer delay)
                        CurrentDelay = Math.Clamp(CurrentDelay + DelayStep, MinDelay, MaxDelay);
                        PrintMenu();
                        break;
                    case ConsoleKey.Enter:
                        // <ENTER>: reset snelheid
                        CurrentDelay = StartDelay;
                        PrintMenu();
                        break;
                    default:
                        break;
                }
            }

            return true;
        }

        /// <summary>
        /// Print menu onder het raster.
        /// </summary>
        private static void PrintMenu()
        {
            Console.SetCursorPosition(0, Game.Rows + 2);
            
            string speed;
            if (CurrentDelay == MaxDelay)
            {
                speed = "MIN";
            }
            else if (CurrentDelay == MinDelay)
            {
                speed = "MAX";
            } else
            {
                speed = (StartDelay - CurrentDelay).ToString();
            }

            Console.WriteLine($"Snelheid: {speed}   ");
            Console.WriteLine("[ESC] stop simulatie");
            Console.WriteLine("[+] verhoog snelheid\t[-] verlaag snelheid\t[ENTER] reset snelheid");
        }

        /// <summary>
        /// Print het huidige raster naar de console
        /// </summary>
        /// <param name="grid">Af te printen raster.</param>
        static void PrintGrid()
        {
            GridOutput.Clear();
            for (int row = 0; row < Game.Rows; row++)
            {
                for (int col = 0; col < Game.Cols; col++)
                {
                    GridOutput.Append(Game.Grid[row, col] ? ActiveCell : InactiveCell);
                }
                GridOutput.AppendLine();
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(GridOutput);
        }
    }
}