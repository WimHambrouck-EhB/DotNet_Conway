using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        /// <summary>
        /// Update het raster volgens de regels van het spel:
        ///     Any live cell with fewer than two live neighbours dies, as if by underpopulation.
        ///     Any live cell with two or three live neighbours lives on to the next generation.
        ///     Any live cell with more than three live neighbours dies, as if by overpopulation.
        ///     Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
        /// </summary>
        public void UpdateGrid()
        {
            bool[,] newGrid = new bool[Rows, Cols];

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    int livingNeighbors = CountLivingNeighbors(r, c);
                    if (Grid[r, c]) // cel is levend
                    {
                        // 2 of 3 levende buren => cel blijft leven
                        newGrid[r, c] = livingNeighbors == 2 || livingNeighbors == 3;
                    }
                    else // cel is dood
                    {
                        // 3 levende buren => cel wordt levend
                        newGrid[r, c] = livingNeighbors == 3;
                    }
                }
            }

            Grid = newGrid; // update het raster
        }

        /// <summary>
        /// Telt het aantal levende buren van een cel.
        /// </summary>
        /// <param name="row">Rij waar de cel zich bevindt.</param>
        /// <param name="col">Kolom waar de cel zich bevindt.</param>
        /// <returns></returns>
        private int CountLivingNeighbors(int row, int col)
        {
            int livingNeighbors = 0;

            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    if (r == 0 && c == 0) continue; // cel zelf wordt niet meegeteld

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
