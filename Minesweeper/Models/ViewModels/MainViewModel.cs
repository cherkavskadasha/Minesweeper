using Minesweeper.Models.DbModels;
using Minesweeper.Models.ViewModels.ObserverModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel(IGameRepository repository)
        {
            _repository = repository;

            Menu = new MenuViewModel(repository);

            GameManager = new GameManager();

            Rows = 16;
            Columns = 15;

            CheckBombCommand = new RelayCommand((o) =>
            {
                int idx = (int)o;

                CellViewModel cell = Cells[idx];
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
                    GameStatus.EmptyCellCount--;
                    if (cell.Flaged)
                    {
                        cell.Flaged = false;
                        GameStatus.BombCellCount++;
                    }
                }
            }, (o) => !Cells[(int)o].Clicked && !GameStatus.IsGameEnded);

            FlagCommand = new RelayCommand((o) =>
            {
                int idx = (int)o;
                CellViewModel cell = Cells[idx];

                if (!cell.Clicked)
                {
                    if (cell.Flaged)
                    {
                        UpdateImage(cell, "block");
                        GameStatus.BombCellCount++;
                    }
                    else
                    {
                        UpdateImage(cell, "flag");
                        GameStatus.BombCellCount--;
                    }
                    cell.Flaged = !cell.Flaged;
                }
            },
            (o) => (GameStatus.BombCellCount > 0 || Cells[(int)o].Flaged) && !GameStatus.IsGameEnded);

            GameStartCommand = new RelayCommand((o) =>
            {
                InitializeGame();
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IGameRepository _repository;

        public MenuViewModel Menu {  get; private set; }

        private int _rows;

        public int Rows
        {
            get => _rows;
            set
            {
                _rows = value;
                OnPropertyChanged();
            }
        }

        private int _columns;

        public int Columns
        {
            get => _columns;
            set
            {
                _columns = value;
                OnPropertyChanged();
            }
        }

        private GameStatusViewModel _gameStatus;

        public GameStatusViewModel GameStatus
        {
            get => _gameStatus;
            set
            {
                _gameStatus = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public GameManager GameManager { get; set; }

        public RelayCommand CheckBombCommand { get; set; }

        public RelayCommand FlagCommand { get; set; }

        public RelayCommand GameStartCommand { get; set; }

        public void InitializeGame()
        {
            GameManager.Initialize(Rows, Columns, "intermediate");

            GameStatus = new GameStatusViewModel(Rows * Columns - GameManager.Field.BombCount, GameManager.Field.BombCount);

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
            GameStatus.Score = GameManager.Score;
            if (isFullUpdate)
            {
                int startFlagCount = GameManager.Field.BombCount;

                foreach (var cell in Cells)
                {
                    if (GameManager.Field.Cells[cell.X, cell.Y].IsActivated)
                    {
                        cell.Clicked = true;
                        cell.Flaged = false;
                        UpdateImageByType(cell);
                    }
                    else if (cell.Flaged)
                    {
                        startFlagCount--;
                    }
                }
                GameStatus.EmptyCellCount = GameManager.Field.ActiveCellsRemain;
                GameStatus.BombCellCount = startFlagCount;
            }
            if (GameManager.IsEnd)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            GameStatus.IsGameEnded = true;
            GameStatus.IsWin = GameManager.IsWin;

            foreach (var cell in Cells)
            {
                if (GameManager.Field.Cells[cell.X, cell.Y].IsBomb)
                {
                    UpdateImage(cell, "bomb");
                }
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

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
