using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging
{
    //public class FormatMessage
    //{
    //    public string getTagValue()
    //    {
    //        string tagValue = string.Empty;
    //        string prefix = string.Empty;
    //        string typescription = string.Empty;
    //        string typeText = string.Empty;

    //        // {0} = prefix
    //        // {1} = Return description (class name, resolved label, method name, etc)
    //        // {2} = Return type text
    //        string baseFormat = "{0} {1} {2}";

    //        // Check for most commons method prefixes
    //        if (this.method.Name.StartsWith("get"))
    //        {
    //            prefix = "Get";

    //            switch (this.method.ReturnType.Type)
    //            {
    //                case CompilerBaseType.Void:
    //                    typescription = "???????????";
    //                    break;
    //                case CompilerBaseType.ExtendedDataType:
    //                    AxEdt axEdt = Utils.MetadataProvider.Edts.Read(this.method.ReturnType.TypeName);

    //                    typescription = Utils.ResolveLabel(axEdt.Label).ToLower();
    //                    typeText = "value";
    //                    break;
    //                case CompilerBaseType.Record:
    //                    AxTable axTable = Utils.MetadataProvider.Tables.Read(this.method.ReturnType.TypeName);
    //                    typescription = $"{Utils.ResolveLabel(axTable.Label).ToLower()} (<c>{this.method.ReturnType.TypeName}</c>)";
    //                    typeText = "record";
    //                    break;
    //                case CompilerBaseType.Class:
    //                    typescription = this.method.ReturnType.TypeName;
    //                    typeText = "class object";
    //                    break;
    //                default:
    //                    break;
    //            }

    //            tagValue = string.Format(baseFormat, prefix, typescription, typeText);
    //        }
    //        else if (this.method.Name.StartsWith("set"))
    //        {
    //            prefix = "Set";

    //            if (this.method.Parameters.Count >= 1)
    //            {
    //                switch (this.method.Parameters[0].Type)
    //                {
    //                    case CompilerBaseType.Void:
    //                        typescription = "???????????";
    //                        break;
    //                    case CompilerBaseType.ExtendedDataType:
    //                        AxEdt axEdt = Utils.MetadataProvider.Edts.Read(this.method.Parameters[0].Name);

    //                        typescription = Utils.ResolveLabel(axEdt.Label).ToLower();
    //                        typeText = "value";
    //                        break;
    //                    case CompilerBaseType.Record:
    //                        AxTable axTable = Utils.MetadataProvider.Tables.Read(this.method.Parameters[0].Name);
    //                        typescription = $"{Utils.ResolveLabel(axTable.Label).ToLower()} (<c>{this.method.Parameters[0].TypeName}</c>)";
    //                        typeText = "record";
    //                        break;
    //                    case CompilerBaseType.Class:
    //                        typescription = this.method.Parameters[0].TypeName;
    //                        typeText = "class object";
    //                        break;
    //                    default:
    //                        break;
    //                }

    //                tagValue = string.Format(baseFormat, prefix, typescription, typeText);
    //            }
    //            else
    //            {
    //                tagValue = "NON SENSE!!!";
    //            }
    //        }
    //        else if (this.method.Name == "main")
    //        {
    //            tagValue = $"Standalone class entry point";
    //        }
    //        else if (this.method.Name == "run")
    //        {
    //            tagValue = $"Execute class business logic";
    //        }
    //        else if (this.method.Name == "calc")
    //        {
    //            tagValue = $"Calculate values";
    //        }
    //        else if (this.method.Name.StartsWith("construct"))
    //        {
    //            tagValue = $"Initialize <c>{this.method.ReturnType.TypeName}</c> object.";
    //        }
    //        else if (this.method.Name.StartsWith("calculate"))
    //        {
    //            tagValue = "Calculate " + Utils.splitUpperCases(this.method.Name.Replace("calculate", "")).ToLower();
    //        }
    //        else if (this.method.Name.StartsWith("prepare"))
    //        {
    //            tagValue = "Prepare " + Utils.splitUpperCases(this.method.Name.Replace("prepare", "")).ToLower();
    //        }
    //        else
    //        {
    //            tagValue = "UNKNOWN";
    //        }

    //        return string.Format(tag, tagValue);
    //    }
    //}
}
