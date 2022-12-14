using Sat.Recruitment.Models;

namespace Sat.Recruitment.Contracts.Engine
{
    public interface IUserEngine
    {
        Task<bool> Delete(int userId);

        Task<User> GetById(int userId);

        Task<IEnumerable<User>> GetAll();

        Task<User> AddUser(User user);

        Task<User> UpdateUser(User user);
    }
}
