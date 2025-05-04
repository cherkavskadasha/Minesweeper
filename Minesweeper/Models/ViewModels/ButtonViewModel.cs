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

        public bool Clicked { get; set; }

        public ButtonViewModel(int x, int y)
        {
            X = x;
            Y = y;
            Clicked = false;
        }
    }
}
