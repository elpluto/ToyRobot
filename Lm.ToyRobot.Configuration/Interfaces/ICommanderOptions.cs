using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Lm.ToyRobot.Configuration
{
    /// <summary>
    /// Define which properties will be part of the options needed to configure a commander.
    /// </summary>
    public interface ICommanderOptions
    {
        /// <summary>
        /// Define how many robots could be handled at a time by the commander.
        /// </summary>
        int MaxRobots { get; set; }
        /// <summary>
        /// Width dimension of the table.
        /// </summary>
        int PlaygroundWidth { get; set; }
        /// <summary>
        /// Height dimension of the table.
        /// </summary>
        int PlaygroundHeight { get; set; }
        /// <summary>
        /// Define if boudaries of playground ara allowed to move on.
        /// </summary>
        bool AllowedBoundaries { get; set; }
        /// <summary>
        /// Define if collisions are detected and movement will be prevented.
        /// </summary>
        bool CollisionsDetected { get; set; }
        /// <summary>
        /// Define the size of a movement step.
        /// </summary>
        int StepSize { get; set; }
        
    }
}
