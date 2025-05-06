namespace Minesweeper.Models.Field
{
    public class Field
    {
        public Cell[,] Cells { get; set; }

        public int BombCount { get; set; }

        public int ActiveCellsRemain { get; set; }

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
                        for (int x = -1; x <= 1; x++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                if ((x != 0 || y != 0) && i + x >= 0 && j + y >= 0 && i + x < rows && j + y < columns && bombsArr[(i + x) * columns + (j + y)])
                                {
                                    bombNear++;
                                }
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
