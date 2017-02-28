using System.Collections.Generic;

namespace Logging
{
    /// <summary>
    /// Save created labels as logging
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// Created labels list
        /// </summary>
        protected List<string> labels;

        /// <summary>
        /// Initialize global variables
        /// </summary>
        public Logging()
        {
            this.labels = new List<string>();
        }

        /// <summary>
        /// Add new label to list
        /// </summary>
        /// <param name="singleLog">Log object</param>
        public void add(Log singleLog)
        {
            string formatedLabel;

            formatedLabel = $"({singleLog.labelFile}) {singleLog.labelId}: {singleLog.label}\n";

            this.labels.Add(formatedLabel);
        }

        /// <summary>
        /// Concatenates all label into logging message
        /// </summary>
        /// <returns></returns>
        public string getLogging()
        {
            string ret = string.Empty;

            if (labels.Count > 0)
            {
                ret += "The following labels were created:\n\n";

                foreach (string label in labels)
                {
                    ret += $"{label}";
                }
            }
            else
            {
                ret += "No label created.";
            }
            

            return ret;
        }

        /// <summary>
        /// Counts the list elements
        /// </summary>
        /// <returns></returns>
        public int count()
        {
            return this.labels.Count;
        }
    }

    /// <summary>
    /// Represents a single log message
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Label id
        /// </summary>
        public string labelId { set; get; }

        /// <summary>
        /// Label text
        /// </summary>
        public string label { set; get; }

        /// <summary>
        /// Label file id
        /// </summary>
        public string labelFile { set; get; }
    }
}
