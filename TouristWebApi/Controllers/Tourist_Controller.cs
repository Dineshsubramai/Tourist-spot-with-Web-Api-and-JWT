using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TouristWebApi.Models;
using TouristWebApi.Services;
using TouristWebApi.Tourist_DTO;

namespace TouristWebApi.Controllers;

[ApiController]
[Route("api/places")]
public class Tourist_Controller : ControllerBase
{
    private readonly ITourist_service _service;

    public Tourist_Controller(ITourist_service service)
    {
        _service = service;
    }

    // Anyone can view all tourist places.
    [HttpGet]
    public async Task<ActionResult<List<place>>> GetAllPlaces()
    {
        return Ok(await _service.GetAllPlaces());
    }

    // New clean route: a valid JWT is needed.
    [Authorize]
    [HttpGet("above-altitude/{altitude:int}")]
    public async Task<ActionResult<List<place>>> GetPlacesAboveAltitude(int altitude)
    {
        return Ok(await _service.GetPlacesAboveAltitude(altitude));
    }

    // Keep the previous route for clients and tests that already use it.
    [Authorize]
    [HttpGet("GetHillStation")]
    public ActionResult<IEnumerable<TouristResponseDTO>> GetHillStation(int alitude)
    {
        var places = _service.GetplaceAboveAlitude(alitude).GetAwaiter().GetResult();
        var result = places.Select(place => new TouristResponseDTO
        {
            Id = place.Id,
            Name = place.Name,
            Location = place.Location,
            alitude = place.alitude,
            description = place.Desc
        });
        return Ok(result);
    }

    // A valid JWT is needed to add a place.
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<place>> AddPlace(TouristResponseDTO dto)
    {
        var newPlace = new place
        {
            Name = dto.Name,
            Location = dto.Location,
            alitude = dto.alitude,
            Desc = dto.description
        };

        var savedPlace = await _service.AddPlace(newPlace);
        return CreatedAtAction(nameof(GetAllPlaces), new { id = savedPlace.Id }, savedPlace);
    }
}
