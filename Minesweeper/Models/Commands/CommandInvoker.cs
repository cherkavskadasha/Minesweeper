using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.Commands
{
    public class CommandInvoker
    {
        private readonly Stack<IGameCommand> _commandHistory = new Stack<IGameCommand>();

        public void ExecuteCommand(IGameCommand command)
        {
            if (command.CanExecute())
            {
                command.Execute();
                _commandHistory.Push(command);
            }
        }

        public void UndoLastCommand()
        {
            if (_commandHistory.Count > 0)
            {
                var lastCommand = _commandHistory.Pop();
                lastCommand.Undo();
            }
        }

        public void ClearHistory()
        {
            _commandHistory.Clear();
        }
    }
}