using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.ViewModels
{
    public class ButtonViewModel
    {
        public int X {  get; set; }

        public int Y { get; set; }

        public int Idx { get; set; }

        public bool Clicked { get; set; }

        public ButtonViewModel(int x, int y, int idx)
        {
            X = x;
            Y = y;
            Idx = idx;
            Clicked = false;
        }
    }
}
