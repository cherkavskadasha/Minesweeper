using Minesweeper.Models.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.DifficultyStrategy
{
    public abstract class IDifficultyStrategy
    {
        protected IDifficultyStrategy(GameManager gameManager, int bombMinCount)
        {
            GameManager = gameManager;
            BombMinCount = bombMinCount;
        }

        protected GameManager GameManager;

        protected readonly int BombMinCount;

        public abstract void GenerateField();

        public abstract void UpdateScore(CellType cellType);
    }
}
