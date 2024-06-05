using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Services
{
    /// <summary>
    /// Service for handling user authentication through the database.
    /// </summary>
    public class DBLoginService : ILoginService
    {
        private WamDBContext _context;

        public DBLoginService(WamDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a user from the database based on their email.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>The user corresponding to the provided email, if found; otherwise, null.</returns>
        public User? GetUser(string email)
        {
            return _context.Users.Where(user => user.Email == email).FirstOrDefault();
        }
    }
}
