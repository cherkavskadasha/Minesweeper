using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.Commands
{
    public interface IGameCommand
    {
        void Execute();
        bool CanExecute();
        void Undo();
    }
}