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
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Grid[row, col] = random.Next(2) == 0;
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

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    // op basis van huidige raster het nieuwe raster updaten
                    int livingNeighbors = CountLivingNeighbors(row, col);
                    if (Grid[row, col]) // cel is momenteel levend
                    {
                        // 2 of 3 levende buren => cel blijft leven
                        newGrid[row, col] = livingNeighbors == 2 || livingNeighbors == 3;
                    }
                    else // cel is momenteel dood
                    {
                        // 3 levende buren => cel wordt levend
                        newGrid[row, col] = livingNeighbors == 3;
                    }
                }
            }

            Grid = newGrid; // vervang het huidige raster
        }

        /// <summary>
        /// Telt het aantal levende buren van een cel.
        /// </summary>
        /// <param name="cellRow">Rij waar de cel zich bevindt.</param>
        /// <param name="cellCol">Kolom waar de cel zich bevindt.</param>
        /// <returns></returns>
        private int CountLivingNeighbors(int cellRow, int cellCol)
        {
            int count = 0;
            int rowStart = Math.Max(cellRow - 1, 0);
            int rowEnd = Math.Min(cellRow + 1, Rows - 1);
            int colStart = Math.Max(cellCol - 1, 0);
            int colEnd = Math.Min(cellCol + 1, Cols - 1);

            for (int row = rowStart; row <= rowEnd; row++)
            {
                for (int col = colStart; col <= colEnd; col++)
                {
                    if (Grid[row, col]) count++;
                }
            }

            if (Grid[cellRow, cellCol]) count--; // gevraagde cel zelf niet meetellen
            return count;
        }
    }
}
