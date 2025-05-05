using Minesweeper.Models.DbModels;
using Minesweeper.Models.ViewModels.Commands;
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

            Menu = new MenuViewModel(repository, this);

            GameManager = new GameManager();

            InitializeCommands();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IGameRepository _repository;

        public MenuViewModel Menu {  get; private set; }

        public string Difficulty { get; set; }

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

        public RelayCommand InitializeGameCommand { get; set; }

        public RelayCommand BackToMenuCommand { get; set; }

        public void InitializeGame(int rows, int columns, string difficulty)
        {
            Rows = rows;
            Columns = columns;
            Difficulty = difficulty;

            GameManager.Initialize(rows, columns, difficulty);

            GameStatus = new GameStatusViewModel(rows * columns - GameManager.Field.BombCount, GameManager.Field.BombCount, GameManager);

            Cells.Clear();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Cells.Add(new CellViewModel(i, j, i * columns + j));
                }
            }
        }

        public void UpdateGameStatus(bool isFullUpdate)
        {
            GameStatus.Update();
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
                GameStatus.BombCellCount = startFlagCount;
            }
            if (GameManager.IsEnd)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            _gameStatus.End();

            string img = GameStatus.IsWin ? "bomb" : "9";
            foreach (var cell in Cells)
            {
                if (GameManager.Field.Cells[cell.X, cell.Y].IsBomb)
                {
                    UpdateImage(cell, img);
                }
            }

            _repository.AddResult(Menu.UserId, Difficulty, GameStatus.IsWin ? "Перемога" : "Поразка", GameStatus.Score);
        }

        public void UpdateImageByType(CellViewModel cell)
        {
            UpdateImage(cell, $"{(int)GameManager.Field.Cells[cell.X, cell.Y].CellType}");
        }

        public void UpdateImage(CellViewModel cell, string imageName)
        {
            cell.Image = $"../../Images/{imageName}.png";
        }

        public void InitializeCommands()
        {
            CheckBombCommand = GameCommandService.CreateCheckBombCommand(this);

            FlagCommand = GameCommandService.CreateFlagCommand(this);

            InitializeGameCommand = GameCommandService.CreateInitializeGameCommand(this);

            BackToMenuCommand = GameCommandService.CreateBackToMenuCommand(this);
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
