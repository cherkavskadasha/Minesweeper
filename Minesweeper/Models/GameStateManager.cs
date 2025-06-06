using Minesweeper.Models.Field;
using System;

namespace Minesweeper.Models
{
    public class GameStateManager
    {
        public bool IsEnd { get; private set; }
        public bool IsWin { get; private set; }
        public bool IsSafeClick { get; set; }
        public int Score { get; private set; }

        public event Action<bool> GameEnded;
        public event Action<int> ScoreChanged;

        public void Initialize()
        {
            IsEnd = false;
            IsWin = false;
            IsSafeClick = false;
            Score = 0;
        }

        public void AddToScore(int points)
        {
            Score += points;
            if (Score < 0)
                Score = 0;

            ScoreChanged?.Invoke(Score);
        }

        public void CheckWinCondition(Field.Field field)
        {
            if (field.ActiveCellsRemain == 0 && !IsEnd)
            {
                EndGame(true);
            }
        }

        public void EndGame(bool isWin)
        {
            if (IsEnd) return; // Запобігаємо подвійному завершенню

            IsEnd = true;
            IsWin = isWin;
            GameEnded?.Invoke(isWin);
        }

        public bool IsFirstMove(Field.Field field, int rows, int columns)
        {
            return field.ActiveCellsRemain == rows * columns - field.BombCount;
        }
    }
}