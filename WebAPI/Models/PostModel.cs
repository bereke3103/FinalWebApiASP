using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class PostModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = "Your title";
        public string Description { get; set; } = "Your desciption";

        [JsonIgnore]
        public UserModel User { get; set; }

        public int UserId { get; set; }
    }
}
