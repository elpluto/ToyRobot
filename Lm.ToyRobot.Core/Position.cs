using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.ToyRobot.Core.Enumerations;

namespace Lm.ToyRobot.Core
{
    /// <summary>
    /// Define the position of the robot including its facing orientation.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// X absolute value of position in the workspace.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y absolute value of position in the workspace.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Orientation value of the face of the robot.
        /// </summary>
        public OrientationEnum FaceTo { get; set; }
    }
    
}
