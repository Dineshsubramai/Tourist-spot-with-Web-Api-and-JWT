using TouristWebApi.Models;

namespace TouristWebApi.Services
{
    public interface ILoginService
    {
        string PerformLogin(string username, string password);
    }
}