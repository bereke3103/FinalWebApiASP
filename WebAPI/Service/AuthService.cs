using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Service.Interface;

namespace WebAPI.Service
{
    public class AuthService : IAuth
    {
        public AuthService(DataContext context, IConfiguration configuration)
        {
            Context = context;
            this.configuration = configuration;
        }

        public DataContext Context;
        private readonly IConfiguration configuration;

        public async Task<string> Register(UserRegisterModel user)
        {

            var emailResponse = await Context.Users.FirstOrDefaultAsync(e => e.Email == user.Email);

            if (emailResponse?.Email != null)
            {
                throw new Exception("Такой пользователь уже существует");
            };

            UserModel userModel = new UserModel()
            {
                Name = user.Name,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            await Context.AddAsync(userModel);
            await Context.SaveChangesAsync();


            return "Регистрация прошла успешна";



        }

        public async Task<Token> Authentication(UserAuthenModel user)
        {
            var response = await Context.Users.FirstOrDefaultAsync(u=> u.Email == user.Email);

            if (response?.Email != user.Email)
            {
                throw new Exception("Неверный логин");
            }

            if (!BCrypt.Net.BCrypt.Verify(user.Password, response.Password))
            {
                throw new Exception("Неверный пароль");
            }

            Token token = CreateToken(user);
            return token;
        }

        public Token CreateToken(UserAuthenModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration
                .GetSection("AppSettings:Key").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);


            var response = new Token()
            {
                access_token = jwtToken,
                username = user.Email
            };


            return response;
        }
    }
}
