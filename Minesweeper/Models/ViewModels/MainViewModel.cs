using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _rows;

        public int Rows
        {
            get => _rows;
            set
            {
                _rows = value;
                OnPropertyChanged(nameof(Rows));
            }
        }

        private int _columns;

        public int Columns
        {
            get => _columns;
            set
            {
                _columns = value;
                OnPropertyChanged(nameof(Columns));
            }
        }

        public ObservableCollection<ButtonViewModel> cells { get; set; } = new ObservableCollection<ButtonViewModel>();

        public GameManager GameManager { get; set; }

        public MainViewModel()
        {
            GameManager = new GameManager();
            Rows = 16;
            Columns = 15;
            InitializeGame();
        }

        public void InitializeGame()
        {
            GameManager.Initialize(Rows, Columns, "intermediate");
            cells.Clear();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    cells.Add(new ButtonViewModel(i, j));
                }
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
