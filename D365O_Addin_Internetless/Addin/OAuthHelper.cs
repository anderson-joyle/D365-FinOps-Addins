using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addin
{
    public class OAuthHelper
    {
        /// <summary>
        /// The header to use for OAuth authentication.
        /// </summary>
        public const string OAuthHeader = "Authorization";

        /// <summary>
        /// Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for the Web API call.</returns>
        public static string GetAuthenticationHeader(ClientConfiguration curConfiguration)
        {
            string aadTenant = curConfiguration.ActiveDirectoryTenant;
            string aadClientAppId = curConfiguration.ActiveDirectoryClientAppId;
            string aadClientAppSecret = curConfiguration.ActiveDirectoryClientAppSecret;
            string aadResource = curConfiguration.ActiveDirectoryResource;

            AuthenticationContext authenticationContext = new AuthenticationContext(aadTenant, false);
            AuthenticationResult authenticationResult;

            // OAuth through username and password.
            string username = curConfiguration.UserName;
            string password = curConfiguration.Password;

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Failed OAuth by empty password.");
            }

            try
            {
                // Get token object
                var userCredential = new UserPasswordCredential(username, password); ;
                authenticationResult = authenticationContext.AcquireTokenAsync(aadResource, aadClientAppId, userCredential).Result;
            }
            catch (Exception ex)
            {
                string ret = string.Empty;

                if (ex is AggregateException)
                {
                    AggregateException multiEx = ex as AggregateException;

                    foreach (Exception exception in multiEx.InnerExceptions)
                    {
                        ret += exception.Message + "\n";
                    }
                }
                else
                {
                    ret = ex.Message;
                }

                throw new Exception(ret);
            }

            // Create and get JWT token
            return authenticationResult.CreateAuthorizationHeader();
        }
    }
}
