using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using Web_API.Authenticate;
using Web_API.Models;

namespace Web_API.Services
{
    public interface IAccountService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Account Add(Account account);
    }

    public class AccountService :  IAccountService
    {
        private readonly DBContext _dbContextStudent;
        private readonly AppSettings _appSettings;

        public AccountService(IOptions<AppSettings> appSettings, DBContext dbContextStudent)
        {
            _dbContextStudent = dbContextStudent;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var account = _dbContextStudent.Accounts.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (account == null)
                return null;
            var token = generateJwtToken(account);
            return new AuthenticateResponse(account, token);
        }

        public Account Add(Account account)
        {
            var addAccount = _dbContextStudent.Accounts.Add(new Account()
            {
                Username = account.Username,
                Password = account.Password,
                Confirmpassword = account.Confirmpassword
            });
            if (account.Password != account.Confirmpassword)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            }

            _dbContextStudent.SaveChanges();
            return addAccount.Entity;
        }

        private string generateJwtToken(Account account)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", account.Id.ToString()),
                    new Claim("username", account.Username.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
