using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace Study.Common.IdentityServer
{
    
    public abstract class AbstractDataSeeder : ITransientDependency
    {
     
        private readonly IApiResourceRepository apiResourceRepository;
        private readonly IClientRepository clientRepository;
        private readonly IIdentityResourceDataSeeder identityResourceDataSeeder;
        private readonly IIdentityResourceRepository identityResourceRepository;
        private readonly IGuidGenerator guidGenerator;
        private readonly IPermissionDataSeeder permissionDataSeeder;

        protected static readonly string secret = "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=";//secret

        #region 构造函数
        public AbstractDataSeeder(IClientRepository clientRepository,
            IApiResourceRepository apiResourceRepository,
            IIdentityResourceRepository identityResourceRepository,
            IGuidGenerator guidGenerator,
            IIdentityResourceDataSeeder identityResourceDataSeeder
            ,IPermissionDataSeeder permissionDataSeeder
            )
        {
            this.clientRepository = clientRepository;
            this.apiResourceRepository = apiResourceRepository;
            this.identityResourceRepository = identityResourceRepository;
            this.guidGenerator = guidGenerator;
            this.identityResourceDataSeeder = identityResourceDataSeeder;
            this.permissionDataSeeder = permissionDataSeeder;
        }
        #endregion

        #region 获取ApiUserClaims
        public static string[] GetApiUserClaims()
        {
           return new[]
            {
                "test",
                "email",
                "email_verified",
                "name",
                "phone_number",
                "phone_number_verified",
                "role"
            };
           
        }
        #endregion

        #region 获取Scopes
        public static string[] GetScopes()
        {

            return  new[]
            {
                "email",
                "openid",
                "profile",
                "role",
                "phone",
                "address"
            };
        }
        #endregion

        #region 创建ApiResource
        public async Task<ApiResource> CreateApiResourceAsync( string name, IEnumerable<string> claims)
        {
            var apiResource = await apiResourceRepository.FindByNameAsync(name);
            if (apiResource == null)
            {
                apiResource = await apiResourceRepository.InsertAsync(
                    new ApiResource(guidGenerator.Create(),name,name + " API"),
                    autoSave: true
                );
            }

            foreach (var claim in claims)
            {
                if (apiResource.FindClaim(claim) == null)
                {
                    apiResource.AddUserClaim(claim);
                }
            }

            return await apiResourceRepository.UpdateAsync(apiResource);
        }
        #endregion

        #region 创建Client
        public async Task<Client> CreateClientAsync(string name,IEnumerable<string> scopes,IEnumerable<string> grantTypes,
           string secret,
           string redirectUri = null,
           string postLogoutRedirectUri = null,
           IEnumerable<string> permissions = null)
        {
            var client = await clientRepository.FindByCliendIdAsync(name);
            if (client == null)
            {
                client = await clientRepository.InsertAsync(
                    new Client(  guidGenerator.Create(), name )
                    {
                        ClientName = name,
                        ProtocolType = "oidc",
                        Description = name,
                        AlwaysIncludeUserClaimsInIdToken = true,
                        AllowOfflineAccess = true,
                        AbsoluteRefreshTokenLifetime = 31536000, //365 days
                        AccessTokenLifetime = 31536000, //365 days
                        AuthorizationCodeLifetime = 300,
                        IdentityTokenLifetime = 300,
                        RequireConsent = false
                    },
                    autoSave: true
                );
            }

            foreach (var scope in scopes)
            {
                if (client.FindScope(scope) == null)
                {
                    client.AddScope(scope);
                }
            }

            foreach (var grantType in grantTypes)
            {
                if (client.FindGrantType(grantType) == null)
                {
                    client.AddGrantType(grantType);
                }
            }

            if (client.FindSecret(secret) == null)
            {
                client.AddSecret(secret);
            }

            if (redirectUri != null)
            {
                if (client.FindRedirectUri(redirectUri) == null)
                {
                    client.AddRedirectUri(redirectUri);
                }
            }

            if (postLogoutRedirectUri != null)
            {
                if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
                {
                    client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
                }
            }

            if (permissions != null)
            {
                //await permissionDataSeeder.SeedAsync(
                //    ClientPermissionValueProvider.ProviderName,
                //    name,
                //    permissions
                //);
            }

            return await clientRepository.UpdateAsync(client);
        }

        #endregion


        #region 创建IdentityResources
        public async Task CreateIdentityResourcesAsync(string name, IEnumerable<string> claims)
        {
            var identityResource = new IdentityResource(guidGenerator.Create(), name, $"{name} Identity", required: true);
            foreach (var claim in claims)
            {
                if (identityResource.FindUserClaim(claim) == null)
                {
                    identityResource.AddUserClaim(claim);
                }
            }
          
            await identityResourceRepository.InsertAsync(identityResource);
        }
        #endregion

        #region 创建标准的IdentityResources
        public async Task CreateStandardResourcesAsync()
        {
             await identityResourceDataSeeder.CreateStandardResourcesAsync();
        }
        #endregion


        [UnitOfWork]
        public   abstract  Task SeedAsync();
    }
}
