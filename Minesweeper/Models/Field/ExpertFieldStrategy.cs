using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.Field
{
    public class ExpertFieldStrategy : IFieldStrategy
    {
        public int BOMB_MIN_COUNT = 10;

        public override Cell[,] CreateField(int rows, int columns)
        {
            BombCount = rows * columns / 5; // 20%
            if (BombCount < BOMB_MIN_COUNT)
            {
                BombCount = BOMB_MIN_COUNT;
            }

            return base.CreateField(rows, columns);
        }
    }
}
