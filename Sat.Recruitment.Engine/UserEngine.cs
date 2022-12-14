using Microsoft.Extensions.Logging;
using Sat.Recruitment.Contracts.Engine;
using Sat.Recruitment.DataAccess.Interfaces;
using Sat.Recruitment.DataAccess.DTOAdapter;
using Newtonsoft.Json;

namespace Sat.Recruitment.Engine
{
    public class UserEngine : IUserEngine
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserEngine> _logger;

        public UserEngine(IUserRepository repository,
           ILogger<UserEngine> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> Delete(int userId)
        {
            try
            {
                var entity = await _repository.DeleteAsync(userId);
                if (entity != null)
                {
                    _logger.LogInformation($"User Id: {userId} deleted");
                    return true;
                }

                _logger.LogInformation($"User Id: {userId} doesn't exist");
                return false;
            }
            catch(Exception ex)
            {
                _logger.LogError($"User Id: {userId} error: {ex.Message}");
                return false;
            }
        }

        public async Task<Models.User> GetById(int userId)
        {
            try
            {
                _logger.LogInformation($"User Id: {userId} to search");
                var entity = await _repository.GetByIdAsync(userId);
                return entity.ToModel();
            }
            catch( Exception ex)
            {
                _logger.LogError($"User Id: {userId} to search error: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<Models.User>> GetAll()
        {
            try
            {
                _logger.LogInformation($"Get All Users");
                List<Models.User> listUser = new List<Models.User>();
                var entities = await _repository.GetAsync();
                Parallel.ForEach(entities, entity =>
                {
                    listUser.Add(entity.ToModel());
                });
                return listUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get All Users error: {ex.Message}");
                return null;
            }
        }

        public async Task<Models.User> AddUser(Models.User user)
        {
            try
            {
                user.Id = 0;
                _logger.LogInformation($"User to Add: {JsonConvert.SerializeObject(user)}");
                var entity = await _repository.SaveOrUptedeAsync(user.ToDBModel());
                return entity.ToModel();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Add Users error: {ex.Message}");
                return null;
            }
        }

        public async Task<Models.User> UpdateUser(Models.User user)
        {
            try
            {
                _logger.LogInformation($"User to Update: {JsonConvert.SerializeObject(user)}");
                var entity = await _repository.SaveOrUptedeAsync(user.ToDBModel());
                return entity.ToModel();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update Users error: {ex.Message}");
                return null;
            }
        }
    }
}
