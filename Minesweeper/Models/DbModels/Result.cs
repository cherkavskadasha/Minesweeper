using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.DbModels
{
    public class Result
    {
        public int Id { get; set; }

        public string Difficulty { get; set; }

        public string GameResult { get; set; }

        public int Score { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
