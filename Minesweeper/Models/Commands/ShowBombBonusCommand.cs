using System;

namespace Minesweeper.Models.Commands
{
    public class ShowBombBonusCommand : IGameCommand
    {
        private const int BombBonusPenalty = 1000;

        private readonly GameManager _gameManager;
        private readonly GameStateManager _gameState;

        public ShowBombBonusCommand(GameManager gameManager, GameStateManager gameState)
        {
            _gameManager = gameManager;
            _gameState = gameState;
        }

        public bool CanExecute()
        {
            return _gameManager.ShowBombBonusQuantity > 0 && !_gameState.IsEnd;
        }

        public void Execute()
        {
            if (!CanExecute()) return;

            _gameState.AddToScore(-BombBonusPenalty);
            _gameManager.ShowBombBonusQuantity--;

        }

        public void Undo()
        {
            _gameState.AddToScore(BombBonusPenalty);
            _gameManager.ShowBombBonusQuantity++;
        }
    }
}