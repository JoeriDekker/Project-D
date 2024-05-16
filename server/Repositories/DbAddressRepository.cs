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

        public Task<Address?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Address? Get(Guid id)
        {
            return _context.Addresses.Where(a => a.Id == id).FirstOrDefault();
        }

        public Task<IEnumerable<Address?>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Address?> UpdateAsync(Address entity, Func<Address, bool> predicate)
        {
            var address = _context.Addresses.Where(predicate).FirstOrDefault();
            if (address == null) return null;
            address.Street = entity.Street;
            address.HouseNumber = entity.HouseNumber;
            address.City = entity.City;
            address.Zip = entity.Zip;
            await _context.SaveChangesAsync();
            return address;
        }
    }
}