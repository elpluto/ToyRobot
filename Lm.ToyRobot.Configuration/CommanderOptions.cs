using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Lm.ToyRobot.Configuration
{
    /// <summary>
    /// Set a type to hold the properties to configure a commander.
    /// </summary>
    public class CommanderOptions : ICommanderOptions
    {
        /// <summary>
        /// Define how many robots could be handled at a time by the commander.
        /// </summary>
        public int MaxRobots { get; set; }
        /// <summary>
        /// Width dimension of the table.
        /// </summary>
        public int PlaygroundWidth { get; set; }
        /// <summary>
        /// Height dimension of the table.
        /// </summary>
        public int PlaygroundHeight { get; set; }
        /// <summary>
        /// Define if boudaries of playground ara allowed to move on.
        /// </summary>
        public bool AllowedBoundaries { get; set; }
        /// <summary>
        /// Define if collisions are detected and movement will be prevented.
        /// </summary>
        public bool CollisionsDetected { get; set; }
        /// <summary>
        /// Define the size of a movement step.
        /// </summary>
        public int StepSize { get; set; }
    }
}
