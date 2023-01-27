using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Service.Interface;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public IPost _post;

        public PostController(IPost post)
        {
            _post = post;
        }

        [HttpPost("/create-post")]
        [Authorize]
        public async Task<ActionResult<PostModel>> CreatePost(PostRequest post)
        {
            var response = await _post.CreatePost(post);

            if (response == null)
            {
                throw new Exception("Произошла ошибка");
            }

            return response;
        }

        [HttpGet("/get-post")]
        [Authorize]
        public async Task<List<PostModel>> GetPostById()
        {
            var response = await _post.GetPost();

            return response;
        }


        [HttpPut("/update-post")]
        [Authorize]

        public async Task<PostModel> UpdatePost(int idPost, PostRequest post)
        {
            var response = await _post.UpdatePost(idPost, post);

            return response;
        }


        [HttpDelete("/delete-post")]
        [Authorize]

        public async Task<ActionResult> DeletePost(int idPost)
        {
            var response = _post.DeletePost(idPost);

            if (response == null)
            {
                return Ok("Пост был удален");
            }
            else
            {
                return BadRequest("Такого поста не существует");
            }
        }
    }
}
