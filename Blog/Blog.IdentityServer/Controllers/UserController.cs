using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blog.IdentityServer.Configuration;
using Blog.IdentityServer.Model;
using IdentityModel.Client;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private async Task<TokenResponse> GetToken()
        {
            var client = new HttpClient();
            //var response = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = "https://localhost:44339/connect/token",
            //    ClientId = "oauthClient",
            //    ClientSecret = "superSecretPassword",
            //    Scope = "customAPI.read"
            //});

            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "http://localhost:9000/connect/token",

                ClientId = "oauthClient",
                ClientSecret = "superSecretPassword",
                Scope = "customAPI.read",

                UserName = "user",
                Password = "123"
            });

            return response;
        }

        // POST api/values
        [HttpPost]
        public async Task<UserLoginResponse> ValidateUser([FromBody] UserLogin user)
        {
            var check = Users.Get().Any(u => u.Username == user.Username && u.Password == user.Password);
            UserLoginResponse response = new UserLoginResponse();
            if (check)
            {
                var token = await GetToken();
                response.Success = true;
                response.Token = token.AccessToken;
                response.Error = token.Error + " " + token.ErrorDescription;
                return response;
            }
            response.Success = false;
            response.Token = "";
            return response;
        }
    }
}