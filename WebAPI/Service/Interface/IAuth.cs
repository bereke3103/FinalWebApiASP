using WebAPI.Models;

namespace WebAPI.Service.Interface
{
    public interface IAuth
    {
        public Task<string> Register(UserRegisterModel user);

        public Task<Token> Authentication(UserAuthenModel user);
    }
}
