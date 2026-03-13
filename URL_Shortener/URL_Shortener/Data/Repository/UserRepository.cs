using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;

namespace URL_Shortener.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
