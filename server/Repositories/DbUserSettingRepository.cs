using WAMServer.Interfaces;
using WAMServer.Models;
using Microsoft.EntityFrameworkCore;

namespace WAMServer.Repositories{

    public class DbUserSettingRepository : IRepository<UserSetting>
    {
        public Task<UserSetting> AddAsync(UserSetting entity)
        {
            throw new NotImplementedException();
        }

        public Task<UserSetting?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserSetting? Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserSetting?>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserSetting?> UpdateAsync(UserSetting entity, Func<UserSetting, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}