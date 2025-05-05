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

                CellViewModel cell = Cells[idx];
                if (!cell.Clicked)
                {
                    GameManager.ActivateCell(cell.X, cell.Y);
                    cell.Clicked = true;
                    if (GameManager.Field.Cells[cell.X, cell.Y].CellType == Field.CellType.None)
                    {
                        UpdateGameStatus(true);
                    }
                    else
                    {
                        UpdateImageByType(cell);
                        UpdateGameStatus(false);
                    }
                }
            });

            FlagCommand = new RelayCommand((o) =>
            {
                int idx = (int)o;
                CellViewModel cell = Cells[idx];

                if (!cell.Clicked)
                {
                    if (cell.Flaged)
                    {
                        UpdateImage(cell, "block");
                    }
                    else
                    {
                        UpdateImage(cell, "flag");
                    }
                    cell.Flaged = !cell.Flaged;
                }
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

        private bool _isGameEnded;

        public bool IsGameEnded
        {
            get => _isGameEnded;
            set
            {
                _isGameEnded = value;
                OnPropertyChanged(nameof(IsGameEnded));
            }
        }

        private bool _isWin;

        public bool IsWin
        {
            get => _isWin;
            set
            {
                _isWin = value;
                OnPropertyChanged(nameof(IsWin));
            }
        }

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public GameManager GameManager { get; set; }

        public RelayCommand CheckBombCommand { get; set; }

        public RelayCommand FlagCommand { get; set; }

        public void InitializeGame()
        {
            GameManager.Initialize(Rows, Columns, "intermediate");

            Cells.Clear();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Cells.Add(new CellViewModel(i, j, i * Columns + j));
                }
            }
        }

        public void UpdateGameStatus(bool isFullUpdate)
        {
            if (isFullUpdate)
            {
                foreach (var cell in Cells)
                {
                    if (GameManager.Field.Cells[cell.X, cell.Y].IsActivated)
                    {
                        cell.Clicked = true;
                        UpdateImageByType(cell);
                    }
                }
            }
            if (GameManager.IsEnd)
            {
                IsGameEnded = true;
                IsWin = GameManager.IsWin;
            }
        }

        private void UpdateImageByType(CellViewModel cell)
        {
            UpdateImage(cell, $"{(int)GameManager.Field.Cells[cell.X, cell.Y].CellType}");
        }

        private void UpdateImage(CellViewModel cell, string imageName)
        {
            cell.Image = $"../../Images/{imageName}.png";
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
