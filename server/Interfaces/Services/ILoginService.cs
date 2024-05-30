using WAMServer.Models;

namespace WAMServer.Interfaces.Services
{
    public interface ILoginService
    {
        public User? GetUser(string email);
    }
}