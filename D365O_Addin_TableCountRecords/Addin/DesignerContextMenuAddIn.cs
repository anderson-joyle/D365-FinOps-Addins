namespace Addin
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Microsoft.Dynamics.Framework.Tools.Extensibility;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
    using Microsoft.Dynamics.Framework.Tools.Configuration;

    using Tables = Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
    using DataEntities = Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.DataEntityViews;

    using Metadata = Microsoft.Dynamics.AX.Metadata;
    using System.Data.SqlClient;
    using System.IO;
    using System.Diagnostics;

    /// <summary>
    /// Counts the number of records of the selected table
    /// </summary>
    [Export(typeof(IDesignerMenu))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(Tables.ITable))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(Tables.ITableExtension))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(Tables.IRelation))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(DataEntities.IDataEntityView))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(DataEntities.IDataEntityViewExtension))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(DataEntities.IRelation))]
    public class DesignerContextMenuAddIn : DesignerMenuBase
    {
        #region Member variables
        private const string addinName = "DesignerAddin";
        private string tableName;
        private string dataEntityName;
        #endregion

        #region Properties
        /// <summary>
        /// Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get
            {
                return AddinResources.DesignerAddinCaption;
            }
        }

        /// <summary>
        /// Unique name of the add-in
        /// </summary>
        public override string Name
        {
            get
            {
                return DesignerContextMenuAddIn.addinName;
            }
        }

        private static string BusinessDatabaseName
        {
            get
            {
                return ConfigurationHelper.CurrentConfiguration.BusinessDatabaseName;
            }
        }

        private static string BusinessDatabaseConnectionString
        {
            get
            {
                return ConfigurationHelper.CurrentConfiguration.BusinessDatabaseConnectionString;
            }
        }

        /// <summary>
        /// Backing field for the metadata provider that is useful for retrieving
        /// metadata that is not loaded in the VS instance.
        /// </summary>
        private Metadata.Providers.IMetadataProvider metadataProvider = null;

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

        #region Callbacks
        /// <summary>
        /// Called when user clicks on the add-in menu
        /// </summary>
        /// <param name="e">The context of the VS tools and metadata</param>
        public override void OnClick(AddinDesignerEventArgs e)
        {
            try
            {
                StringBuilder result;
                INamedElement namedElement = e.SelectedElement as INamedElement;

                if (namedElement is Tables.ITable)
                {
                    result = this.GenerateFromTable(namedElement as Tables.ITable, true);
                }
                else if (namedElement is Tables.ITableExtension)
                {
                    result = this.GenerateFromTableExtension(namedElement as Tables.ITableExtension, true);
                }
                else if (namedElement is Tables.IRelation)
                {
                    result = this.GenerateFromTableRelations(e.SelectedElements.OfType<Tables.IRelation>());

                    var selectedRelations = e.SelectedElements.OfType<Tables.IRelation>();
                    if (selectedRelations.Any())
                    {
                        result = this.GenerateFromTableRelations(selectedRelations);
                    }
                }
                else if (namedElement is DataEntities.IDataEntityView)
                {
                    result = this.GenerateFromDataEntity(namedElement as DataEntities.IDataEntityView, true);
                }
                else
                {
                    throw new NotImplementedException($"Element {e.SelectedElement.ToString()} is not supported.");
                }

                if (result != null)
                {
                    string message = string.Empty;
                    int counter = 0;

                    message += $"Counting for " + tableName;
                    message += "\n\n";

                    using (SqlConnection conn = new SqlConnection(BusinessDatabaseConnectionString))
                    {
                        var query = result.ToString();

                        query = query.Replace("\n", " ");
                        query = query.Replace("\r", " ");
                        query = query.Replace(" GO ", "");

                        conn.Open();

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.FieldCount == 1)
                                    {
                                        string counterLocStr = reader["COUNTER"].ToString();

                                        counter += int.Parse(counterLocStr);
                                    }
                                    else
                                    {
                                        string dataAreaId = reader["DATAAREAID"].ToString();
                                        string counterLocStr = reader["COUNTER"].ToString();

                                        message += $"{dataAreaId.ToUpper()}: {int.Parse(counterLocStr)}";

                                        counter += int.Parse(counterLocStr);
                                    }
                                }
                            }
                        }

                        conn.Close();
                    }

                    message += "\n\n";
                    message += $"Total: {counter}";
                    message += "\n\n";
                    message += "==================  USED QUERY  ===================\n";
                    message += result.ToString();
                    message += "===============================================";
                    message += "\n\n";
                    message += "Do you want to open this query in MSSQL Management Studio?";

                    if (CoreUtility.PromptYesNo(message, "Counting records"))
                    {
                        // Save the SQL file and open it in SQL management studio. 
                        string temporaryFileName = Path.GetTempFileName();

                        // Rename and move
                        var sqlFileName = temporaryFileName.Replace(".tmp", ".sql");
                        File.Move(temporaryFileName, sqlFileName);

                        // Store the script in the file
                        File.AppendAllText(sqlFileName, result.ToString());

                        //var dte = CoreUtility.ServiceProvider.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
                        //dte.ExecuteCommand("File.OpenFile", sqlFileName);

                        Process sqlManagementStudio = new Process();
                        sqlManagementStudio.StartInfo.FileName = sqlFileName;
                        sqlManagementStudio.StartInfo.UseShellExecute = true;
                        sqlManagementStudio.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        sqlManagementStudio.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// The method is called when the user has selected to view a single view.
        /// The method generates a select on all fields, with no joins.
        /// </summary>
        /// <param name="selectedTable">The designer metadata designating the view.</param>
        /// <returns>The string containing the SQL command.</returns>
        private StringBuilder GenerateFromTable(Tables.ITable selectedTable, bool addGroupBy)
        {
            var result = new StringBuilder();

            // It is indeed a table. Look at the properties
            if (!selectedTable.IsKernelTable)
            {
                bool first = true;

                result.AppendLine(string.Format(CultureInfo.InvariantCulture, "USE {0}", BusinessDatabaseName));
                result.AppendLine("GO");

                Stack<Metadata.MetaModel.AxTable> tables = this.SuperTables(selectedTable.Name);

                tableName = SqlNameMangling.GetSqlTableName(tables.First().Name);

                // List any developer documentation as a SQL comment:
                //if (!string.IsNullOrEmpty(selectedTable.DeveloperDocumentation))
                //{
                //    result.Append("-- " + selectedTable.Name);
                //    result.AppendLine(" : " + this.ResolveLabel(selectedTable.DeveloperDocumentation));
                //}
                //else
                //{
                //    result.AppendLine();
                //}

                if (tables.First().SaveDataPerCompany == Metadata.Core.MetaModel.NoYes.Yes)
                {
                    result.AppendLine($"SELECT {tableName}.DATAAREAID, COUNT(*) AS COUNTER");
                }
                else
                {
                    result.AppendLine("SELECT COUNT(*) AS COUNTER");
                }

                result.AppendLine("FROM " + tableName);

                if (tables.First().SaveDataPerCompany == Metadata.Core.MetaModel.NoYes.Yes && addGroupBy)
                {
                    result.AppendLine($"GROUP BY {tableName}.{SqlNameMangling.GetValidSqlNameForField("DATAAREAID")}");
                }
            }

            return result;
        }

        /// <summary>
        /// The method is called when the user has selected to view a single view.
        /// The method generates a select on all fields, with no joins.
        /// </summary>
        /// <param name="selectedTable">The designer metadata designating the view.</param>
        /// <returns>The string containing the SQL command.</returns>
        private StringBuilder GenerateFromDataEntity(DataEntities.IDataEntityView selectedDataEntity, bool addGroupBy)
        {
            var result = new StringBuilder();
            bool first = true;

            Metadata.MetaModel.AxDataEntityView axDataEntity = this.MetadataProvider.DataEntityViews.Read(selectedDataEntity.Name);

            result.AppendLine(string.Format(CultureInfo.InvariantCulture, "USE {0}", BusinessDatabaseName));
            result.AppendLine("GO");

            dataEntityName = SqlNameMangling.GetSqlTableName(selectedDataEntity.Name);

            result.AppendLine("SELECT COUNT(*) AS COUNTER");
            result.AppendLine("FROM " + dataEntityName);

            return result;
        }

        /// <summary>
        /// The method is called when the user has selected to view a table extension instance.
        /// The method generates a select on all fields, with no joins.
        /// </summary>
        /// <param name="selectedExtensionTable">The designer metadata designating the view.</param>
        /// <returns>The string containing the SQL command.</returns>
        private StringBuilder GenerateFromTableExtension(Tables.ITableExtension selectedExtensionTable, bool addGroupBy)
        {
            var result = new StringBuilder();
            bool first = true;

            result.AppendLine(string.Format(CultureInfo.InvariantCulture, "USE {0}", BusinessDatabaseName));
            result.AppendLine("GO");
            result.AppendLine();

            Metadata.MetaModel.AxTableExtension extension = this.MetadataProvider.TableExtensions.Read(selectedExtensionTable.Name);
            var baseTableName = selectedExtensionTable.Name.Split('.').First();
            var tables = this.SuperTables(baseTableName);

            tableName = baseTableName;

            HashSet<string> extendedFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var extendedField in extension.Fields)
            {
                extendedFields.Add(extendedField.Name);
            }

            if (tables.First().SaveDataPerCompany == Metadata.Core.MetaModel.NoYes.Yes)
            {
                result.AppendLine($"SELECT {SqlNameMangling.GetSqlTableName(tables.First().Name)}.DATAAREAID, COUNT(*) AS COUNTER");
            }
            else
            {
                result.AppendLine("SELECT COUNT(*) AS COUNTER");
            }

            result.AppendLine("FROM " + SqlNameMangling.GetSqlTableName(tables.First().Name));

            if (tables.First().SaveDataPerCompany == Metadata.Core.MetaModel.NoYes.Yes && addGroupBy)
            {
                result.AppendLine($"GROUP BY {SqlNameMangling.GetSqlTableName(tables.First().Name)}.{SqlNameMangling.GetValidSqlNameForField("DATAAREAID")}");
            }

            return result;
        }

        /// <summary>
        /// Generate the SQL command selecting the fields from the selected table, performing 
        /// joins on the tables indicated by the relations selected.
        /// </summary>
        /// <param name="selectedRelations">The list of field groups to select fields from.</param>
        /// <returns>The string containing the SQL command.</returns>
        private StringBuilder GenerateFromTableRelations(IEnumerable<Tables.IRelation> selectedRelations)
        {
            Tables.ITable table = selectedRelations.First().Table;
            Stack<Metadata.MetaModel.AxTable> tables = this.SuperTables(table.Name);

            var result = this.GenerateFromTable(table, false);
            int disambiguation = 1;

            // Now add the joins. Assume that multiple relations can be
            // selected; joins will be added for all of them.
            foreach (var relation in selectedRelations)
            {
                var relatedTabularObjectName = relation.RelatedTable;
                // TODO: In principle this could be a view. Assuming table for now

                result.AppendLine(string.Format(CultureInfo.InvariantCulture, "INNER JOIN {0} as t{1}", SqlNameMangling.GetSqlTableName(relatedTabularObjectName), disambiguation));
                result.Append("ON ");

                bool first = true;
                foreach (Tables.IRelationConstraint constraint in relation.RelationConstraints)
                {
                    if (!first)
                        result.Append(" AND ");

                    if (constraint is Tables.IRelationConstraintField)
                    {   // Table.field = RelatedTable.relatedField
                        var fieldConstraint = constraint as Tables.IRelationConstraintField;
                        result.Append(SqlNameMangling.GetSqlTableName(table.Name) + ".[" + SqlNameMangling.GetValidSqlNameForField(fieldConstraint.Field) + "]");
                        result.Append(" = ");
                        result.AppendLine(string.Format(CultureInfo.InvariantCulture, "t{0}", disambiguation) + "." + SqlNameMangling.GetValidSqlNameForField(fieldConstraint.RelatedField));
                    }
                    else if (constraint is Tables.IRelationConstraintFixed)
                    {   // Table.field = value
                        var fixedConstraint = constraint as Tables.IRelationConstraintFixed;

                        result.Append(SqlNameMangling.GetSqlTableName(table.Name) + ".[" + SqlNameMangling.GetValidSqlNameForField(fixedConstraint.Field) + "]");
                        result.Append(" = ");
                        result.AppendLine(fixedConstraint.Value.ToString(CultureInfo.InvariantCulture));
                    }
                    else if (constraint is Tables.IRelationConstraintRelatedFixed)
                    {   // Value = RelatedTable.field
                        var relatedFixedConstraint = constraint as Tables.IRelationConstraintRelatedFixed;
                        result.Append(relatedFixedConstraint.Value);
                        result.Append(" = ");
                        result.AppendLine(string.Format(CultureInfo.InvariantCulture, "t{0}", disambiguation) + ".[" + SqlNameMangling.GetValidSqlNameForField(relatedFixedConstraint.RelatedField) + "]");
                    }

                    first = false;
                }

                disambiguation += 1;
            }

            if (tables.First().SaveDataPerCompany == Metadata.Core.MetaModel.NoYes.Yes)
            {
                result.AppendLine($"GROUP BY {SqlNameMangling.GetSqlTableName(tables.First().Name)}.{SqlNameMangling.GetValidSqlNameForField("DATAAREAID")}");
            }

            return result;
        }

        /// <summary>
        /// Find the sequence of tables from the table to its root in the inheritance chain.
        /// If the indicated table is not part of a supertype subtype relationship, the 
        /// result contains just the single table.
        /// </summary>
        /// <param name="leafTableName">The most derived table.</param>
        /// <returns>The sequence of tables, with the root at the top of the stack.</returns>
        private Stack<Metadata.MetaModel.AxTable> SuperTables(string leafTableName)
        {
            Stack<Metadata.MetaModel.AxTable> result = new Stack<Metadata.MetaModel.AxTable>();
            Metadata.MetaModel.AxTable table = this.MetadataProvider.Tables.Read(leafTableName);

            while (table.SupportInheritance == Metadata.Core.MetaModel.NoYes.Yes
                && !string.IsNullOrWhiteSpace(table.Extends))
            {
                result.Push(table);
                table = this.MetadataProvider.Tables.Read(table.Extends);
            }

            result.Push(table); // stack the root.
            return result;
        }

        /// <summary>
        /// Resolve the given label to the text it represents in the default language. If a non 
        /// label is passed, that text is returned unchanged.
        /// </summary>
        /// <param name="label">The label to look up.</param>
        /// <returns>The text for the label resolved to the current language.</returns>
        private string ResolveLabel(string label)
        {
            var labelResolver = CoreUtility.ServiceProvider.GetService(typeof(Microsoft.Dynamics.Framework.Tools.Integration.Interfaces.ILabelResolver)) as Microsoft.Dynamics.Framework.Tools.Integration.Interfaces.ILabelResolver;

            if (labelResolver != null)
            {
                return labelResolver.GetLabelText(label);
            }
            return label;
        }

        #endregion
    }
}
