using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Services
{
    public class DBLoginService : ILoginService
    {
        private WamDBContext _context;

        public DBLoginService(WamDBContext context)
        {
            _context = context;
        }

        public User? GetUser(string email)
        {
            return _context.Users.Where(_ => _.Email == email).FirstOrDefault();
        }
    }
}