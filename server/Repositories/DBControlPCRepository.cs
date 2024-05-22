using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Seeders;

namespace WAMServer.Repositories
{
    /// <summary>
    /// This class is responsible for handling the database operations for the ControlPC entity.
    /// </summary>
    public class DbControlPCRepository : IRepository<ControlPC>
    {
        private readonly WamDBContext _context;

        public DbControlPCRepository(WamDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a ControlPC to the database in an asynchronous manner.
        /// </summary>
        /// <param name="entity">The entity to be added, in this case the ControlPC.</param>
        /// <returns>The created ControlPC.</returns>
        public async Task<ControlPC> AddAsync(ControlPC entity)
        {
            await _context.ControlPC.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Deletes a ControlPC from the database by its id.
        /// </summary>
        /// <param name="id">The id of the ControlPC to be deleted.</param>
        /// <returns>The deleted ControlPC if it exists, otherwise null.</returns>
        public async Task<ControlPC?> DeleteAsync(Guid id)
        {
            var entity = await _context.ControlPC.FindAsync(id);
            if (entity != null)
            {
                _context.ControlPC.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return null;
        }

        /// <summary>
        /// Gets a ControlPC from the database by its id.
        /// </summary>
        /// <param name="id">The id of the ControlPC to be fetched.</param>
        /// <returns>Returns the ControlPC from the database if it exists, otherwise null.</returns>
        public ControlPC? Get(Guid id)
        {
            return _context.ControlPC.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Gets all ControlPCs from the database in an asynchronous manner.
        /// </summary>
        /// <returns>A list of all ControlPCs.</returns>
        public async Task<IEnumerable<ControlPC>> GetAllAsync()
        {
            return await _context.ControlPC.ToListAsync();
        }

        /// <summary>
        /// Gets a ControlPC from the database by its id in an asynchronous manner.
        /// </summary>
        /// <param name="id">The id that's used for fetching the ControlPC.</param>
        /// <returns>The ControlPC object if it exists in the database, otherwise null.</returns>
        public async Task<ControlPC?> GetAsync(Guid id)
        {
            return await _context.ControlPC.FindAsync(id);
        }

        /// <summary>
        /// Updates a ControlPC in the database in an asynchronous manner.
        /// </summary>
        /// <param name="entity">The ControlPC entity to be updated.</param>
        /// <param name="predicate">A function to locate the entity to be updated.</param>
        /// <returns>The updated ControlPC if the update was successful, otherwise null.</returns>
        public async Task<ControlPC?> UpdateAsync(ControlPC entity, Func<ControlPC, bool> predicate)
        {
            var existingEntity = _context.ControlPC.FirstOrDefault(predicate);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return null;
        }
    }
}
