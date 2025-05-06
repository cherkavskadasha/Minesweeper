using Minesweeper.Models.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.DifficultyStrategy
{
    public class BeginnerDifficultyStrategy : IDifficultyStrategy
    {
        public BeginnerDifficultyStrategy(GameManager gameManager) : base(gameManager, BOMB_MIN_COUNT) { }

        private const int BOMB_MIN_COUNT = 3;

        public override void GenerateField()
        {
            int rows = GameManager.Rows;
            int columns = GameManager.Columns;
            int bombCount = rows * columns / 10; // 10%

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
                    score = 50;
                    break;
                case CellType.Five:
                case CellType.Six:
                    score = 200;
                    break;
                case CellType.Eight:
                case CellType.Seven:
                    score = 300;
                    break;
                default:
                    score = 100;
                    break;
            }

            GameManager.Score += score;
        }
    }
}
