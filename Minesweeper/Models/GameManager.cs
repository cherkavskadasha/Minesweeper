using Minesweeper.Models.Commands;
using Minesweeper.Models.DifficultyStrategy;
using Minesweeper.Models.Field;

namespace Minesweeper.Models
{
    public class GameManager
    {
        public Field.Field Field { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int ShowFreeCellBonusQuantity { get; set; }
        public int ShowBombBonusQuantity { get; set; }
        public int SafeClickBonusQuantity { get; set; }
        public IDifficultyStrategy DifficultyStrategy { get; set; }
        public GameStateManager GameState { get; private set; }
        public CommandInvoker CommandInvoker { get; private set; }

        // Властивості для зворотної сумісності
        public int Score => GameState.Score;
        public bool IsEnd => GameState.IsEnd;
        public bool IsWin => GameState.IsWin;
        public bool IsSafeClick
        {
            get => GameState.IsSafeClick;
            set => GameState.IsSafeClick = value;
        }

        public GameManager()
        {
            GameState = new GameStateManager();
            CommandInvoker = new CommandInvoker();
        }

        public void Initialize(int rows, int columns, string difficulty)
        {
            Field = new Field.Field();
            Rows = rows;
            Columns = columns;

            GameState.Initialize();
            CommandInvoker.ClearHistory();

            DifficultyStrategy = difficulty switch
            {
                "Expert" => new ExpertDifficultyStrategy(this),
                "Intermediate" => new IntermediateDifficultyStrategy(this),
                _ => new BeginnerDifficultyStrategy(this)
            };

            DifficultyStrategy.GenerateField();
            DifficultyStrategy.SetBonusesQuantity();
        }

        public void ActivateCell(int x, int y)
        {
            var command = new ActivateCellCommand(x, y, Field, GameState, DifficultyStrategy, Rows, Columns);
            CommandInvoker.ExecuteCommand(command);
        }

        public void ShowFreeCellBonus()
        {
            var command = new ShowFreeCellBonusCommand(this, GameState, CommandInvoker);
            CommandInvoker.ExecuteCommand(command);
        }

        public void ShowBombBonus()
        {
            var command = new ShowBombBonusCommand(this, GameState);
            CommandInvoker.ExecuteCommand(command);
        }

        public void SafeClickBonus()
        {
            var command = new SafeClickBonusCommand(this, GameState);
            CommandInvoker.ExecuteCommand(command);
        }

        // Метод для скасування останньої дії (для майбутнього розширення)
        public void UndoLastAction()
        {
            CommandInvoker.UndoLastCommand();
        }
    }
}