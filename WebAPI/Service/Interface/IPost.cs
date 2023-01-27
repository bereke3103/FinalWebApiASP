using WebAPI.Models;

namespace WebAPI.Service.Interface
{
    public interface IPost
    {
        public Task<PostModel> CreatePost(PostRequest post);

        public Task<PostModel> UpdatePost(int idPost, PostRequest post);

        public  Task DeletePost(int idPost);

        public Task<List<PostModel>> GetPost();

    
     
    }
}
