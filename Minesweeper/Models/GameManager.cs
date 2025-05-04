using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Models.Field;

namespace Minesweeper.Models
{
    public class GameManager
    {
        public Field.Field Field { get; set; }

        public int Rows { get; set; }

        public int Score { get; set; }

        public int Columns { get; set; }

        public bool IsEnd { get; set; }

        public bool IsWin { get; set; }

        public void Initialize(int rows, int columns, string difficulty)
        {
            Field = new Field.Field(rows, columns, difficulty);
            IsEnd = false;
            IsWin = false;
        }
    }
}
