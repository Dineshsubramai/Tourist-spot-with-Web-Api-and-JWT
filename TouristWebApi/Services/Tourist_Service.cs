using Microsoft.EntityFrameworkCore;
using TouristWebApi.Dbcontext;
using TouristWebApi.Models;
using TouristWebApi.Services;

namespace TourWebAPI.Services;

public class Tourist_service : ITourist_service
{
    private readonly Tourist_DbContext _db;

    public Tourist_service(Tourist_DbContext db)
    {
        _db = db;
    }

    public Task<List<place>> GetAllPlaces()
    {
        return _db.Places.AsNoTracking().ToListAsync();
    }

    public Task<List<place>> GetPlacesAboveAltitude(int altitude)
    {
        return _db.Places.AsNoTracking()
            .Where(place => place.alitude > altitude)
            .ToListAsync();
    }

    // Keep this older name for existing callers in the solution.
    public async Task<IEnumerable<place>> GetplaceAboveAlitude(int altitude)
    {
        return await GetPlacesAboveAltitude(altitude);
    }

    public async Task<place> AddPlace(place newPlace)
    {
        _db.Places.Add(newPlace);
        await _db.SaveChangesAsync();
        return newPlace;
    }
}
