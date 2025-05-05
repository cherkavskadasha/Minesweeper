using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.DbModels
{
    public interface IGameRepository
    {
        public int LoginUser(string login, string password);

        public int RegisterUser(string login, string password);

        public void AddResult(int userId, string difficulty, string result, int score);

        public List<Result> GetResults(int userId);
    }
}
