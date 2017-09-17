using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.Labels;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem;

namespace Labeling
{
    public class LabelManager
    {
        protected const string PREFIX = "@@@";
        protected const string LABELFILEID = "MyLabel"; // TODO: change to your label file name
        private Logging.Logging logging { get; }

        private List<AxLabelFile> labelFiles;

        protected EnvDTE.DTE dte;
        protected VSApplicationContext context;

        public LabelManager()
        {
            this.logging = new Logging.Logging();
            this.labelFiles = new List<AxLabelFile>();
            
            this.dte = CoreUtility.ServiceProvider.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
            this.context = new VSApplicationContext(dte);

            // Todo Declarer all labels files here
            // Initialize label files (each languages)
            // Most likely there is a better way to get the AxLabelFile elements.
            // This works only if the label files are in the same model than the other elements.
            // Use LabelManager.model(modelname) otherwise.
            this.labelFiles.Add(LabelManager.currentModel().GetLabelFile($"{LABELFILEID}_{"en-GB"}"));
            this.labelFiles.Add(LabelManager.currentModel().GetLabelFile($"{LABELFILEID}_{"en-US"}"));
            
        }

        /// <summary>
        /// Checks if the label should be created, based on the prefix @@@
        /// </summary>
        /// <param name="propertyText">Label string</param>
        /// <returns>true/false</returns>
        protected bool isPrefixed(string propertyText)
        {
            return propertyText.StartsWith(PREFIX);
        }

        /// <summary>
        /// If property text is prefixed, then create a new label.
        /// Otherwise, return the very same property text value
        /// </summary>
        /// <param name="propertyText">Label text from element property (label, help text, caption, etc)</param>
        /// <returns>The new label id created</returns>
        public string createLabel(string propertyText)
        {
            string ret = propertyText;

            if (this.isPrefixed(propertyText))
            {
                string labelId = LabelManager.getLabelId(propertyText);
                string label = LabelManager.getLabel(propertyText);

                foreach (AxLabelFile labelfile in this.labelFiles)
                {
                    LabelControllerFactory factory = new LabelControllerFactory();
                    LabelEditorController labelEditorController = factory.GetOrCreateLabelController(labelfile, context);

                    if (!labelEditorController.Exists(labelId))
                    {
                        labelEditorController.Insert(labelId, label, string.Empty);
                        labelEditorController.Save();

                        this.log(labelId, label, labelfile.Name);
                    }
                }

                ret = $"@{LABELFILEID}:{labelId}";
            }

            return ret;
        }

        /// <summary>
        /// Add log message
        /// </summary>
        /// <param name="labelId">Label id</param>
        /// <param name="label">Label text</param>
        /// <param name="labelFileName">Label file name</param>
        private void log(string labelId, string label, string labelFileName)
        {
            Logging.Log singleLog = new Logging.Log();

            singleLog.labelId = labelId;
            singleLog.label = label;
            singleLog.labelFile = labelFileName;

            this.logging.add(singleLog);
        }

        /// <summary>
        /// Extracts the label id from label string property
        /// </summary>
        /// <param name="propertyText">Label string e.g. @@@MyNewLabelId=My new label id</param>
        /// <returns>The label string e.g. MyNewLabelId</returns>
        public static string getLabelId(string propertyText)
        {
            string ret = string.Empty;

            if (propertyText.Substring(PREFIX.Length).Split('=').Length > 1)
            {
                ret = propertyText.Substring(PREFIX.Length).Split('=')[0].Replace(" ", string.Empty);
            }
            else
            {
                ret = propertyText.Substring(PREFIX.Length).Replace(" ", string.Empty);
            }

            return ret;
        }

        /// <summary>
        /// Extracts the label from label string property
        /// </summary>
        /// <param name="propertyText">Label string e.g. @@@MyNewLabelId=My new label id</param>
        /// <returns>The label string e.g. My label id</returns>
        public static string getLabel(string propertyText)
        {
            string ret = string.Empty;

            if (propertyText.Substring(PREFIX.Length).Split('=').Length > 1)
            {
                ret = propertyText.Substring(PREFIX.Length).Split('=')[1];
            }
            else
            {
                ret = propertyText.Substring(PREFIX.Length);
            }

            return ret;
        }

        /// <summary>
        /// Get formated log message
        /// </summary>
        /// <returns>Log message</returns>
        public string getLoggingMessage()
        {
            return this.logging.getLogging();
        }

        /// <summary>
        /// Used to find metadata on current model
        /// </summary>
        /// <returns>MetaModelService instance</returns>
        /// <remarks>For sure there is a better way to do it</remarks>
        static protected Microsoft.Dynamics.AX.Metadata.Service.IMetaModelService currentModel()
        {
            var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            var metaModelService = metaModelProviders.CurrentMetaModelService;

            return metaModelService;
        }

        /// <summary>
        /// Used to find metadata on model (by model name)
        /// </summary>
        /// <param name="modelName">Model name</param>
        /// <returns>MetaModelService instance</returns>
        /// /// <remarks>For sure there is a better way to do it</remarks>
        static protected Microsoft.Dynamics.AX.Metadata.Service.IMetaModelService model(string modelName)
        {
            var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            var metaModelService = metaModelProviders.GetMetaModelService(modelName);

            return metaModelService;
        }
    }
}
