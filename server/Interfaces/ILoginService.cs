using WAMServer.Models;

namespace WAMServer.Interfaces
{
    public interface ILoginService
    {
        public User? GetUser(string email);
    }
}