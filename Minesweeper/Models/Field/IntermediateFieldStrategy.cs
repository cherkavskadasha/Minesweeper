using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.Field
{
    public class IntermediateFieldStrategy : IFieldStrategy
    {
        public int BOMB_MIN_COUNT = 6;

        public override Cell[,] CreateField(int rows, int columns)
        {
            BombCount = rows * columns * 15 / 100; // 15%
            if (BombCount < BOMB_MIN_COUNT)
            {
                BombCount = BOMB_MIN_COUNT;
            }

            return base.CreateField(rows, columns);
        }
    }
}
