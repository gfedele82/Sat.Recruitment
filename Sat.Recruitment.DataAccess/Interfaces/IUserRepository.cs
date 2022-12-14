using Sat.Recruitment.DataAccess.Schema;

namespace Sat.Recruitment.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> SaveOrUptedeAsync(User user);
        Task<User> DeleteAsync(int id);
    }
}
