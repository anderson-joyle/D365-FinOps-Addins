using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.AX.Metadata.Core.MetaModel;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Classes;

using Metadata = Microsoft.Dynamics.AX.Metadata;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

using Decorating;

namespace Building
{
    public class ClassDevDocEngine
    {
        #region Member variables
        protected ClassItem classItem;
        #endregion

        #region Properties
        /// <summary>
        /// Backing field for the metadata provider that is useful for retrieving
        /// metadata that is not loaded in the VS instance.
        /// </summary>
        private Metadata.Providers.IMetadataProvider metadataProvider = null;
        private Metadata.Service.IMetaModelService metaModelService = null;
        private ModelSaveInfo modelSaveInfo = null;

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

        public ModelSaveInfo ModelSaveInfo
        {
            get
            {
                ModelInfo modelInfo;

                if (this.modelSaveInfo == null)
                {
                    modelInfo = this.MetaModelService.GetClassModelInfo(this.classItem.Name).FirstOrDefault<ModelInfo>();

                    modelSaveInfo = new ModelSaveInfo(modelInfo);
                }

                return modelSaveInfo;
            }
        }
        #endregion

        public ClassDevDocEngine(ClassItem classItem)
        {
            this.classItem = classItem;
        }

        public void run()
        {
            AxClass axClass = MetadataProvider.Classes.Read(classItem.Name);

            bool allMethodsDocumented = true; // Please do not disappoint me! :)

            foreach (AxMethod method in axClass.Methods)
            {
                IContent tagContent = null;
                string devDoc = string.Empty;

                if (!method.Source.Contains("<summary>"))
                {
                    tagContent = new SummaryTag(method);
                    tagContent = new ParamTag(tagContent, method);
                    tagContent = new ExceptionTag(tagContent, method);
                    tagContent = new ReturnsTag(tagContent, method);
                    tagContent = new RemarksTag(tagContent, method);

                    method.Source = method.Source.Insert(0, $"{tagContent.getContent()}\n");
                    
                    allMethodsDocumented = false; // Shame on you! :(
                }
            }

            if (allMethodsDocumented)
            {
                CoreUtility.DisplayInfo("All your methods are documented already! I've transfered $500,00 to your bank account as reward!");
            }
            else
            {
                this.MetaModelService.UpdateClass(axClass, this.ModelSaveInfo);
            }
        }
    }
}
