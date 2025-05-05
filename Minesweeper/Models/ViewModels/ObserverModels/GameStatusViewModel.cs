using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.ViewModels.ObserverModels
{
    public class GameStatusViewModel : INotifyPropertyChanged
    {
        public GameStatusViewModel(int emptyCellCount, int bombCellCount, GameManager gameManager)
        {
            IsWin = false;
            IsGameEnded = false;
            Score = 0;
            EmptyCellCount = emptyCellCount;
            BombCellCount = bombCellCount;
            _gameManager = gameManager;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private GameManager _gameManager;

        private bool _isGameEnded;

        public bool IsGameEnded
        {
            get => _isGameEnded;
            set
            {
                _isGameEnded = value;
                OnPropertyChanged();
            }
        }

        private bool _isWin;

        public bool IsWin
        {
            get => _isWin;
            set
            {
                _isWin = value;
                OnPropertyChanged();
            }
        }

        private int _emptyCellCount;

        public int EmptyCellCount
        {
            get => _emptyCellCount;
            set
            {
                _emptyCellCount = value;
                OnPropertyChanged();
            }
        }

        private int _bombCellCount;

        public int BombCellCount
        {
            get => _bombCellCount;
            set
            {
                _bombCellCount = value;
                OnPropertyChanged();
            }
        }

        private int _score;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        public void Update()
        {
            Score = _gameManager.Score;
            EmptyCellCount = _gameManager.Field.ActiveCellsRemain;
        }

        public void End()
        {
            IsGameEnded = true;
            IsWin = _gameManager.IsWin;
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
