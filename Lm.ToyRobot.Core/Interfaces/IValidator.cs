using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.ToyRobot.Configuration;

namespace Lm.ToyRobot.Core.Validation
{
    /// <summary>
    /// Validator interface describe validation result and how to keep a error history-
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Keep the boolean result of a validation.
        /// </summary>
        bool IsValid{ get; set; }
        /// <summary>
        /// Keep a collection of errors produced during the validation.
        /// </summary>
        IList<Error> Errors { get; set; }
        /// <summary>
        /// Validate the coordinates of position values according to the setup of the playground.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        IValidator Validate(Position position, Playground playground);
    }
}
