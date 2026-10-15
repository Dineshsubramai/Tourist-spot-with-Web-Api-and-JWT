using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using TouristWebApi.Dbcontext;
using TouristWebApi.Models;
using TouristWebApi.Services;
using TouristWebApi.Tourist_DTO;
using TourWebAPI.Services;

namespace TouristWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tourist_Controller : ControllerBase
    {


        private readonly Tourist_service _Tourist_service;

        public Tourist_Controller(Tourist_service service)
        {
            _Tourist_service = service;

        }

        [HttpGet("GetAllPlaces")]
        public async Task<ActionResult<IEnumerable<TouristResponseDTO>>> GetAllPlaces()
        {
            var allplaces = await _Tourist_service.GetAllPlaces();
            return Ok(allplaces);
        }


        [HttpGet("GetHillStation")]
        public ActionResult<IEnumerable<TouristResponseDTO>> GetHillStation(int alitude)
        {
            Task<IEnumerable<place>> highplaces = _Tourist_service.GetplaceAboveAlitude(alitude);
            return Ok("Spoted");
        }


        [HttpPost("AddPlaces")]
        public async Task<IActionResult> createplace(TouristResponseDTO tourDTO)
        {
            //if (string.IsNullOrWhiteSpace(tourDTO.description))
            //{
            //    return BadRequest("Description is required.");
            //}

            var newPlace = new place
            {
                Name=tourDTO.Name,
                Location = tourDTO.Location,
                alitude=tourDTO.alitude,
                Desc = tourDTO.description
                
                // Map your other required properties here
            };

            var result = await _Tourist_service.AddPlace(newPlace);

            return Ok(new
            {
                message = "Place added successfully",
                success = result
            });
        }






    }
    }

