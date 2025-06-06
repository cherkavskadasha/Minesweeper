using Minesweeper.Models.Field;

namespace Minesweeper.Models.DifficultyStrategy
{
    public class BeginnerDifficultyStrategy : IDifficultyStrategy
    {
        public BeginnerDifficultyStrategy(GameManager gameManager) : base(gameManager) { }

        private const int BOMB_MIN_COUNT = 3;

        private static readonly Dictionary<CellType, int> ScoreMap = new Dictionary<CellType, int>
        {
            { CellType.None, 50 },
            { CellType.One, 100 },
            { CellType.Two, 100 },
            { CellType.Three, 100 },
            { CellType.Four, 100 },
            { CellType.Five, 200 },
            { CellType.Six, 200 },
            { CellType.Seven, 300 },
            { CellType.Eight, 300 }
        };

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
            int score = ScoreMap.TryGetValue(cellType, out int value) ? value : 100;
            GameManager.GameState.AddToScore(score);
        }

        public override void SetBonusesQuantity()
        {
            GameManager.ShowFreeCellBonusQuantity = 3;
            GameManager.ShowBombBonusQuantity = 2;
            GameManager.SafeClickBonusQuantity = 1;
        }
    }
}