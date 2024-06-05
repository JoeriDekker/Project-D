using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Services
{
    /// <summary>
    /// Service for handling user authentication through the database.
    /// </summary>
    public class DBWaterlevelSettingsService : IWaterLevelSettings
    {
        private WamDBContext _context;

        public DBWaterlevelSettingsService(WamDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a user from the database based on their email.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>The user corresponding to the provided email, if found; otherwise, null.</returns>
        public WaterLevelSettings? GetByUserId(Guid id)
        {
            return _context.WaterLevelSettings.Where(wls => wls.UserId == id).FirstOrDefault();
        }
    }
    

}
