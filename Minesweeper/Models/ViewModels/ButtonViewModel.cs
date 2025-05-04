using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.ViewModels
{
    public class ButtonViewModel : INotifyPropertyChanged
    {
        public ButtonViewModel(int x, int y, int idx)
        {
            X = x;
            Y = y;
            Idx = idx;
            Clicked = false;
            Image = "../../Images/block.png";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int X {  get; set; }

        public int Y { get; set; }

        public int Idx { get; set; }

        public bool Clicked { get; set; }

        private string _image;
        public string Image
        {
            get => _image;
            set
            { 
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
