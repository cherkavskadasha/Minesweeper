using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.DbModels
{
    public class GameRepository : IGameRepository
    {
        public int LoginUser(string login, string password)
        {
            using (var dbContext = new GameDbContext())
            {
                User? user = dbContext.Users.AsNoTracking().Where(u => u.Login == login && u.Password == password).FirstOrDefault();

                if (user == null)
                {
                    return -1;
                }

                return user.Id;
            }
        }

        public int RegisterUser(string login, string password)
        {
            using (var dbContext = new GameDbContext())
            {
                User? user = dbContext.Users.AsNoTracking().Where(u => u.Login == login).FirstOrDefault();

                if (user != null)
                {
                    return -1;
                }

                User newUser = new User
                {
                    Login = login,
                    Password = password
                };

                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();

                return newUser.Id;
            }
        }

        public void AddResult(int userId, string difficulty, string gameResult, int score)
        {
            using (var dbContext = new GameDbContext())
            {
                Result result = new Result
                {
                    Difficulty = difficulty,
                    GameResult = gameResult,
                    Score = score,
                    UserId = userId
                };

                dbContext.Results.Add(result);
                dbContext.SaveChanges();
            }
        }

        public List<Result> GetResults(int userId)
        {
            using (var dbContext = new GameDbContext())
            {
                List<Result> results = dbContext.Results.Where(r => r.UserId == userId).ToList();

                return results;
            }
        }
    }
}
