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
using Microsoft.Dynamics.AX.Metadata;
using Microsoft.Dynamics.AX.Metadata.Providers;
using Microsoft.Dynamics.AX.Metadata.Service;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.AX.Metadata.Extensions.CanonicalForm;

namespace Operating
{
    public interface IOperation
    {
        void visitTable(Table table);
        void visitTableExtension(TableExtension tableExtension);
        void visitView(View table);
        void visitClass(ClassItem classItem);
        void visitSimpleQuery(SimpleQuery simpleQuery);
        void visitCompositeQuery(CompositeQuery compositeQuery);
        void visitForm(Form form);
        void visitFormExtension(FormExtension formExtension);
        void visitDataEntity(DataEntityViewBase dataEntity);
    }

    abstract public class Operation
    {
        protected BuildOperation operationType;

        #region Properties
        private IMetadataProvider metadataProvider = null;
        private IMetaModelService metaModelService = null;

        public IMetadataProvider MetadataProvider
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

        public IMetaModelService MetaModelService
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

        protected void run(ModelElementType type, INamedElement element, ModelInfo modelInfo)
        {
            AsyncBuildController controller = new AsyncBuildController(this.operationType, modelInfo);
            List<ModelElementCompilationDescriptor> descriptors = new List<ModelElementCompilationDescriptor>();

            descriptors.Add(new ModelElementCompilationDescriptor(type, element.Name));

            controller.ProcessBuild(descriptors);
        }
    }

    public class BPCheck : Operation, IOperation
    {
        public BPCheck()
        {
            operationType = BuildOperation.BPCheck;
        }

        public void visitTable(Table table)
        {
            this.run(
                ModelElementType.Table,
                table,
                this.MetaModelService.GetTableModelInfo(table.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitTableExtension(TableExtension tableExtension)
        {
            this.run(
                ModelElementType.TableExtension,
                tableExtension,
                this.MetaModelService.GetTableExtensionModelInfo(tableExtension.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitView(View view)
        {
            this.run(
                ModelElementType.View,
                view,
                this.MetaModelService.GetViewModelInfo(view.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitClass(ClassItem classItem)
        {
            this.run(
                ModelElementType.Class,
                classItem,
                this.MetaModelService.GetClassModelInfo(classItem.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitSimpleQuery(SimpleQuery simpleQuery)
        {
            this.run(
                ModelElementType.SimpleQuery,
                simpleQuery,
                this.MetaModelService.GetQueryModelInfo(simpleQuery.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitCompositeQuery(CompositeQuery compositeQuery)
        {
            this.run(
                ModelElementType.CompositeQuery,
                compositeQuery,
                this.MetaModelService.GetQueryModelInfo(compositeQuery.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitForm(Form form)
        {
            this.run(
                ModelElementType.Table,
                form,
                this.MetaModelService.GetFormModelInfo(form.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitFormExtension(FormExtension formExtension)
        {
            this.run(
                ModelElementType.FormExtension,
                formExtension,
                this.MetaModelService.GetFormExtensionModelInfo(formExtension.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitDataEntity(DataEntityViewBase dataEntity)
        {
            this.run(
                ModelElementType.DataEntityView,
                dataEntity,
                this.MetaModelService.GetDataEntityViewModelInfo(dataEntity.Name).FirstOrDefault<ModelInfo>());
        }
    }

    public class Build : Operation, IOperation
    {
        public Build()
        {
            operationType = BuildOperation.XppValidationAndIL;
        }

        public void visitTable(Table table)
        {
            this.run(
                ModelElementType.Table,
                table,
                this.MetaModelService.GetTableModelInfo(table.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitTableExtension(TableExtension tableExtension)
        {
            this.run(
                ModelElementType.TableExtension,
                tableExtension,
                this.MetaModelService.GetTableExtensionModelInfo(tableExtension.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitView(View view)
        {
            this.run(
                ModelElementType.View,
                view,
                this.MetaModelService.GetViewModelInfo(view.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitClass(ClassItem classItem)
        {
            this.run(
                ModelElementType.Class,
                classItem,
                this.MetaModelService.GetClassModelInfo(classItem.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitSimpleQuery(SimpleQuery simpleQuery)
        {
            this.run(
                ModelElementType.SimpleQuery,
                simpleQuery,
                this.MetaModelService.GetQueryModelInfo(simpleQuery.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitCompositeQuery(CompositeQuery compositeQuery)
        {
            this.run(
                ModelElementType.CompositeQuery,
                compositeQuery,
                this.MetaModelService.GetQueryModelInfo(compositeQuery.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitForm(Form form)
        {
            this.run(
                ModelElementType.Table,
                form,
                this.MetaModelService.GetFormModelInfo(form.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitFormExtension(FormExtension formExtension)
        {
            this.run(
                ModelElementType.FormExtension,
                formExtension,
                this.MetaModelService.GetFormExtensionModelInfo(formExtension.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitDataEntity(DataEntityViewBase dataEntity)
        {
            this.run(
                ModelElementType.DataEntityView,
                dataEntity,
                this.MetaModelService.GetDataEntityViewModelInfo(dataEntity.Name).FirstOrDefault<ModelInfo>());
        }
    }

    public class Sync : Operation, IOperation
    {
        public Sync()
        {
            operationType = BuildOperation.DBSynchronization;
        }

        public void visitTable(Table table)
        {
            this.run(
                ModelElementType.Table,
                table,
                this.MetaModelService.GetTableModelInfo(table.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitTableExtension(TableExtension tableExtension)
        {
            this.run(
                ModelElementType.TableExtension,
                tableExtension,
                this.MetaModelService.GetTableExtensionModelInfo(tableExtension.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitView(View view)
        {
            this.run(
                ModelElementType.View,
                view,
                this.MetaModelService.GetViewModelInfo(view.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitClass(ClassItem classItem)
        {
        }

        public void visitSimpleQuery(SimpleQuery simpleQuery)
        {
            this.run(
                ModelElementType.SimpleQuery,
                simpleQuery,
                this.MetaModelService.GetQueryModelInfo(simpleQuery.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitCompositeQuery(CompositeQuery compositeQuery)
        {
            this.run(
                ModelElementType.CompositeQuery,
                compositeQuery,
                this.MetaModelService.GetQueryModelInfo(compositeQuery.Name).FirstOrDefault<ModelInfo>());
        }

        public void visitForm(Form form)
        {
        }

        public void visitFormExtension(FormExtension formExtension)
        {
        }

        public void visitDataEntity(DataEntityViewBase dataEntity)
        {
            this.run(
                ModelElementType.DataEntityView,
                dataEntity,
                this.MetaModelService.GetDataEntityViewModelInfo(dataEntity.Name).FirstOrDefault<ModelInfo>());
        }
    }
}