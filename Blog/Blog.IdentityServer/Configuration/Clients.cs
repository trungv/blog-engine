﻿using IdentityServer4.Models;
using System.Collections.Generic;

namespace Blog.IdentityServer.Configuration
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client> {
            new Client {
                ClientId = "oauthClient",
                ClientName = "Example Client Credentials Client Application",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = new List<Secret> {
                    new Secret("superSecretPassword".Sha256())},
                AllowedScopes = new List<string> {"customAPI.read"}
            }
        };
        }
    }
}
