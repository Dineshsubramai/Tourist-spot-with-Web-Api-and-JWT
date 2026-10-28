using TouristWebApi.Models;

namespace TouristWebApi.Services;

public interface ITourist_service
{
    Task<List<place>> GetAllPlaces();
    Task<List<place>> GetPlacesAboveAltitude(int altitude);

    // Kept so older callers in this solution continue to work.
    Task<IEnumerable<place>> GetplaceAboveAlitude(int altitude);

    Task<place> AddPlace(place newPlace);
}
