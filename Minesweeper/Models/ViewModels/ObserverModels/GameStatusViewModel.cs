using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Minesweeper.Models.ViewModels.ObserverModels
{
    public class GameStatusViewModel : INotifyPropertyChanged
    {
        private GameManager _gameManager;

        private bool _isGameEnded;
        private bool _isWin;
        private int _emptyCellCount;
        private int _bombCellCount;
        private int _score;

        public GameStatusViewModel(int emptyCellCount, int bombCellCount, GameManager gameManager)
        {
            _gameManager = gameManager;
            _emptyCellCount = emptyCellCount;
            _bombCellCount = bombCellCount;
            _score = 0;
            _isWin = false;
            _isGameEnded = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsGameEnded
        {
            get => _isGameEnded;
            set => SetField(ref _isGameEnded, value);
        }

        public bool IsWin
        {
            get => _isWin;
            set => SetField(ref _isWin, value);
        }

        public int EmptyCellCount
        {
            get => _emptyCellCount;
            set => SetField(ref _emptyCellCount, value);
        }

        public int BombCellCount
        {
            get => _bombCellCount;
            set => SetField(ref _bombCellCount, value);
        }

        public int Score
        {
            get => _score;
            set => SetField(ref _score, value);
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

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
