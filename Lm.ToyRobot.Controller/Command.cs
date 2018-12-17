using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.ToyRobot.Controller.Enumerations;


namespace Lm.ToyRobot.Controller
{
    /// <summary>
    /// Basic command.
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        /// Command keyword.
        /// </summary>
        public CommandEnum Key { get; set; }
        /// <summary>
        /// Command arguments. This version only support int arguments.
        /// </summary>
        public IList<int> Arguments { get; set; }  
    }
}
