using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.ToyRobot.Controller.Enumerations;

namespace Lm.ToyRobot.Controller
{
    /// <summary>
    /// Describe a basic command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Command keyword.
        /// </summary>
        CommandEnum Key { get; set; }
        /// <summary>
        /// Command arguments. This version only support int arguments.
        /// </summary>
        IList<int> Arguments { get; set; }
    }
}
