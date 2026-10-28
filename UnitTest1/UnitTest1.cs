using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using TouristWebApi.Tourist_DTO;
using System.Numerics;
using TouristWebApi.Controllers;
using TouristWebApi.Dbcontext;
using TouristWebApi.Migrations;
using TouristWebApi.Models;
using TouristWebApi.Services;
using TourWebAPI.Services;

namespace TuristTest
{
    public class TouristUnitTester
    {
        [Fact]
        public async Task Test1()
        {
            //Arange
            // Arrange

            int altitude = 150;


            var expectedPlaces = new List<place>

{

     new place { Name = "HillPlace1", Desc = "HillDesc1", alitude = 200 },

     new place { Name = "HillPlace2", Desc = "HillDesc2", alitude = 300 }

};
            var mockService = new Mock<ITourist_service>();
            mockService.Setup(service => service.GetplaceAboveAlitude(altitude))
                       .ReturnsAsync(expectedPlaces);

            var controller = new Tourist_Controller(mockService.Object);

            //Act
            var result = controller.GetHillStation(altitude);

            //Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TouristResponseDTO>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, okResult.StatusCode);

        }
    }
}
