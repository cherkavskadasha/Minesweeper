using Minesweeper.Models.Field;
using Minesweeper.Models.DifficultyStrategy;
using System;
using System.Collections.Generic;

namespace Minesweeper.Models.Commands
{
    public class ActivateCellCommand : IGameCommand
    {
        private readonly int _x;
        private readonly int _y;
        private readonly Field.Field _field;
        private readonly GameStateManager _gameState;
        private readonly IDifficultyStrategy _difficultyStrategy;
        private readonly int _rows;
        private readonly int _columns;

        private readonly List<(int x, int y)> _activatedCells = new List<(int x, int y)>();

        public ActivateCellCommand(int x, int y, Field.Field field, GameStateManager gameState,
            IDifficultyStrategy difficultyStrategy, int rows, int columns)
        {
            _x = x;
            _y = y;
            _field = field;
            _gameState = gameState;
            _difficultyStrategy = difficultyStrategy;
            _rows = rows;
            _columns = columns;
        }

        public bool CanExecute()
        {
            return IsWithinBounds(_x, _y) && !_gameState.IsEnd && !_field.Cells[_x, _y].IsActivated;
        }

        public void Execute()
        {
            if (!CanExecute()) return;

            // Якщо це перший хід і клітинка не пуста, перегенеруємо поле
            if (_gameState.IsFirstMove(_field, _rows, _columns) && _field.Cells[_x, _y].CellType != CellType.None)
            {
                RegenerateFieldUntilCellIsEmpty();
            }

            ActivateCell(_x, _y);
            _gameState.CheckWinCondition(_field);
        }

        public void Undo()
        {
            // Скасування активації клітинок (для майбутнього розширення)
            foreach (var (x, y) in _activatedCells)
            {
                _field.Cells[x, y].IsActivated = false;
                _field.ActiveCellsRemain++;
            }
            _activatedCells.Clear();
        }

        private void ActivateCell(int x, int y)
        {
            if (!IsWithinBounds(x, y) || _field.Cells[x, y].IsActivated)
                return;

            Cell cell = _field.Cells[x, y];
            cell.Activate();
            _activatedCells.Add((x, y));

            if (cell.IsBomb)
            {
                HandleBombActivation();
            }
            else
            {
                _difficultyStrategy.UpdateScore(cell.CellType);
                _field.ActiveCellsRemain--;

                if (cell.CellType == CellType.None)
                {
                    ActivateAdjacentCells(x, y);
                }
            }

            _gameState.IsSafeClick = false;
        }

        private void HandleBombActivation()
        {
            if (_gameState.IsSafeClick)
            {
                _field.BombCount--;
            }
            else
            {
                _gameState.EndGame(false);
            }
        }

        private void ActivateAdjacentCells(int x, int y)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i != 0 || j != 0) && IsWithinBounds(x + i, y + j))
                    {
                        ActivateCell(x + i, y + j);
                    }
                }
            }
        }

        private void RegenerateFieldUntilCellIsEmpty()
        {
            while (_field.Cells[_x, _y].CellType != CellType.None)
            {
                _difficultyStrategy.GenerateField();
            }
        }

        private bool IsWithinBounds(int x, int y) =>
            x >= 0 && y >= 0 && x < _rows && y < _columns;
    }
}