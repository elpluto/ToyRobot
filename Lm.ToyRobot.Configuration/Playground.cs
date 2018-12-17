using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.ToyRobot.Configuration
{
    /// <summary>
    /// Playgroung class
    /// Define how is the workspace where the robots move through.
    /// </summary>
    public class Playground
    {
        /// <summary>
        /// Width of the environment in which the robot moves, correspond to the X limit.
        /// Set to a default value of 5.
        /// </summary>
        public int Width { get; set; } = 5;
        /// <summary>
        /// Height of the environment in which the robot moves, correnpond to the Y limit.
        /// Set to a default value of 5.
        /// </summary>
        public int Height { get; set; } = 5;
        /// <summary>
        /// Define if the robot are allowed to move over the border of the environment.
        /// Set to false by default.
        /// </summary>
        public bool AllowedBoundaries { get; set; } = false;
        /// <summary>
        /// Define if collisions must be detected before any movement. Set to false by default.
        /// </summary>
        public bool CollisionsDetected { get; set; } = false;
    }
}
