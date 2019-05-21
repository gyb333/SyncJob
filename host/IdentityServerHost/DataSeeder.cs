 
using Study.Common.IdentityServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.PermissionManagement;
using static IdentityModel.OidcConstants;

namespace IdentityServerHost
{
    public class DataSeeder : AbstractDataSeeder
    {
        public DataSeeder(IClientRepository clientRepository,
                IApiResourceRepository apiResourceRepository,
                IIdentityResourceRepository identityResourceRepository,
                IGuidGenerator guidGenerator,
                    IIdentityResourceDataSeeder identityResourceDataSeeder
            //, IPermissionDataSeeder permissionDataSeeder
            ) : 
            base(clientRepository, apiResourceRepository , identityResourceRepository, 
                guidGenerator, identityResourceDataSeeder,null// permissionDataSeeder
                )
        {

        }
        public override async Task SeedAsync()
        {
            await CreateStandardResourcesAsync();
            await CreateApiResourcesAsync();
            await CreateClientsAsync();
        }

        private async Task CreateApiResourcesAsync()
        {
            await CreateApiResourceAsync("api", new string[] { "email", "role" });

            var apiUserClaims = GetApiUserClaims();
            await CreateApiResourceAsync("IdentityService", apiUserClaims);
            await CreateApiResourceAsync("ProductService", apiUserClaims);
            await CreateApiResourceAsync("InternalGateway", apiUserClaims);
            await CreateApiResourceAsync("BackendAdminAppGateway", apiUserClaims);
            await CreateApiResourceAsync("PublicWebSiteGateway", apiUserClaims);
           
        }

        private async Task CreateClientsAsync()
        {
            var commonScopes = GetScopes();
            await CreateClientAsync("console-client",
                new[] { "IdentityService", "InternalGateway", "ProductService" },
                new[] { "client_credentials", GrantTypes.Password }, 
                secret,
                permissions: new[] { "IdentityPermissions.Users", "ProductPermissions.Products" }
            );
            
            await CreateClientAsync("backend-admin-app-client",
                commonScopes.Union(new[] { "BackendAdminAppGateway", "IdentityService", "ProductService" }),
                new[] { "hybrid" },
                secret,
                redirectUri: "http://localhost:51000/signin-oidc",
                postLogoutRedirectUri: "http://localhost:51000/signout-callback-oidc"
            );

            await CreateClientAsync("public-website-client",
                commonScopes.Union(new[] { "PublicWebSiteGateway",  "ProductService" }),
                new[] { "hybrid" },
                secret,
                redirectUri: "http://localhost:52000/signin-oidc",
                postLogoutRedirectUri: "http://localhost:52000/signout-callback-oidc"
            );

            await CreateClientAsync("service-client",
                new[] { "InternalGateway", "IdentityService" },
                new[] { "client_credentials" },
                secret,
                permissions: new[] { "IdentityPermissions.UserLookup" }
            );
        }

    }
}
