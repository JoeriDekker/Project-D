using WAMServer.Interfaces;
using WAMServer.Models;
using Microsoft.EntityFrameworkCore;

namespace WAMServer.Repositories{

    public class DbWaterLevelSettingsRepository : IRepository<WaterLevelSettings>
    {

        private WamDBContext _context;

        public DbWaterLevelSettingsRepository(WamDBContext context)
        {
            _context = context;
        }

        public async Task<WaterLevelSettings> AddAsync(WaterLevelSettings entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<WaterLevelSettings> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public WaterLevelSettings? Get(Guid id)
        {
            return _context.WaterLevelSettings.Where(a => a.Id == id).FirstOrDefault();
        }

        public WaterLevelSettings? GetByUser(Guid id)
        {
            return _context.WaterLevelSettings.Where(a => a.UserId == id).FirstOrDefault();
        }

        public IEnumerable<WaterLevelSettings?> GetAll(Func<WaterLevelSettings, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WaterLevelSettings>> GetAllAsync()
        {
           throw new NotImplementedException();
        }

        // TODO: understand why it has to be 2 params instead of 1
        public async Task<WaterLevelSettings?> UpdateAsync(WaterLevelSettings entity, Func<WaterLevelSettings, bool> predicate)
        {
            var settings = _context.WaterLevelSettings.Where(predicate).FirstOrDefault();
            if (settings == null) return null;
            if (entity.PoleHeight != null) settings.PoleHeight = entity.PoleHeight;
            if (entity.PoleHeight != null) settings.PoleHeight = entity.PoleHeight;

            await _context.SaveChangesAsync();
            return settings;        
        }
    }
}