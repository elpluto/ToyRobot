using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.ToyRobot.Controller.Enumerations;

namespace Lm.ToyRobot.Controller
{
    /// <summary>
    /// This interface define a parser for commands.
    /// </summary>
    public interface ICommandParser
    {
        /// <summary>
        /// Parst the input to create a command.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ICommand ParseInput(string input);
    }
}
