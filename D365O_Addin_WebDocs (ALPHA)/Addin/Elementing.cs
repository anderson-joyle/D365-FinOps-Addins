using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.Framework.Tools.MetaModel;
using Microsoft.Dynamics.Framework.Tools.Integration.Interfaces;
using ProjectSystem = Microsoft.Dynamics.Framework.Tools.ProjectSystem;

using Metadata = Microsoft.Dynamics.AX.Metadata;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

using Formatting;

namespace Elementing
{
    public class SingleElement
    {
        #region Methods
        private string name = string.Empty;
        private string type = string.Empty;
        private IVsMetadataReference metadataReference = null;
        #endregion

        #region Properties
        public IVsMetadataReference MetadataReference
        {
            set
            {
                this.metadataReference = value;
            }
            get
            {
                return this.metadataReference;
            }
        }

        public string Name
        {
            set
            {
                this.name = value;
            }
            get
            {
                return this.name;
            }
        }

        public string Type
        {
            set
            {
                this.type = value;
            }
            get
            {
                return this.type;
            }
        }
        #endregion
    }

    public class SolutionElement
    {
        #region Methods
        private string name = string.Empty;
        private List<ProjectSystem.OAVSProject> projects = new List<ProjectSystem.OAVSProject>();
        #endregion

        #region Properties
        public string Name
        {
            set
            {
                this.name = value;
            }
            get
            {
                return this.name;
            }
        }

        public List<ProjectSystem.OAVSProject> Projects
        {
            get
            {
                if (projects == null)
                {
                    projects = new List<ProjectSystem.OAVSProject>();
                }

                return this.projects;
            }
        }
        #endregion
    }

    public class AxMetadataHelper
    {
        #region Properties
        /// <summary>
        /// Backing field for the metadata provider that is useful for retrieving
        /// metadata that is not loaded in the VS instance.
        /// </summary>
        private Metadata.Providers.IMetadataProvider metadataProvider = null;
        private Metadata.Service.IMetaModelService metaModelService = null;

        public Metadata.Providers.IMetadataProvider MetadataProvider
        {
            get
            {
                if (this.metadataProvider == null)
                {
                    this.metadataProvider = DesignMetaModelService.Instance.CurrentMetadataProvider;
                }
                return this.metadataProvider;
            }
        }

        public Metadata.Service.IMetaModelService MetaModelService
        {
            get
            {
                if (this.metaModelService == null)
                {
                    this.metaModelService = DesignMetaModelService.Instance.CurrentMetaModelService;
                }
                return this.metaModelService;
            }
        }
        #endregion

        public AxMetadataHelper(string name)
        {

        }

        protected string ResolveLabel(string label)
        {
            var labelResolver = CoreUtility.ServiceProvider.GetService(typeof(ILabelResolver)) as ILabelResolver;

            if (labelResolver != null)
            {
                return labelResolver.GetLabelText(label);
            }
            return label;
        }
    }

    public class AxTableHelper : AxMetadataHelper
    {
        protected AxTable table = null;

        public string DevDocument
        {
            get
            {
                return this.ResolveLabel(this.table.DeveloperDocumentation);
            }
        }

        public string Label
        {
            get
            {
                return this.ResolveLabel(this.table.Label);
            }
        }

        public string FormattedFields
        {
            get
            {
                string htmlFields = string.Empty;

                foreach (AxTableField field in this.table.Fields)
                {
                    AxEdt edt = this.MetadataProvider.Edts.Read(field.ExtendedDataType);

                    string properties = string.Empty;
                    string labelHelpText = string.Empty;
                    string label = string.Empty;
                    string helpText = string.Empty;
                    string edtOrEnumType = string.Empty;
                    string edtOrEnumTypeValue = string.Empty;

                    #region Label and help text solving
                    if (field.Label != string.Empty)
                    {
                        label = this.ResolveLabel(field.Label);
                    }
                    else
                    {
                        if (field.ExtendedDataType != string.Empty)
                        {
                            label = this.ResolveLabel(getEdtLabel(field.ExtendedDataType));
                        }

                        if (label == string.Empty && field is AxTableFieldEnum)
                        {
                            AxTableFieldEnum fieldEnum = field as AxTableFieldEnum;
                            AxEnum axEnum = this.MetadataProvider.Enums.Read(fieldEnum.EnumType);

                            if (axEnum.Label != string.Empty)
                            {
                                label = this.ResolveLabel(axEnum.Label);
                            }
                            else
                            {
                                label = "(empty)";
                            }
                        }
                    }

                    if (field.HelpText != string.Empty)
                    {
                        helpText = this.ResolveLabel(field.HelpText);

                    }
                    else
                    {
                        if (field.ExtendedDataType != string.Empty)
                        {
                            helpText = this.ResolveLabel(getEdtHelpText(field.ExtendedDataType));
                        }

                        if (helpText == string.Empty && field is AxTableFieldEnum)
                        {
                            AxTableFieldEnum fieldEnum = field as AxTableFieldEnum;
                            AxEnum axEnum = this.MetadataProvider.Enums.Read(fieldEnum.EnumType);

                            if (axEnum.HelpText != string.Empty)
                            {
                                helpText = this.ResolveLabel(axEnum.HelpText);
                            }
                            else
                            {
                                helpText = "(empty)";
                            }
                        }
                    }
                    #endregion

                    if (field is AxTableFieldEnum)
                    {
                        AxTableFieldEnum fieldEnum = field as AxTableFieldEnum;

                        if (fieldEnum.EnumType != string.Empty)
                        {
                            edtOrEnumType = "Enum type";
                            edtOrEnumTypeValue = fieldEnum.EnumType;
                        }
                        else
                        {
                            edtOrEnumType = "Extended data type";
                            edtOrEnumTypeValue = field.ExtendedDataType;
                        }
                    }
                    else
                    {
                        edtOrEnumType = "Extended data type";
                        edtOrEnumTypeValue = field.ExtendedDataType;
                    }

                    properties += $"Label: {label}<br>";
                    properties += $"HelpText: {helpText}<br>";
                    properties += $"{edtOrEnumType}: {edtOrEnumTypeValue}<br>";
                    properties += $"Mandatory: {field.Mandatory}<br>";

                    labelHelpText += $"Label: {label}<br>";
                    labelHelpText += $"HelpText: {helpText}<br>";

                    htmlFields += string.Format(HTMLContent.TableFieldsTag, field.Name, edt.Name, "AxEdt", labelHelpText, "YES"); 
                }

                return htmlFields;
            }
        }

        public AxTableHelper(string name) : base (name)
        {
            this.table = this.MetadataProvider.Tables.Read(name);
        }

        protected string getEdtLabel(string name)
        {
            AxEdt axEdt = this.MetadataProvider.Edts.Read(name);
            string ret = string.Empty;

            if (axEdt.Label != string.Empty)
            {
                ret = axEdt.Label;

            }
            else
            {
                if (axEdt.Extends != string.Empty)
                {
                    ret = this.getEdtLabel(axEdt.Extends);
                }
                else
                {
                    ret = "(empty)";
                }
            }

            return ret;
        }

        protected string getEdtHelpText(string name)
        {
            AxEdt axEdt = this.MetadataProvider.Edts.Read(name);
            string ret = string.Empty;

            if (axEdt.HelpText != string.Empty)
            {
                ret = axEdt.HelpText;
            }
            else
            {
                if (axEdt.Extends != string.Empty)
                {
                    ret = this.getEdtLabel(axEdt.Extends);
                }
                else
                {
                    ret = "(empty)";
                }
            }

            return ret;
        }
    }
}
