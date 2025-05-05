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
        public GameStatusViewModel(int emptyCellCount, int bombCellCount)
        {
            IsWin = false;
            IsGameEnded = false;
            Score = 0;
            EmptyCellCount = emptyCellCount;
            BombCellCount = bombCellCount;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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
                OnPropertyChanged(nameof(IsWin));
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

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
