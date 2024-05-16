using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Repositories
{
    /// <summary>
    /// This class is responsible for handling the database operations for the Address entity.
    /// </summary>
    public class DbAddressRepository : IRepository<Address>
    {
        private WamDBContext _context;

        public DbAddressRepository(WamDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds an address to the database in an asynchronous manner.
        /// </summary>
        /// <param name="entity">The entity to be added. In this case the address.</param>
        /// <returns>The created address.</returns>
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

        /// <summary>
        /// Gets an address from the database by its id.
        /// </summary>
        /// <param name="id">The id that's used for fetching the address</param>
        /// <returns>The address object if it exists in the database, returns null if it doesnt exist.</returns>
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