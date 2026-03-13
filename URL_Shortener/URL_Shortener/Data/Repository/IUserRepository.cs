using URL_Shortener.Models;

namespace URL_Shortener.Data.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByUserNameAsync(string userName);
    }
}
