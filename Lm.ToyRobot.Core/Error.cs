using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.ToyRobot.Core.Validation
{
    /// <summary>
    /// Error class, describe an error with an error message and an error number.
    /// </summary>
    public class Error : IError
    {
        /// <summary>
        /// Error number.
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; set; }
    }
}
