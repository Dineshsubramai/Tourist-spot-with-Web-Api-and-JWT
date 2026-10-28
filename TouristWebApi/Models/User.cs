using System.ComponentModel.DataAnnotations;

namespace TouristWebApi.Models
{

    public class User

    {
        public int Id { get; set; }
        public string user { get; set; }
        public string password { get; set; }
    }
}
