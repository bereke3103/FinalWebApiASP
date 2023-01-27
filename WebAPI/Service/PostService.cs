using System.Security.Claims;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Service.Interface;

namespace WebAPI.Service
{
    public class PostService : IPost
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor _http;
        public PostService(DataContext context, IHttpContextAccessor httpContext)
        {
            this.context = context;
            _http = httpContext;
        }

        public async Task<PostModel> CreatePost(PostRequest post)
        {

            var emailIden = _http.HttpContext?
                .User?
                .Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value!;

            var person = context.Users.FirstOrDefault(u => u.Email == emailIden);

            if (person == null)
            {
                throw new Exception("Такого пользователя не существует");
            }


            var newPost = new PostModel()
            {
                Title = post.Title,
                Description = post.Description,
                User = person
            };


            await context.Posts.AddAsync(newPost);
            await context.SaveChangesAsync();
            return newPost;


        }



        public async Task<List<PostModel>?> GetPost()
        {
            var emailIden = _http.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value!;

            var person = context.Users.FirstOrDefault(u => u.Email == emailIden);

            if (person == null)
            {
                throw new Exception("Вы ввели неправильный логин или пароль");
            }

            var posts = context.Posts.Where(p => p.UserId == person.Id).ToList();

            if (posts.Count == 0)
            {
                throw new Exception("У вас нет ни одного поста");
            }

            return posts;
        }


        public async Task<PostModel> UpdatePost(int idPost, PostRequest post)
        {
            var emailIden = _http.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value!;

            var person = context.Users.FirstOrDefault(u => u.Email == emailIden);

            if (person == null)
            {
                throw new Exception("Такого пользователя не существует");
            }

            //var posts = context.Posts.Where(p=> p.UserId== person.Id).ToList();

            var findPost = context.Posts.FirstOrDefault(p => p.Id == idPost && person.Id == p.UserId);

            if (findPost != null)
            {
                findPost.Title = post.Title;
                findPost.Description = post.Description;

                context.Update(findPost);
                context.SaveChanges();
                return findPost;

            }
            else
            {
                throw new Exception("такого поста нет");
            }
            

        }




        public async Task DeletePost(int idPost)
        {
            var emailIden = _http.HttpContext?
                .User?
                .Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value!;

            var person = context.Users.FirstOrDefault(u => u.Email == emailIden);

            if (person == null)
            {
                throw new Exception("Такого пользователя не существует");
            }

            var findPost = context.Posts.FirstOrDefault(p => p.Id == idPost && person.Id == p.UserId);

            if (findPost != null)
            {
                context.Posts.Remove(findPost);
                context.SaveChanges();
              

            } 
            else

            {
               throw new Exception ("Такого поста не существует");
            }
            


        }

    }
}
