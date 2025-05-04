using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.Field
{
    public class BeginnerFieldStrategy : IFieldStrategy
    {
        public int BOMB_MIN_COUNT = 3;

        public override Cell[,] CreateField(int rows, int columns)
        {
            BombCount = rows * columns / 10; // 10%
            if (BombCount < BOMB_MIN_COUNT)
            {
                BombCount = BOMB_MIN_COUNT;
            }

            return base.CreateField(rows, columns);
        }
    }
}
