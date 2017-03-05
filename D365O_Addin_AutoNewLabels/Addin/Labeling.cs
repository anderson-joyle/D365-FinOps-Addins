using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.Extensibility;

namespace Labeling
{
    public class LabelManager
    {
        protected const string PREFIX = "@@@";
        protected const string LABELFILEID = "LabelFileJYL"; // TODO: change to your label file name
        private Logging.Logging logging { get; }

        private List<AxLabelFile> labelFiles;
        private Dictionary<string,string> labelContents;

        public LabelManager()
        {
            this.logging = new Logging.Logging();
            this.labelFiles = new List<AxLabelFile>();
            this.labelContents = new Dictionary<string, string>();

            // Initialize label files (each languages)
            // Most likely there is a better way to get the AxLabelFile elements.
            // This works only if the label files are in the same model than the other elements.
            // Use CreateLabels.model(modelname) otherwise.
            this.labelFiles.Add(LabelManager.currentModel().GetLabelFile($"{LABELFILEID}_{"en-GB"}"));
            this.labelFiles.Add(LabelManager.currentModel().GetLabelFile($"{LABELFILEID}_{"en-US"}"));

            this.initContents();
        }

        private void initContents()
        {
            foreach (AxLabelFile labelfile in this.labelFiles)
            {
                var reader = new System.IO.StreamReader(labelfile.LocalPath());
                string content = reader.ReadToEnd();

                reader.Close();

                this.labelContents[labelfile.Name] = content;
            }
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
        /// If property text is prefixed, then create a new label. Otherwise, return the very same property text value
        /// </summary>
        /// <param name="propertyText">Label text from element property (label, help text, caption, etc)</param>
        /// <returns>The new label id created</returns>
        /// <remarks>TODO: This is messed up. Needs to prettify.</remarks>
        public string createLabel(string propertyText)
        {
            string ret = propertyText;

            if (this.isPrefixed(propertyText))
            {
                string labelId = LabelManager.getLabelId(propertyText);
                string label = LabelManager.getLabel(propertyText);

                foreach (AxLabelFile labelfile in this.labelFiles)
                {
                    if (!this.exist(labelId, labelfile.Name))
                    {
                        System.IO.StreamWriter writer = new System.IO.StreamWriter(labelfile.LocalPath(), true);

                        writer.WriteLine($"{labelId}={label}");
                        //writer.WriteLine($" ;");
                        writer.Close();

                        this.log(labelId, label, labelfile.Name);
                        this.add2LocalContent(labelId, labelfile.Name);
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
        /// Add the recently create label id to local label content
        /// </summary>
        /// <param name="labelId">Label id</param>
        /// <param name="labelFileName">Label file name</param>
        /// <remarks>The ideia is to simulate the creating of a new label into the file, w/o modifing the file for real.</remarks>
        private void add2LocalContent(string labelId, string labelFileName)
        {
            if (!this.exist(labelId, labelFileName))
            {
                this.labelContents[labelFileName] += labelId;
            }
        }

        /// <summary>
        /// Check if the label id already exist on label file
        /// </summary>
        /// <param name="labelId">Label id to check</param>
        /// <param name="labelFileName">Label file name</param>
        /// <returns>True if label id exists on local content variable</returns>
        /// <remarks>This method is public to make it available to test project</remarks>
        public bool exist(string labelId, string labelFileName)
        {
            bool ret = false;

            if (this.labelContents[labelFileName].Contains(($"{labelId}=")))
            {
                ret = true;
            }

            return ret;
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
