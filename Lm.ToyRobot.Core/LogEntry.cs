using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.ToyRobot.Core.Logging
{
    /// <summary>
    /// Log class for movements and changes history, is not a struct because need to be mutable.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Date and time for the entry.
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// Position logged.
        /// </summary>
        public Position Position { get; set; }
    }
}
