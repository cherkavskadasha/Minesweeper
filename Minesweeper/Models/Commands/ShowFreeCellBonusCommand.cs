using Minesweeper.Models.Field;
using System;

namespace Minesweeper.Models.Commands
{
    public class ShowFreeCellBonusCommand : IGameCommand
    {
        private const int FreeCellBonusPenalty = 500;

        private readonly GameManager _gameManager;
        private readonly GameStateManager _gameState;
        private readonly CommandInvoker _commandInvoker;

        private (int x, int y) _activatedCell = (-1, -1);

        public ShowFreeCellBonusCommand(GameManager gameManager, GameStateManager gameState, CommandInvoker commandInvoker)
        {
            _gameManager = gameManager;
            _gameState = gameState;
            _commandInvoker = commandInvoker;
        }

        public bool CanExecute()
        {
            return _gameManager.ShowFreeCellBonusQuantity > 0 && !_gameState.IsEnd;
        }

        public void Execute()
        {
            if (!CanExecute()) return;

            _gameState.AddToScore(-FreeCellBonusPenalty);
            _gameManager.ShowFreeCellBonusQuantity--;

            if (!TryActivateFirstCell(c => c.CellType == CellType.None))
            {
                TryActivateFirstCell(c => c.CellType != CellType.Bomb);
            }
        }

        public void Undo()
        {
            if (_activatedCell.x >= 0)
            {
                _gameState.AddToScore(FreeCellBonusPenalty);
                _gameManager.ShowFreeCellBonusQuantity++;

                _activatedCell = (-1, -1);
            }
        }

        private bool TryActivateFirstCell(Func<Cell, bool> cellSelector)
        {
            for (int i = 0; i < _gameManager.Rows; i++)
            {
                for (int j = 0; j < _gameManager.Columns; j++)
                {
                    var cell = _gameManager.Field.Cells[i, j];
                    if (!cell.IsActivated && cellSelector(cell))
                    {
                        var activateCommand = new ActivateCellCommand(i, j, _gameManager.Field, _gameState,
                            _gameManager.DifficultyStrategy, _gameManager.Rows, _gameManager.Columns);
                        _commandInvoker.ExecuteCommand(activateCommand);
                        _activatedCell = (i, j);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}