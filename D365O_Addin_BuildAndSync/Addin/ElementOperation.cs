using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.Framework.Tools.BuildTasks;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Views;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Classes;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;

using Metadata = Microsoft.Dynamics.AX.Metadata;

using Element;

namespace Operation
{
    /// <summary>
    /// Element operator interface
    /// </summary>
    /// <remarks>Visitor class</remarks>
    public interface IElementOperation
    {
        void visitTable(Table table, ElementTable element);
        void visitTableExtension(TableExtension tableExtension, ElementTableExtension element);
        void visitView(View table, ElementView element);
        void visitClass(ClassItem classItem, ElementClass element);
        void visitSimpleQuery(SimpleQuery simpleQuery, ElementSimpleQuery element);
        void visitCompositeQuery(CompositeQuery compositeQuery, ElementCompositeQuery element);
        void visitForm(Form form, ElementForm element);
        void visitFormExtension(FormExtension formExtension, ElementFormExtension element);
        void visitDataEntity(DataEntityViewBase dataEntity, ElementDataEntity element);
    }

    abstract public class ElementOperation : IElementOperation
    {
        #region Member variables
        protected Metadata.Providers.IMetadataProvider metadataProvider = null;
        protected BuildOperation buildOperation;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a singleton instance of the metadata provider that can access the metadata repository.
        /// Any metadata, irrespective of whether it is part of what is being edited by VS, is available
        /// through this provider.
        /// </summary>
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
        #endregion

        #region Methods
        /*
         * To implement new visit methods, copy and paste the following method body template:
         

            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).Get<ELEMENT_TYPE>ModelInfo(<NAMEDELEMENT>.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.run(modelInfo,
                     Metadata.Extensions.CanonicalForm.ModelElementType.<ELEMENT_TYPE>,
                     <NAMEDELEMENT>.Name);
            
        */
        public void visitTable(Table table, ElementTable element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetTableModelInfo(table.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.run(modelInfo,
                     Metadata.Extensions.CanonicalForm.ModelElementType.Table,
                     table.Name);
        }

        public void visitTableExtension(TableExtension tableExtension, ElementTableExtension element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetTableExtensionModelInfo(tableExtension.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.run(modelInfo,
                     Metadata.Extensions.CanonicalForm.ModelElementType.TableExtension,
                     tableExtension.Name);
        }

        public void visitView(View view, ElementView element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetViewModelInfo(view.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.run(modelInfo,
                     Metadata.Extensions.CanonicalForm.ModelElementType.View,
                     view.Name);
        }

        public void visitClass(ClassItem classItem, ElementClass element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetClassModelInfo(classItem.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.run(modelInfo,
                     Metadata.Extensions.CanonicalForm.ModelElementType.Class,
                     classItem.Name);
        }

        public void visitSimpleQuery(SimpleQuery simpleQuery, ElementSimpleQuery element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetQueryModelInfo(simpleQuery.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.run(modelInfo,
                     Metadata.Extensions.CanonicalForm.ModelElementType.Query,
                     simpleQuery.Name);
        }

        public void visitCompositeQuery(CompositeQuery compositeQuery, ElementCompositeQuery element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetQueryModelInfo(compositeQuery.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.run(modelInfo,
                     Metadata.Extensions.CanonicalForm.ModelElementType.Query,
                     compositeQuery.Name);
        }

        public void visitForm(Form form, ElementForm element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetFormModelInfo(form.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.run(modelInfo,
                     Metadata.Extensions.CanonicalForm.ModelElementType.Form,
                     form.Name);
        }

        public void visitFormExtension(FormExtension formExtension, ElementFormExtension element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetFormExtensionModelInfo(formExtension.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.run(modelInfo,
                     Metadata.Extensions.CanonicalForm.ModelElementType.FormExtension,
                     formExtension.Name);
        }

        public void visitDataEntity(DataEntityViewBase dataEntity, ElementDataEntity element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetDataEntityViewModelInfo(dataEntity.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.run(modelInfo,
                     Metadata.Extensions.CanonicalForm.ModelElementType.DataEntityView,
                     dataEntity.Name);
        }

        private async void run(Metadata.MetaModel.ModelInfo modelInfo, Metadata.Extensions.CanonicalForm.ModelElementType elementType, string elementName)
        {
            bool result = await BuildElement(modelInfo, elementType, elementName);
        }

        private Task<bool> BuildElement(Metadata.MetaModel.ModelInfo modelInfo, Metadata.Extensions.CanonicalForm.ModelElementType elementType, string elementName)
        {
            AsyncBuildController buildController;
            List<ModelElementCompilationDescriptor> descriptors = new List<ModelElementCompilationDescriptor>();

            descriptors.Add(new ModelElementCompilationDescriptor(elementType, elementName));

            buildController = new AsyncBuildController(buildOperation, modelInfo);            

            return Task.FromResult(buildController.ProcessBuild(descriptors));
        }
        #endregion
    }

    public class BuildElementOperation : ElementOperation
    {
        public BuildElementOperation()
        {
            buildOperation = BuildOperation.XppValidationAndIL;
        }
    }

    public class SyncElementOperation : ElementOperation
    {
        public SyncElementOperation()
        {
            buildOperation = BuildOperation.DBSynchronization;
        }
    }
}
