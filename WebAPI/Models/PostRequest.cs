using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class PostRequest
    {
        public required string Title { get; set; } = "Post title";

        public required string Description { get; set; } = "Post content";
      
    }
}
