using TouristWebApi.Models;

namespace TouristWebApi.Services
{
    public interface ITourist_service
    {
       Task<IEnumerable<place>> GetAllPlaces();

        Task<IEnumerable<place>> GetplaceAboveAlitude(int alitude);


        Task<bool> AddPlace(place newPlace);
        


    }
}
