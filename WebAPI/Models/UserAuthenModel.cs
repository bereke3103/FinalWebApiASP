using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class UserAuthenModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
