using System.Data;

namespace GameOfLife
{
    internal class Program
    {
        private const int startDelay = 100;
        private const string activeCell = "#";
        private const string inactiveCell = ".";

        private static readonly Game game = new();
        private static int delay = startDelay;

        static void Main()
        {
            PrintMenu();

            while (HandleUserInput())
            {
                PrintGrid(game.Grid);
                game.UpdateGrid();

                Thread.Sleep(delay); // Pauze voor duidelijkheid
            }
        }

        private static bool HandleUserInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        Console.Clear();
                        return false;
                    case ConsoleKey.OemPlus:
                    case ConsoleKey.Add:
                        delay -= 10;
                        if (delay < 0)
                            delay = 0;
                        PrintMenu();
                        break;
                    case ConsoleKey.OemMinus:
                    case ConsoleKey.Subtract:
                        delay += 10;
                        if (delay > startDelay + 100)
                            delay = startDelay + 100;
                        PrintMenu();
                        break;
                    case ConsoleKey.Enter:
                        delay = startDelay;
                        PrintMenu();
                        break;
                    default:
                        break;
                }
            }

            return true;
        }

        private static void PrintMenu()
        {
            Console.SetCursorPosition(0, game.Rows + 2);
            Console.WriteLine($"Snelheid: {100 - delay}   ");
            Console.WriteLine("[ESC] stop simulatie");
            Console.WriteLine("[+] verhoog snelheid\t[-] verlaag snelheid\t[ENTER] reset snelheid");
        }

        // Print het huidige raster naar de console
        static void PrintGrid(bool[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            Console.SetCursorPosition(0, 0);
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Console.Write(grid[r, c] ? activeCell : inactiveCell);
                }
                Console.WriteLine();
            }
        }
    }
}