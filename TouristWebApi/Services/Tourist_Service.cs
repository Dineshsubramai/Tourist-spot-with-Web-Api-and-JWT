using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TouristWebApi.Dbcontext;
using TouristWebApi.Models;
using TouristWebApi.Services;
namespace TourWebAPI.Services
    
{
    public class Tourist_service :ITourist_service

    {
        private readonly Tourist_DbContext touristDbContext;

        public Tourist_service(Tourist_DbContext _touristDbContext)
        {
            touristDbContext = _touristDbContext;
        }

        public async Task<IEnumerable<place>> GetAllPlaces()
        {
            var allPlaces = await touristDbContext.Places.ToListAsync();
            return allPlaces;
        }


        public async Task<IEnumerable<place>> GetplaceAboveAlitude(int alitude)
        {
            var highPlaces = await touristDbContext.Places.Where(x => x.alitude > alitude).ToListAsync();
            return highPlaces;
        }


        public async Task<IEnumerable<place>> GetAllPlace()
        {
            var AllPlaces = await touristDbContext.Places.ToListAsync();

            return AllPlaces;
        }


        public async Task<bool> AddPlace(place newPlace)
        {
            await touristDbContext.Places.AddAsync(newPlace);
            await touristDbContext.SaveChangesAsync();
            return true;
        }
           
        }
    }
