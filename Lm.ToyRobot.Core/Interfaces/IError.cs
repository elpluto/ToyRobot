using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.ToyRobot.Core.Validation
{
    /// <summary>
    /// Error interface defining how is an error.
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// Error number.
        /// </summary>
        int Number { get; set; }
        /// <summary>
        /// Error message.
        /// </summary>
        string Message { get; set; }
    }
}
