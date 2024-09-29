using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Game
    {
        public int Rows { get; } = 20;
        public int Cols { get; } = 40;

        public bool[,] Grid { get; private set; }

        public Game()
        {
            Grid = new bool[Rows, Cols];
            InitializeGrid();
        }

        /// <summary>
        /// Genereer willekeurige starttoestand.
        /// </summary>
        private void InitializeGrid()
        {
            Random random = new();
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    Grid[r, c] = random.Next(2) == 0;
                }
            }
        }

        public void UpdateGrid()
        {
            bool[,] newGrid = new bool[Rows, Cols];

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    int livingNeighbors = CountLivingNeighbors(r, c);
                    if (Grid[r, c])
                    {
                        newGrid[r, c] = livingNeighbors == 2 || livingNeighbors == 3;
                    }
                    else
                    {
                        newGrid[r, c] = livingNeighbors == 3;
                    }
                }
            }

            Grid = newGrid; // Update het raster
        }

        // Tel het aantal levende buren van een cel
        private int CountLivingNeighbors(int row, int col)
        {
            int livingNeighbors = 0;

            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    if (r == 0 && c == 0) continue; // Sla de cel zelf over

                    int neighborRow = row + r;
                    int neighborCol = col + c;

                    if (neighborRow >= 0 && neighborRow < Rows && neighborCol >= 0 && neighborCol < Cols)
                    {
                        if (Grid[neighborRow, neighborCol]) livingNeighbors++;
                    }
                }
            }

            return livingNeighbors;
        }
    }
}
