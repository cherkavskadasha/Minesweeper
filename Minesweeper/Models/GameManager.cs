using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Models.Field;

namespace Minesweeper.Models
{
    public class GameManager
    {
        public Field.Field Field { get; set; }

        public int Rows { get; set; }

        public int Score { get; set; }

        public int Columns { get; set; }

        public bool IsEnd { get; set; }

        public bool IsWin { get; set; }

        public void Initialize(int rows, int columns, string difficulty)
        {
            Field = new Field.Field(rows, columns, difficulty);
            IsEnd = false;
            IsWin = false;
            Score = 0;
            Rows = rows;
            Columns = columns;
        }

        public void ActivateCell(int x, int y)
        {
            if (x < Rows && y < Columns && x >= 0 && y >= 0)
            {
                Cell cell = Field.Cells[x, y];
                if (!cell.IsActivated)
                {
                    cell.Activate();
                    if (cell.IsBomb)
                    {
                        IsEnd = true;
                    }
                    else
                    {
                        if (cell.CellType == CellType.None)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                for (int j = -1; j <= 1; j++)
                                {
                                    if ((i != 0 || j != 0) && i + x >= 0 && j + y >= 0 && i + x < Rows && j + y < Columns)
                                    {
                                        ActivateCell(i + x, j + y);
                                    }
                                }
                            }
                        }

                        Score += 100;
                        Field.ActiveCellsRemain--;
                        if (Field.ActiveCellsRemain == 0)
                        {
                            IsEnd = true;
                            IsWin = true;
                        }
                    }
                }
            }
        }
    }
}
