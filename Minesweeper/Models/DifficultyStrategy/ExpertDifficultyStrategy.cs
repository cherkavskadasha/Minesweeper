using Minesweeper.Models.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.DifficultyStrategy
{
    public class ExpertDifficultyStrategy : IDifficultyStrategy
    {
        public ExpertDifficultyStrategy(GameManager gameManager) : base(gameManager, BOMB_MIN_COUNT) { }

        private const int BOMB_MIN_COUNT = 10;

        public override void GenerateField()
        {
            int rows = GameManager.Rows;
            int columns = GameManager.Columns;
            int bombCount = rows * columns / 5; // 20%

            if (bombCount < BOMB_MIN_COUNT)
            {
                bombCount = BOMB_MIN_COUNT;
            }

            GameManager.Field.GenerateField(rows, columns, bombCount);
        }

        public override void UpdateScore(CellType cellType)
        {
            int score = (int)cellType * 100 + 100;

            GameManager.Score += score;
        }
    }
}
