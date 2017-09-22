using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Addin
{
    public static class SqlNameMangling
    {
        private const char X = 'X';
        private const char ExtensionNameSeparator = '.';

        private static char[] ArrayIndexFieldSplitter = new char[] { '[', ']' };

        private static HashSet<string> ReservedKeywordsSet = new HashSet<string>(new[] {
            "ACCESS", "ALL", "ANY", "AUTHORIZATION", "BETWEEN", "CASE", "CHECK", "CHECKPOINT", "COLUMN",
            "COMMENT", "CONNECT", "CONFIRM", "CONSTRAINT", "CONTINUE", "CONVERT", "CREATE", "CURRENT",
            "DATABASE", "DECIMAL", "DEFAULT", "DISK", "DISTINCT", "END", "EXECUTE", "FILE", "FOREIGN",
            "FUNCTION", "FREETEXT", "FROM", "GRANT", "GROUP", "IDENTITY", "INITIAL", "INTEGER", "KEY", "KILL", "LEVEL", "LIKE",
            "MAX", "MIN", "MODE", "MODIFY", "NULL", "NUMBER", "ONCE", "OPTION", "ORDER", "PERCENT", "PLAN", "PRECISION",
            "PRIOR", "PUBLIC", "RAW", "REFERENCES", "RESOURCE", "ROW", "ROWS", "RULE", "SESSION", "SIZE", "START",
            "STATISTICS", "TABLE", "TO", "UNIQUE", "USER", "VALIDATE", "VALUES", "VIEW"
        }, StringComparer.OrdinalIgnoreCase);

        public static string GetValidSqlName(string nameOfField)
        {
            return GetValidSqlName(nameOfField, 0);
        }

        public static string GetValidSqlName(string nameOfField, int arrayIndex)
        {
            // Support for Table Extension
            nameOfField = nameOfField.Contains(ExtensionNameSeparator) ? nameOfField.Replace(ExtensionNameSeparator, '$') : nameOfField;
            nameOfField = nameOfField.ToUpperInvariant();

            if (nameOfField[0] == '.')
            {
                nameOfField = "$" + nameOfField.Substring(1);
            }
            else if (nameOfField[0] == ':')
            {
                nameOfField = X + nameOfField.Substring(1);
            }
            else if (nameOfField[0] == '_')
            {
                nameOfField = X + nameOfField.Substring(1);
            }

            StringBuilder filtered = new StringBuilder(nameOfField.Length);
            for (int i = 0; i < nameOfField.Length; i++)
            {
                char current = nameOfField[i];
                if (current > 127)
                {
                    filtered.Append(X);
                }
                else
                {
                    filtered.Append(current);
                }
            }
            nameOfField = filtered.ToString();

            if (arrayIndex > 1)
            {
                // generate array fields like Dim, Dim2_, Dim3_ ...
                nameOfField += arrayIndex + "_";
            }
            // handle reserved keywords like "key", "percent" etc.
            else if (ReservedKeywordsSet.Contains(nameOfField))
            {
                nameOfField += "_";
            }

            return nameOfField;
        }

        public static string GetValidSqlNameForField(string fieldName)
        {
            fieldName = fieldName.ToUpperInvariant();
            Boolean hasReservedWord = ReservedKeywordsSet.Contains(fieldName);

            // split Dim[2] in [] {"Dim", 2} 
            var arrayField = fieldName.Split(ArrayIndexFieldSplitter, StringSplitOptions.RemoveEmptyEntries);
            if (arrayField.Count() > 1)
            {
                // Dim[1] is Dim, Dim[2] is Dim2_, Key[1] is Key_ and Key[2] is Key_2_ (key is reserved word)
                int arrayIndex = Convert.ToInt32(arrayField[1], CultureInfo.InvariantCulture);
                fieldName = arrayField[0];
                if (hasReservedWord)
                {
                    fieldName += "_";
                }

                if (arrayIndex > 1)
                {
                    fieldName += arrayField[1] + "_";
                }

            }
            else if (hasReservedWord)
            {
                fieldName += "_";
            }
            return fieldName;
        }

        public static string GetSqlTableName(string tableName)
        {
            tableName = tableName.ToUpperInvariant();
            if (ReservedKeywordsSet.Contains(tableName))
            {
                tableName += "_";
            }
            return tableName;
        }
    }
}

