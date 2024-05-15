using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Repositories
{
    public class DbAddressRepository : IRepository<Address>
    {
        private WamDBContext _context;

        public DbAddressRepository(WamDBContext context)
        {
            _context = context;
        }

        public async Task<Address> AddAsync(Address entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<Address> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Address? Get(Guid id)
        {
            return _context.Addresses.Where(a => a.Id == id).FirstOrDefault();
        }

        public Task<IEnumerable<Address>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Address> UpdateAsync(Address entity)
        {
            throw new NotImplementedException();
        }
    }
}