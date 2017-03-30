/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Views;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Classes;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;

using Microsoft.Dynamics.AX.Data.Management;

using Metadata = Microsoft.Dynamics.AX.Metadata;
using Environment = Microsoft.Dynamics.ApplicationPlatform.Environment;

using Element;

/// <summary>
/// DO NOT USE THIS NAMESPACE. THIS HAS BEEN KEEP IT ONLY AS A REMIDER OF HOW TO CALL XPPC AND SYNCENGINE.
/// </summary>
namespace Operation_OLD
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
        void visitClass(ClassItem classItem, Element.ElementClass element);
        void visitQuery(Query query, Element.ElementQuery element);
        void visitForm(Form form, ElementForm element);
        void visitFormExtension(FormExtension formExtension, ElementFormExtension element);
        void visitDataEntity(DataEntity dataEntity, ElementDataEntity element);
    }

    abstract public class ElementOperation : IElementOperation
    {       
        #region Member variables
        /// <summary>
        /// Backing field for the metadata provider that is useful for retrieving
        /// metadata that is not loaded in the VS instance.
        /// </summary>
        protected Metadata.Providers.IMetadataProvider metadataProvider = null;
        protected Environment.IApplicationEnvironment environment;
        protected string application;
        protected string baseArguments;

        protected DateTime startTime;
        protected DateTime endTime;
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

        #region Abstract
        abstract public void visitTable(Table table, ElementTable element);
        abstract public void visitTableExtension(TableExtension tableExtension, ElementTableExtension element);
        abstract public void visitView(View view, ElementView element);
        abstract public void visitClass(ClassItem classItem, ElementClass element);
        abstract public void visitQuery(Query query, ElementQuery element);
        abstract public void visitForm(Form form, ElementForm element);
        abstract public void visitFormExtension(FormExtension formExtension, ElementFormExtension element);
        abstract public void visitDataEntity(DataEntity dataEntity, ElementDataEntity element);
        #endregion

        virtual protected void init()
        {
            environment = Environment.EnvironmentFactory.GetApplicationEnvironment();
        }

        public void run()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

            // ToDo Change the style to Hidden in order to suppress the prompt
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = this.application;
            startInfo.Arguments = this.baseArguments;

            this.startTime = DateTime.Now;

            process.StartInfo = startInfo;            
            process.Start();
            process.WaitForExit();

            this.endTime = DateTime.Now;
        }

        public TimeSpan getTimeTotal()
        {
            return this.endTime - this.startTime;
        }
    }

    public class BuildElement : ElementOperation
    {
        #region Overlride
        public override void visitTable(Table table, ElementTable element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetTableModelInfo(table.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.init();
            this.initArguments(modelInfo);

            this.baseArguments += $" -tables={table.Name}";            

            this.run();
        }

        public override void visitTableExtension(TableExtension tableExtension, ElementTableExtension element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetTableExtensionModelInfo(tableExtension.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.init();
            this.initArguments(modelInfo);

            this.baseArguments += $" -tables={tableExtension.Name}";

            this.run();
        }

        public override void visitView(View view, ElementView element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetTableModelInfo(view.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.init();
            this.initArguments(modelInfo);

            this.baseArguments += $" -tables={view.Name}";

            this.run();
        }

        public override void visitClass(ClassItem classItem, ElementClass element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetTableModelInfo(classItem.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.init();
            this.initArguments(modelInfo);

            this.baseArguments += $" -classes={classItem.Name}";

            this.run();
        }

        public override void visitQuery(Query query, ElementQuery element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetTableModelInfo(query.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.init();
            this.initArguments(modelInfo);

            this.baseArguments += $" -queries={query.Name}";

            this.run();
        }

        public override void visitForm(Form form, ElementForm element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetTableModelInfo(form.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.init();
            this.initArguments(modelInfo);

            this.baseArguments += $" -forms={form.Name}";

            this.run();
        }

        public override void visitFormExtension(FormExtension formExtension, ElementFormExtension element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetTableModelInfo(formExtension.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.init();
            this.initArguments(modelInfo);

            this.baseArguments += $" -forms={formExtension.Name}";

            this.run();
        }

        public override void visitDataEntity(DataEntity dataEntity, ElementDataEntity element)
        {
            Metadata.Service.MetaModelServiceFactory factory = new Metadata.Service.MetaModelServiceFactory();
            Metadata.MetaModel.ModelInfo modelInfo;

            modelInfo = factory.Create(this.MetadataProvider).GetTableExtensionModelInfo(dataEntity.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();

            this.init();
            this.initArguments(modelInfo);

            this.baseArguments += $" -tables={dataEntity.Name}";

            this.run();
        }
        #endregion

        protected override void init()
        {
            base.init();

            this.application = $"{environment.Aos.MetadataDirectory}\\Bin\\Xppc.exe";
        }

        protected void initArguments(Metadata.MetaModel.ModelInfo modelInfo)
        {
            //this.baseArguments += $" -verbose";                                                                 // -verbose
            this.baseArguments += $" -failfast";                                                                // -failfast
            this.baseArguments += $" -modelmodule={modelInfo.Name}";                                            // -modelmodule
            this.baseArguments += $" -referenceFolder={this.environment.Aos.MetadataDirectory}";                // -referenceFolder
            this.baseArguments += $" -metadata={this.environment.Aos.MetadataDirectory}";                       // -metadata
            this.baseArguments += $" -output={this.environment.Aos.MetadataDirectory}\\{modelInfo.Name}\\bin";  // -output
            this.baseArguments += $" -refPath={this.environment.Aos.MetadataDirectory}\\{modelInfo.Name}\\bin"; // -refPath
        }
    }

    public class SyncElement : ElementOperation
    {
        #region Constants
        // ToDo Change these values according to your necessity
        protected const string servername = "localhost";
        protected const string dbname = "AxDB";
        #endregion

        #region Override
        public override void visitTable(Table table, ElementTable element)
        {
            this.init();
            this.initArguments();
            this.baseArguments += $" -synclist={table.Name}";

            //this.run();
            this.test(table.Name);
        }

        public override void visitTableExtension(TableExtension tableExtension, ElementTableExtension element)
        {
            this.init();
            this.initArguments();
            this.baseArguments += $" -synclist={tableExtension.Name}";

            this.run();
        }

        public override void visitView(View view, ElementView element)
        {
            this.init();
            this.initArguments();
            this.baseArguments += $" -synclist={view.Name}";

            this.run();
        }

        public override void visitClass(ClassItem classItem, ElementClass element)
        {
            throw new NotImplementedException("Classes are not syncable.");
        }

        public override void visitQuery(Query query, ElementQuery element)
        {
            throw new NotImplementedException("Queries are not syncable.");
        }

        public override void visitForm(Form form, ElementForm element)
        {
            throw new NotImplementedException("Forms are not syncable.");
        }

        public override void visitFormExtension(FormExtension formExtension, ElementFormExtension element)
        {
            throw new NotImplementedException("Form extensions are not syncable.");
        }

        public override void visitDataEntity(DataEntity dataEntity, ElementDataEntity element)
        {
            this.init();
            this.initArguments();
            this.baseArguments += $" -synclist={dataEntity.Name}";

            this.run();
        }
        #endregion

        protected override void init()
        {
            base.init();

            this.application = $"{environment.Aos.MetadataDirectory}\\Bin\\SyncEngine.exe";
        }

        protected void initArguments()
        {
            //this.baseArguments += $" -verbosity";                                                   // -verbosity
            this.baseArguments += $" -syncmode=partiallist";                                        // -syncmode
            this.baseArguments += $" -metadatabinaries={this.environment.Aos.MetadataDirectory}";   // -metadatabinaries
            this.baseArguments += $" -connect=";
            this.baseArguments += $"\"";
            this.baseArguments += $"Data Source={servername};";                                     // servername
            this.baseArguments += $" Initial Catalog={dbname};";                                    // dbname
            this.baseArguments += $" Integrated Security=True;";
            this.baseArguments += $" Enlist=True;";
            this.baseArguments += $" Application Name=SyncEngine";
            this.baseArguments += $"\"";
            this.baseArguments += $" -fallbacktonative=False";                                      // -fallbacktonative
        }

        public void test(string tablename)
        {

            ManagedSync sync = new ManagedSync(@"Data Source=localhost; Initial Catalog=AxDB; Integrated Security=True; Enlist=True; Application Name=D365O_Addin_BuildAndSync"
                                              , this.MetadataProvider
                                              , callback
                                              , Microsoft.Dynamics.AX.Data.Management.ManagedSync.ExecutionMode.Table);

            List<SyncRequest> requests = new List<SyncRequest>();

            requests.Add(new SyncRequest(Microsoft.Dynamics.AX.Data.Management.Enum.SyncRequestType.ModifyTable, tablename));

            sync.Synchronize(requests);
        }

        public void callback(Microsoft.Dynamics.AX.Data.Management.Enum.LogEntryType type, string message)
        {
            CoreUtility.DisplayInfo($"Type: {type}\nMessage: {message}");
        }
    }
}
*/