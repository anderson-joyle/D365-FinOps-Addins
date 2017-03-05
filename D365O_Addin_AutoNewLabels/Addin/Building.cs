using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Views;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.BaseTypes;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Security;

namespace Building
{
    /// <summary>
    /// Base class to create new labels
    /// </summary>
    abstract public class CreateLabels
    {
        protected Labeling.LabelManager labelManager;
        /// <summary>
        /// Initialize class
        /// </summary>
        public CreateLabels()
        {
            labelManager = new Labeling.LabelManager();
        }

        /// <summary>
        /// Point to all label properties and child elements label properties
        /// </summary>
        abstract public void run();

        /// <summary>
        /// Contructs a classes based on the element type
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        static public CreateLabels construct(NamedElement element)
        {
            // TODO: Add here new elements constructors
            switch (element.GetType().Name)
            {
                case "Table":
                    return new CreateLabels_Table(element as Table);

                case "View":
                    return new CreateLabels_View(element as View);

                case "EdtBase":
                case "EdtString":
                case "EdtContainer":
                case "EdtDate":
                case "EdtEnum":
                case "EdtDateTime":
                case "EdtGuid":
                case "EdtReal":
                case "EdtInt":
                case "EdtInt64":
                    return new CreateLabels_Edt(element as EdtBase);

                case "BaseEnum":
                    return new CreateLabels_BaseEnum(element as BaseEnum);

                case "BaseEnumExtension":
                    return new CreateLabels_BaseEnumExtension(element as BaseEnumExtension);

                case "MenuItem":
                case "MenuItemAction":
                case "MenuItemDisplay":
                case "MenuItemOutput":
                    return new CreateLabels_MenuItem(element as MenuItem);

                case "Form":
                    return new CreateLabels_Form(element as Form);

                case "SecurityPrivilege":
                    return new CreateLabels_SecurityPrivilege(element as SecurityPrivilege);

                default:
                    throw new NotImplementedException($"The type {element.GetType().Name} is not implemented.");
            }
        }

        /// <summary>
        /// Get logging message
        /// </summary>
        /// <returns>Logging message</returns>
        public string getLoggingMessage()
        {
            return this.labelManager.getLoggingMessage();
        }
    }

    /// <summary>
    /// Creates labels to table and child elements
    /// </summary>
    /// <remarks>Currently, the following elements are covered: table, fields, field groups</remarks>
    public class CreateLabels_Table : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current table
        /// </summary>
        protected Table table;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="table">Selected table element</param>
        public CreateLabels_Table(Table table)
        {
            this.table = table;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.table.Label = this.labelManager.createLabel(this.table.Label);

            // Dev doc
            this.table.DeveloperDocumentation = this.labelManager.createLabel(this.table.DeveloperDocumentation);

            foreach (BaseField field in this.table.BaseFields)
            {
                // Field label
                field.Label = this.labelManager.createLabel(field.Label);

                // Field help text
                field.HelpText = this.labelManager.createLabel(field.HelpText);
            }

            foreach (Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables.FieldGroup fieldGroup in this.table.FieldGroups)
            {
                // Field group label
                fieldGroup.Label = this.labelManager.createLabel(fieldGroup.Label);
            }
        }
    }

    /// <summary>
    /// Creates labels to table and child elements
    /// </summary>
    public class CreateLabels_Edt : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current Edt
        /// </summary>
        protected EdtBase edt;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="edt">Selected edt element</param>
        public CreateLabels_Edt(EdtBase edt)
        {
            this.edt = edt;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.edt.Label = this.labelManager.createLabel(this.edt.Label);

            // Help text
            this.edt.HelpText = this.labelManager.createLabel(this.edt.HelpText);
        }
    }

    /// <summary>
    /// Creates labels to BaseEnum and child elements
    /// </summary>
    public class CreateLabels_BaseEnum : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current BaseEnum
        /// </summary>
        protected BaseEnum baseEnum;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="baseEnum">Selected baseenum element</param>
        public CreateLabels_BaseEnum(BaseEnum baseEnum)
        {
            this.baseEnum = baseEnum;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.baseEnum.Label = this.labelManager.createLabel(this.baseEnum.Label);

            // Elements labels
            foreach (BaseEnumValue values in this.baseEnum.BaseEnumValues)
            {
                values.Label = this.labelManager.createLabel(values.Label);
            }
        }
    }

    /// <summary>
    /// Creates labels to BaseEnum extension and child elements
    /// </summary>
    public class CreateLabels_BaseEnumExtension : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current BaseEnum extension
        /// </summary>
        protected BaseEnumExtension baseEnumExtension;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="baseEnumExtension">Selected baseenum extension element</param>
        public CreateLabels_BaseEnumExtension(BaseEnumExtension baseEnumExtension)
        {
            this.baseEnumExtension = baseEnumExtension;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Elements labels
            foreach (BaseEnumValue values in this.baseEnumExtension.BaseEnumValues)
            {
                values.Label = this.labelManager.createLabel(values.Label);
            }
        }
    }

    /// <summary>
    /// Creates labels to MenuItem and child elements
    /// </summary>
    public class CreateLabels_MenuItem : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current menuitem
        /// </summary>
        protected MenuItem menuItem;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="menuItem">Selected menuitem element</param>
        public CreateLabels_MenuItem(MenuItem menuItem)
        {
            this.menuItem = menuItem;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.menuItem.Label = this.labelManager.createLabel(this.menuItem.Label);

            // Help text
            this.menuItem.HelpText = this.labelManager.createLabel(this.menuItem.HelpText);
        }
    }

    /// <summary>
    /// Creates labels to view and child elements
    /// </summary>
    /// <remarks>Currently, the following elements are covered: view, fields, field groups</remarks>
    public class CreateLabels_View : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current view
        /// </summary>
        protected View view;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="view">Selected view element</param>
        public CreateLabels_View(View view)
        {
            this.view = view;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.view.Label = this.labelManager.createLabel(this.view.Label);

            // Dev doc
            this.view.DeveloperDocumentation = this.labelManager.createLabel(this.view.DeveloperDocumentation);

            foreach (ViewBaseField field in this.view.ViewBaseFields)
            {
                // Field label
                field.Label = this.labelManager.createLabel(field.Label);

                // Field help text
                field.HelpText = this.labelManager.createLabel(field.HelpText);
            }

            foreach (Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Views.FieldGroup fieldGroup in this.view.FieldGroups)
            {
                // Field group label
                fieldGroup.Label = this.labelManager.createLabel(fieldGroup.Label);
            }
        }
    }

    /// <summary>
    /// Creates labels to form and child elements
    /// </summary>
    /// <remarks>Currently, the following elements are covered: design and controls</remarks>
    public class CreateLabels_Form : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current view
        /// </summary>
        protected Form form;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="form">Selected form element</param>
        public CreateLabels_Form(Form form)
        {
            this.form = form;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Caption
            this.form.FormDesign.Caption = this.labelManager.createLabel(this.form.FormDesign.Caption);

            // Form controls
            foreach (FormControl control in this.form.FormDesign.FormControls)
            {
                switch (control.Type)
                {                    
                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.String:
                        FormStringControl stringControl = control as FormStringControl;
                        stringControl.Label = this.labelManager.createLabel(stringControl.Label);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.CheckBox:
                        FormCheckBoxControl checkboxControl = control as FormCheckBoxControl;
                        checkboxControl.Label = this.labelManager.createLabel(checkboxControl.Label);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Group:
                        FormGroupControl groupControl = control as FormGroupControl;
                        groupControl.Caption = this.labelManager.createLabel(groupControl.Caption);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Button:
                        FormButtonControl buttonControl = control as FormButtonControl;
                        buttonControl.Text = this.labelManager.createLabel(buttonControl.Text);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Real:
                        FormRealControl realControl = control as FormRealControl;
                        realControl.Label = this.labelManager.createLabel(realControl.Label);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Integer:
                        FormIntegerControl integerControl = control as FormIntegerControl;
                        integerControl.Label = this.labelManager.createLabel(integerControl.Label);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ComboBox:
                        FormComboBoxControl comboboxControl = control as FormComboBoxControl;
                        comboboxControl.Label = this.labelManager.createLabel(comboboxControl.Label);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Image:
                        FormImageControl imageControl = control as FormImageControl;
                        imageControl.Label = this.labelManager.createLabel(imageControl.Label);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Date:
                        FormDateControl dateControl = control as FormDateControl;
                        dateControl.Label = this.labelManager.createLabel(dateControl.Label);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.RadioButton:
                        FormRadioButtonControl radioControl = control as FormRadioButtonControl;
                        radioControl.Caption = this.labelManager.createLabel(radioControl.Caption);
                        break;
                        
                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ButtonGroup:
                        FormButtonGroupControl buttonGroupCaption = control as FormButtonGroupControl;
                        buttonGroupCaption.Caption = this.labelManager.createLabel(buttonGroupCaption.Caption);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.TabPage:
                        FormTabPageControl tabpageControl = control as FormTabPageControl;
                        tabpageControl.Caption = this.labelManager.createLabel(tabpageControl.Caption);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.CommandButton:
                        FormCommandButtonControl commandbuttonControl = control as FormCommandButtonControl;
                        commandbuttonControl.Text = this.labelManager.createLabel(commandbuttonControl.Text);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.MenuButton:
                        FormMenuButtonControl menubuttonControl = control as FormMenuButtonControl;
                        menubuttonControl.Text = this.labelManager.createLabel(menubuttonControl.Text);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.MenuFunctionButton:
                        FormMenuFunctionButtonControl menufunctionControl = control as FormMenuFunctionButtonControl;
                        menufunctionControl.Text = this.labelManager.createLabel(menufunctionControl.Text);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ListBox:
                        FormListBoxControl listboxControl = control as FormListBoxControl;
                        listboxControl.Label = this.labelManager.createLabel(listboxControl.Label);
                        break;
                        
                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Time:
                        FormTimeControl timeControl = control as FormTimeControl;
                        timeControl.Label = this.labelManager.createLabel(timeControl.Label);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ButtonSeparator:
                        FormButtonSeparatorControl buttonseparatorControl = control as FormButtonSeparatorControl;
                        buttonseparatorControl.Text = this.labelManager.createLabel(buttonseparatorControl.Text);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Guid:
                        FormGuidControl guidControl = control as FormGuidControl;
                        guidControl.Label = this.labelManager.createLabel(guidControl.Label);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Int64:
                        FormInt64Control int64Control = control as FormInt64Control;
                        int64Control.Label = this.labelManager.createLabel(int64Control.Label);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.DateTime:
                        FormDateTimeControl datetimeControl = control as FormDateTimeControl;
                        datetimeControl.Label = this.labelManager.createLabel(datetimeControl.Label);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ActionPane:
                        FormActionPaneControl actionpaneControl = control as FormActionPaneControl;
                        actionpaneControl.Caption = this.labelManager.createLabel(actionpaneControl.Caption);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ActionPaneTab:
                        FormActionPaneTabControl actionpanetabControl = control as FormActionPaneTabControl;
                        actionpanetabControl.Caption = this.labelManager.createLabel(actionpanetabControl.Caption);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.SegmentedEntry:
                        FormSegmentedEntryControl segmentedEntryControl = control as FormSegmentedEntryControl;
                        segmentedEntryControl.Label = this.labelManager.createLabel(segmentedEntryControl.Label);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.DropDialogButton:
                        FormDropDialogButtonControl dropDialogButtonControl = control as FormDropDialogButtonControl;
                        dropDialogButtonControl.Text = this.labelManager.createLabel(dropDialogButtonControl.Text);
                        break;

                    case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ReferenceGroup:
                        FormReferenceGroupControl referenceGroupControl = control as FormReferenceGroupControl;
                        referenceGroupControl.Label = this.labelManager.createLabel(referenceGroupControl.Label);
                        break;

                    default:
                        throw new NotImplementedException($"Form control type {control.Type.ToString()} is not implemented.");
                }

                control.HelpText = this.labelManager.createLabel(control.HelpText);
            }
        }
    }

    /// <summary>
    /// Creates labels to security privileges and child elements
    /// </summary>
    public class CreateLabels_SecurityPrivilege : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current menuitem
        /// </summary>
        protected SecurityPrivilege securityPrivilege;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="securityPrivilege">Selected SecurityPrivilege element</param>
        public CreateLabels_SecurityPrivilege(SecurityPrivilege securityPrivilege)
        {
            this.securityPrivilege = securityPrivilege;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.securityPrivilege.Label = this.labelManager.createLabel(this.securityPrivilege.Label);
        }
    }
}
