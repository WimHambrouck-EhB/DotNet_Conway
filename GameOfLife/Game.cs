namespace GameOfLife
{
    internal class Game
    {
        public int Rows { get; }
        public int Cols { get; }

        public bool[,] Grid { get; private set; }

        /// <summary>
        /// Initialiseert een nieuw spel met raster van 20x50.
        /// </summary>
        public Game() : this(20, 50)
        {

        }

        /// <summary>
        /// Initialiseert een nieuw spel met opgegeven aantal rijen en kolommen.
        /// </summary>
        /// <param name="rows">Aantal rijen in het raster.</param>
        /// <param name="cols">Aantal kolommen in het raster.</param>
        public Game(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new bool[Rows, Cols];
            InitializeRandomGrid();
        }

        /// <summary>
        /// Genereer willekeurige starttoestand.
        /// </summary>
        private void InitializeRandomGrid()
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
            // nieuw raster voor de volgende generatie
            // merk op: bool is standaard false, dus alle cellen zijn initieel 'dood'
            bool[,] newGrid = new bool[Rows, Cols];

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    // op basis van huidige raster het nieuwe raster updaten
                    int livingNeighbors = CountLivingNeighbors(r, c);
                    if (Grid[r, c]) // cel is momenteel levend
                    {
                        // 2 of 3 levende buren => cel blijft leven
                        newGrid[r, c] = livingNeighbors == 2 || livingNeighbors == 3;
                    }
                    else // cel is momenteel dood
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

            // deze lus kan geparalelliseerd worden (zie Parrallel.For)
            // dit komt in later lessen aan bod
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
