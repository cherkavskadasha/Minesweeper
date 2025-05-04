using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.Field
{
    public abstract class IFieldStrategy
    {
        public int BombCount { get; set; }

        public virtual Cell[,] CreateField(int rows, int columns)
        {
            Cell[,] cells = new Cell[rows, columns];
            bool[] bombsArr = new bool[rows * columns];
            for (int i = 0; i < BombCount; i++)
            {
                bombsArr[i] = true;
            }
            Random.Shared.Shuffle(bombsArr);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    CellType cellType;
                    if (bombsArr[i * rows + j])
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
                                if ((x != 0 && y != 0) && (i + x) * rows + (j + y) >= 0 && bombsArr[(i + x) * rows + (j + y)])
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

            return cells;
        }
    }
}
