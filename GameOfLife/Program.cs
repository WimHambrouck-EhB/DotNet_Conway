namespace GameOfLife
{
    internal class Program
    {
        private const int StartDelay = 100;
        private const string ActiveCell = "#";
        private const string InactiveCell = ".";

        private static readonly Game Game = new();
        private static int Delay = StartDelay;

        static void Main()
        {
            PrintMenu();

            while (HandleUserInput())
            {
                PrintGrid();
                Game.UpdateGrid();

                Thread.Sleep(Delay); // Pauze voor duidelijkheid
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
                        // '+': verhoog snelheid (dus minder delay)
                        if (Delay - 10 >= 0)
                            Delay -= 10;
                        PrintMenu();
                        break;
                    case ConsoleKey.OemMinus:
                    case ConsoleKey.Subtract:
                        // '-': verlaag snelheid (dus meer delay)
                        if (Delay + 10 <= StartDelay + 100)
                            Delay += 10;
                        PrintMenu();
                        break;
                    case ConsoleKey.Enter:
                        // <ENTER>: reset snelheid
                        Delay = StartDelay;
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
            Console.WriteLine($"Snelheid: {100 - Delay}   ");
            Console.WriteLine("[ESC] stop simulatie");
            Console.WriteLine("[+] verhoog snelheid\t[-] verlaag snelheid\t[ENTER] reset snelheid");
        }

        /// <summary>
        /// Print het huidige raster naar de console
        /// </summary>
        /// <param name="grid">Af te printen raster.</param>
        static void PrintGrid()
        {
            Console.SetCursorPosition(0, 0);
            for (int r = 0; r < Game.Rows; r++)
            {
                for (int c = 0; c < Game.Cols; c++)
                {
                    Console.Write(Game.Grid[r, c] ? ActiveCell : InactiveCell);
                }
                Console.WriteLine();
            }
        }
    }
}