using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4;
using System.Security.Claims;

namespace IdentityServer.Configuration
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResource("roles", "User role(s)", new List<string> { "role" })
          };

        //public static List<TestUser> GetUsers() =>
        //  new List<TestUser>
        //  {
        //      new TestUser
        //      {
        //          SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b9",
        //          Username = "kanishkashm",
        //          Password = "Kanishka@1988",
        //          Claims = new List<Claim>
        //          {
        //              new Claim("given_name", "Kanishka"),
        //              new Claim("family_name", "Mahanama"),
        //              new Claim("address", "Gampaha"),
        //              new Claim("role", "Admin")
        //          }
        //      },
        //      new TestUser
        //      {
        //          SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b2",
        //          Username = "thenulash",
        //          Password = "thenul@1998",
        //          Claims = new List<Claim>
        //          {
        //              new Claim("given_name", "Thenul"),
        //              new Claim("family_name", "Mahanama"),
        //              new Claim("address", "Gampaha"),
        //              new Claim("role", "User")
        //          }
        //      },
        //      new TestUser
        //      {
        //          SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b3",
        //          Username = "chanuliAyash",
        //          Password = "chanuli@1995",
        //          Claims = new List<Claim>
        //          {
        //              new Claim("given_name", "Chanuli"),
        //              new Claim("family_name", "Mahanama"),
        //              new Claim("address", "Gampaha"),
        //              new Claim("role", "Auditor")
        //          }
        //      },
        //      new TestUser
        //      {
        //          SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
        //          Username = "manuliAnish",
        //          Password = "manuli@1995",
        //          Claims = new List<Claim>
        //          {
        //              new Claim("given_name", "Manuli"),
        //              new Claim("family_name", "Mahanama"),
        //              new Claim("address", "Gampaha"),
        //              new Claim("role", "Auditor")
        //          }
        //      }
        //  };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
               new Client
               {
                    ClientId = "eStoker",
                    ClientSecrets = new [] { new Secret("eStokerSecret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "eStokerApi", "roles" }
                },
                new Client
                {
                    ClientName = "Angular-Client",
                    ClientId = "angular-client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string>{ "http://localhost:4200/signin-callback", "http://localhost:4200/assets/silent-callback.html" },
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "eStokerApi",
                        "roles"
                    },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    RequireClientSecret = false,
                    PostLogoutRedirectUris = new List<string> { "http://localhost:4200/signout-callback" },
                    RequireConsent = false,
                    AccessTokenLifetime = 600
                },
                new Client
                {
                    ClientName = "Postman",
                    ClientId = "postman",
                    RequirePkce = true,
                    Enabled = true,

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "eStokerApi",
                        "roles"
                    }
                },
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope> { new ApiScope("eStokerApi", "eStoker API") };

        public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
            new ApiResource("eStokerApi", "eStoker API")
            {
                Scopes = { "eStokerApi" }
            }
        };
    }
}
