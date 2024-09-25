using System.Data;

namespace ConsoleApp14
{
    internal class Program
    {
        const int rows = 20;
        const int cols = 40;
        static bool[,] grid = new bool[rows, cols];

        static void Main()
        {
            PrintMenu();
            InitializeGrid();
            bool doorgaan = true;
            while (doorgaan)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                            doorgaan = false;
                            break;
                        default:
                            break;
                    }
                }
                //Console.Clear();
                PrintGrid();
                UpdateGrid();
                Thread.Sleep(100); // Pauze voor duidelijkheid
            }
        }

        private static void PrintMenu()
        {
            Console.SetCursorPosition(0, rows + 2);
            Console.WriteLine("Druk op ESC om te stoppen");
        }

        // Genereer willekeurige starttoestand
        static void InitializeGrid()
        {
            Random random = new();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c] = random.Next(2) == 0;
                }
            }
        }

        // Print het huidige raster naar de console
        static void PrintGrid()
        {
            Console.SetCursorPosition(0, 0);
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Console.Write(grid[r, c] ? "#" : ".");
                }
                Console.WriteLine();
            }
        }

        // Update het raster volgens de regels van het spel
        static void UpdateGrid()
        {
            bool[,] newGrid = new bool[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int livingNeighbors = CountLivingNeighbors(r, c);
                    if (grid[r, c])
                    {
                        newGrid[r, c] = livingNeighbors == 2 || livingNeighbors == 3;
                    }
                    else
                    {
                        newGrid[r, c] = livingNeighbors == 3;
                    }
                }
            }

            grid = newGrid; // Update het raster
        }

        // Tel het aantal levende buren van een cel
        static int CountLivingNeighbors(int row, int col)
        {
            int livingNeighbors = 0;

            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    if (r == 0 && c == 0) continue; // Sla de cel zelf over

                    int neighborRow = row + r;
                    int neighborCol = col + c;

                    if (neighborRow >= 0 && neighborRow < rows && neighborCol >= 0 && neighborCol < cols)
                    {
                        if (grid[neighborRow, neighborCol]) livingNeighbors++;
                    }
                }
            }

            return livingNeighbors;
        }
    }

}