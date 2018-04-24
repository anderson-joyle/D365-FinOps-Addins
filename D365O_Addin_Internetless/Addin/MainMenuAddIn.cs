namespace Addin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using Microsoft.Dynamics.Framework.Tools.Extensibility;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Web.Configuration;

    /// <summary>
    /// Log in into AX with no internet access.
    /// </summary>
    [Export(typeof(IMainMenu))]
    public class MainMenuAddIn : MainMenuBase
    {
        #region Member variables
        private const string addinName = "Addin";
        #endregion

        #region Properties
        /// <summary>
        /// Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get
            {
                return AddinResources.MainMenuAddInCaption;
            }
        }

        /// <summary>
        /// Unique name of the add-in
        /// </summary>
        public override string Name
        {
            get
            {
                return MainMenuAddIn.addinName;
            }
        }

        #endregion

        #region Callbacks
        /// <summary>
        /// Called when user clicks on the add-in menu
        /// </summary>
        /// <param name="e">The context of the VS tools and metadata</param>
        public override void OnClick(AddinEventArgs e)
        {
            string aosServicePath = string.Empty;
            string webConfigPath = string.Empty;

            string resource = string.Empty;
            string clientId = string.Empty;
            string tenant = string.Empty;

            try
            {
                if (!Directory.Exists(AddinResources.AOSServicePath))
                {
                    throw new Exception($"Directory '{AddinResources.AOSServicePath}' does not exist.");
                }

                webConfigPath = $"{AddinResources.AOSServicePath}web.config";

                //webConfigPath = VirtualPathUtility.RemoveTrailingSlash(webConfigPath);

                if (!File.Exists(webConfigPath))
                {
                    throw new Exception($"File '{webConfigPath}' does not exist.");
                }
                
                var map = new ExeConfigurationFileMap { ExeConfigFilename = webConfigPath };
                var configFile = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

                resource = configFile.AppSettings.Settings["Infrastructure.HostUrl"].Value;
                clientId = configFile.AppSettings.Settings["Aad.TrustedServiceAppIds"].Value;
                tenant = configFile.AppSettings.Settings["Aad.AADTenantId"].Value;

                if (resource == string.Empty ||
                    clientId == string.Empty ||
                    tenant == string.Empty)
                {
                    throw new Exception($"Missing KeyValue values.");
                }

                ClientConfiguration clientConfiguration = new ClientConfiguration()
                {
                    UriString = resource,
                    UserName = "anderson.ferreira@kpmgdynamics.co.uk",
                    Password = "=>=>tyuUJMmnbBGT099",
                    ActiveDirectoryResource = resource.TrimEnd('/'),
                    ActiveDirectoryTenant = $"https://login.windows.net/{tenant}/wsfed",
                    ActiveDirectoryClientAppId = clientId,
                    ActiveDirectoryClientAppSecret = "",
                    TLSVersion = ""
                };

                var header = OAuthHelper.GetAuthenticationHeader(clientConfiguration);

                CoreUtility.DisplayInfo(header.ToString());
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }
        #endregion
    }
}
