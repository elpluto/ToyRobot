using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.ToyRobot.Core.Enumerations;
using Lm.ToyRobot.Core.Logging;

namespace Lm.ToyRobot.Core
{
    /// <summary>
    /// Robot Interface, define the behavior and operations supported by a robot.
    /// </summary>
    public interface IRobot
    {
        /// <summary>
        /// Keep a UUID identifier.
        /// </summary>
        string GlobalKey { get; set; }
        /// <summary>
        /// Robot identifier, should be a UUID generated id or a GUID struct object.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Keep the initial position to allow the reboot operation.
        /// </summary>
        Position InitialPosition { get; set; }
        /// <summary>
        /// Keep the current position of the robot.
        /// </summary>
        Position CurrentPosition { get; set; }
        /// <summary>
        /// Keep the last position of the robot so could be revert by a validation error or the UNDO command.
        /// </summary>
        Position LastPosition { get; set; }
        /// <summary>
        /// Indicate if the robot has been placed on playground. Movements commands are executed only on placed robots.
        /// </summary>
        bool IsPlaced { get; set; }
        /// <summary>
        /// Indicate if the robot is activated. Movements commands are executed only on activated robots.
        /// </summary>
        bool IsActivated { get; set; }
        /// <summary>
        /// Log list of movements history.
        /// </summary>
        IList<LogEntry> Log { get; set; }
        /// <summary>
        /// Place a robot on the playgroung at a specific oriented position.
        /// </summary>
        bool Place(Position placePosition);
        /// <summary>
        /// Switch on the robot activating movement.
        /// </summary>
        /// <returns></returns>
        void On();
        /// <summary>
        /// Switch off the robot deactivating movement.
        /// </summary>
        /// <returns></returns>
        void Off();
        /// <summary>
        /// Move the active robot one unit at the faced direction.
        /// </summary>
        /// <returns></returns>
        bool Move(int stepSize);
        /// <summary>
        /// Turn the face of the active robot 90ª to the left.
        /// </summary>
        /// <returns></returns>
        bool TurnLeft();
        /// <summary>
        /// Turn the face of the active robot 90ª to the right.
        /// </summary>
        /// <returns></returns>
        bool TurnRight();
        /// <summary>
        /// Reset the postion to the initial position of the robot. This operation clear the robot movements history.
        /// </summary>
        /// <returns></returns>
        void Reboot();
        /// <summary>
        /// Remove the active robot from the playground and deactivate it.
        /// This operation clear the robot movements history.
        /// </summary>
        /// <returns></returns>
        void Remove();
        /// <summary>
        /// Show the current position and orientation of the robot.
        /// </summary>
        /// <returns></returns>
        Position Report();
        /// <summary>
        /// Revert the current position to the last position.
        /// </summary>
        void Undo();
        
    }
}
