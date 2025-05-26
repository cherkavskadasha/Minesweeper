namespace Minesweeper.Models.Field
{
    public class Field
    {
        public Cell[,] Cells { get; set; }

        public int BombCount { get; set; }

        public int ActiveCellsRemain { get; set; }

        private IEnumerable<(int x, int y)> GetNeighbors(int x, int y, int rows, int cols)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;

                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx >= 0 && nx < rows && ny >= 0 && ny < cols)
                        yield return (nx, ny);
                }
            }
        }

        public void GenerateField(int rows, int columns, int bombCount)
        {
            Cell[,] cells = new Cell[rows, columns];
            bool[] bombsArr = new bool[rows * columns];
            BombCount = bombCount;
            ActiveCellsRemain = rows * columns - BombCount;

            for (int i = 0; i < bombCount; i++)
            {
                bombsArr[i] = true;
            }
            Random.Shared.Shuffle(bombsArr);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    CellType cellType;
                    if (bombsArr[i * columns + j])
                    {
                        cellType = CellType.Bomb;
                    }
                    else
                    {
                        int bombNear = 0;
                        foreach (var (nx, ny) in GetNeighbors(i, j, rows, columns))
                        {
                            if (bombsArr[nx * columns + ny])
                            {
                                bombNear++;
                            }
                        }
                        cellType = (CellType)bombNear;
                    }
                    cells[i, j] = new Cell(cellType == CellType.Bomb, cellType);
                }
            }

            Cells = cells;
        }
    }
}
