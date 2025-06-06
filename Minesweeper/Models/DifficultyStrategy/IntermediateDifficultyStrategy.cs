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

        private static readonly Dictionary<CellType, int> ScoreMap = new Dictionary<CellType, int>
        {
            { CellType.None, 100 },
            { CellType.One, 200 },
            { CellType.Two, 200 },
            { CellType.Three, 300 },
            { CellType.Four, 300 },
            { CellType.Five, 400 },
            { CellType.Six, 400 },
            { CellType.Seven, 500 },
            { CellType.Eight, 500 }
        };

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
            int score = ScoreMap.TryGetValue(cellType, out int value) ? value : 300;
            GameManager.GameState.AddToScore(score);
        }

        public override void SetBonusesQuantity()
        {
            GameManager.ShowFreeCellBonusQuantity = 1;
            GameManager.ShowBombBonusQuantity = 1;
            GameManager.SafeClickBonusQuantity = 1;
        }
    }
}