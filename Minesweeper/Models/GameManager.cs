using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Models.DifficultyStrategy;
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

        public int ShowFreeCellBonusQuantity { get; set; }

        public int ShowBombBonusQuantity { get; set; }

        public IDifficultyStrategy DifficultyStrategy { get; set; }

        public void Initialize(int rows, int columns, string difficulty)
        {
            Field = new Field.Field();
            IsEnd = false;
            IsWin = false;
            Score = 0;
            Rows = rows;
            Columns = columns;

            switch (difficulty)
            {
                case "Expert":
                    DifficultyStrategy = new ExpertDifficultyStrategy(this);
                    break;
                case "Intermediate":
                    DifficultyStrategy = new IntermediateDifficultyStrategy(this);
                    break;
                default:
                    DifficultyStrategy = new BeginnerDifficultyStrategy(this);
                    break;
            }

            DifficultyStrategy.GenerateField();
            DifficultyStrategy.SetBonusesQuantity();
        }

        public void ActivateCell(int x, int y)
        {
            if (x < Rows && y < Columns && x >= 0 && y >= 0)
            {
                if (Field.ActiveCellsRemain == Rows * Columns - Field.BombCount) // First move
                {
                    while (Field.Cells[x, y].CellType != CellType.None)
                    {
                        DifficultyStrategy.GenerateField();
                    }
                }

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
                        DifficultyStrategy.UpdateScore(cell.CellType);
                        Field.ActiveCellsRemain--;

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

                        if (Field.ActiveCellsRemain == 0)
                        {
                            IsEnd = true;
                            IsWin = true;
                        }
                    }
                }
            }
        }

        public void ShowFreeCellBonus()
        {
            if (ShowFreeCellBonusQuantity > 0)
            {
                Score -= 500;
                if (Score < 0)
                {
                    Score = 0;
                }
                ShowFreeCellBonusQuantity--;

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (Field.Cells[i, j].CellType == CellType.None && !Field.Cells[i, j].IsActivated)
                        {
                            ActivateCell(i, j);
                            return;
                        }
                    }
                }

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (Field.Cells[i, j].CellType != CellType.Bomb && !Field.Cells[i, j].IsActivated)
                        {
                            ActivateCell(i, j);
                            return;
                        }
                    }
                }
            }
        }

        public void ShowBombBonus()
        {
            if (ShowBombBonusQuantity > 0)
            {
                Score -= 1000;
                if (Score < 0)
                {
                    Score = 0;
                }
                ShowBombBonusQuantity--;
            }
        }
    }
}
