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
        public MainViewModel()
        {
            GameManager = new GameManager();

            Rows = 16;
            Columns = 15;

            CheckBombCommand = new RelayCommand((o) =>
            {
                int idx = (int)o;

                ButtonViewModel cell = cells[idx];

                GameManager.ActivateCell(cell.X, cell.Y);
            });
            InitializeGame();
        }

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

        public RelayCommand CheckBombCommand { get; set; }

        public void InitializeGame()
        {
            GameManager.Initialize(Rows, Columns, "intermediate");

            cells.Clear();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    cells.Add(new ButtonViewModel(i, j, i * Columns + j));
                }
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
