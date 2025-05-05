using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.ViewModels.ObserverModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public CellViewModel(int x, int y, int idx)
        {
            X = x;
            Y = y;
            Idx = idx;
            Clicked = false;
            Flaged = false;
            Image = "../../Images/block.png";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int X { get; set; }

        public int Y { get; set; }

        public int Idx { get; set; }

        public bool Clicked { get; set; }

        public bool Flaged { get; set; }

        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
