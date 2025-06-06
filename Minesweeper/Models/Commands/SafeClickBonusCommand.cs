namespace Minesweeper.Models.Commands
{
    public class SafeClickBonusCommand : IGameCommand
    {
        private readonly GameManager _gameManager;
        private readonly GameStateManager _gameState;

        public SafeClickBonusCommand(GameManager gameManager, GameStateManager gameState)
        {
            _gameManager = gameManager;
            _gameState = gameState;
        }

        public bool CanExecute()
        {
            return _gameManager.SafeClickBonusQuantity > 0 && !_gameState.IsEnd;
        }

        public void Execute()
        {
            if (!CanExecute()) return;

            _gameState.IsSafeClick = true;
            _gameManager.SafeClickBonusQuantity--;
        }

        public void Undo()
        {
            _gameState.IsSafeClick = false;
            _gameManager.SafeClickBonusQuantity++;
        }
    }
}