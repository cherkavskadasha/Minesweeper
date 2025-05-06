using Minesweeper.Models.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.DifficultyStrategy
{
    public class IntermediateDifficultyStrategy : IDifficultyStrategy
    {
        public IntermediateDifficultyStrategy(GameManager gameManager) : base(gameManager) { }

        private const int BOMB_MIN_COUNT = 6;

        public override void GenerateField()
        {
            int rows = GameManager.Rows;
            int columns = GameManager.Columns;
            int bombCount = rows * columns * 15 / 100; // 15%

            if (bombCount < BOMB_MIN_COUNT)
            {
                bombCount = BOMB_MIN_COUNT;
            }

            GameManager.Field.GenerateField(rows, columns, bombCount);
        }

        public override void UpdateScore(CellType cellType)
        {
            int score = 0;
            switch (cellType)
            {
                case CellType.None:
                    score = 100;
                    break;
                case CellType.One:
                case CellType.Two:
                    score = 200;
                    break;
                case CellType.Five:
                case CellType.Six:
                    score = 400;
                    break;
                case CellType.Eight:
                case CellType.Seven:
                    score = 500;
                    break;
                default:
                    score = 300;
                    break;
            }

            GameManager.Score += score;
        }

        public override void SetBonusesQuantity()
        {
            GameManager.ShowFreeCellBonusQuantity = 1;
            GameManager.ShowBombBonusQuantity = 1;
            GameManager.SafeClickBonusQuantity = 1;
        }
    }
}
