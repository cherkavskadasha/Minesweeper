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
        protected IDifficultyStrategy(GameManager gameManager)
        {
            GameManager = gameManager;
        }

        protected GameManager GameManager;

        public abstract void GenerateField();

        public abstract void UpdateScore(CellType cellType);

        public abstract void SetBonusesQuantity();
    }
}
