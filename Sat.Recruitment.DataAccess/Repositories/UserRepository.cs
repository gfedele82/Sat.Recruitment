using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.DataAccess.Interfaces;
using Sat.Recruitment.DataAccess.Schema;

namespace Sat.Recruitment.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _dbContext;

        public UserRepository(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return _dbContext.Users.AsNoTracking();
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<User> SaveOrUptedeAsync(User user)
        {
            user.Money += GetGift(user);
            var entity = await _dbContext.Users.FindAsync(user.Id);
            var existingUser = _dbContext.Users.AsNoTracking().Any(x => x.Email.ToLower().Equals(user.Email.ToLower()) || x.Phone.ToLower().Equals(user.Phone.ToLower()));
            var existingUser2 = _dbContext.Users.AsNoTracking().Any(x => x.Name.ToLower().Equals(user.Name.ToLower()) && x.Address.ToLower().Equals(user.Address.ToLower()));
            _dbContext.ChangeTracker.Clear();
            if (entity == null && !existingUser && !existingUser2)
            {
                await _dbContext.Users.AddAsync(user);
            }
            else if(user.Id != 0) 
            {
                _dbContext.Users.Update(user);
            }
            else
            {
                return user;
            }
            _dbContext.SaveChanges();
            return user;


        }
        public async Task<User> DeleteAsync(int id)
        {
            _dbContext.ChangeTracker.Clear();
            var entity = await _dbContext.Users.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            else
            {
                _dbContext.Users.Remove(entity);
            }
            _dbContext.SaveChanges();
            return entity;
        }

        private decimal GetGift(User user)
        {
            decimal percentage = 0;
            switch (user.UserType)
            {
                case "Normal":
                    percentage = user.Money > 100 ? Convert.ToDecimal(0.12) : 0;
                    percentage = percentage == 0 && user.Money > 10 ? Convert.ToDecimal(0.8) : 0;
                    break;
                case "SuperUser":
                    percentage = user.Money > 100 ? Convert.ToDecimal(0.20) : 0;
                    break;
                case "Premium":
                    percentage = user.Money > 100 ? Convert.ToDecimal(2) : 0;
                    break;
            }
            return user.Money * percentage;
        }
    }
}
