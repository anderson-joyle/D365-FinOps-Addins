namespace Addin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using Microsoft.Dynamics.Framework.Tools.Extensibility;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

    using Networking;
    using Joking;


    /// <summary>
    /// Under presure? Take this dad joke and motivational quote to keep on going.
    /// </summary>
    [Export(typeof(IMainMenu))]
    public class MainMenuAddIn : MainMenuBase
    {
        #region Member variables
        private const string addinName = "OperationsAddin2";
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
            try
            {
                DadJoke joke = DadJoke.construct(Request.dadJoke());
                MotivationalQuote quote = MotivationalQuote.construct(Request.motivationalQuote());

                string message = string.Empty;

                if (joke.Id != null)
                {
                    //message += string.Format("Dad joke:\n{0}", joke.Joke);
                    message += joke.Joke;
                }

                //if (quote.Id != 0)
                //{
                //    message += "\n\n";
                //    message += string.Format("Motivational quote:\n{0}{1}", HttpUtility.HtmlDecode(quote.Content), quote.Title.ToUpper());
                //}

                CoreUtility.DisplayInfo(message);
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }
        #endregion
    }
}
