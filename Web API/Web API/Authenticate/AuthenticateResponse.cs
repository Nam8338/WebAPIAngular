using System;
using Web_API.Models;

namespace Web_API.Authenticate
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        
        public AuthenticateResponse(Account account, string token)
        {
            Id = account.Id;
            Username = account.Username;
            Token = token;
        }
    }
}
