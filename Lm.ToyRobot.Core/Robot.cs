using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.ToyRobot.Core.Enumerations;
using Lm.ToyRobot.Core.Logging;
using Lm.ToyRobot.Core.Validation;

namespace Lm.ToyRobot.Core
{
    /// <summary>
    /// Main robot class.
    /// </summary>
    public class Robot : IRobot, IDisposable
    {
        /// <summary>
        /// Keep a UUID identifier.
        /// </summary>
        public string GlobalKey { get; set; }
        /// <summary>
        /// Robot identifier, should be a UUID generated id or a GUID struct object.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Keep the initial position to allow the reboot operation.
        /// </summary>
        public Position InitialPosition { get; set; }
        /// <summary>
        /// Keep the current position of the robot.
        /// </summary>
        public Position CurrentPosition { get; set; }
        /// <summary>
        /// Keep the last position of the robot so could be revert by a validation error or the UNDO command.
        /// </summary>
        public Position LastPosition { get; set; }
        /// <summary>
        /// Indicate if the robot has been placed on playground. Movements commands are executed only on placed robots.
        /// </summary>
        public bool IsPlaced { get; set; }
        /// <summary>
        /// Indicate if the robot is activated. Movements commands are executed only on activated robots.
        /// </summary>
        public bool IsActivated { get; set; }
        /// <summary>
        /// Log list of movements history.
        /// </summary>
        public IList<LogEntry> Log { get; set; }
        /// <summary>
        /// Ctor.
        /// </summary>
        public Robot()
        {
            //Initialize the log.
            Log = new List<LogEntry>();
            //Generata a UUID as Id of the robot.
            GlobalKey = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// Place a robot on the playgroung at a specific oriented position.
        /// </summary>
        public bool Place(Position placePosition)
        {
            //  Initialize positions.
            InitialPosition = placePosition.Clone();
            CurrentPosition = placePosition.Clone();
            LastPosition = placePosition.Clone();
            IsPlaced = true;
            //  Add log entry.
            Log.Add(new LogEntry { Time = DateTime.Now, Position = CurrentPosition.Clone() });
            return true;
        }
        /// <summary>
        /// Switch on the robot activating movement.
        /// </summary>
        /// <returns></returns>
        public void On() => IsActivated = true;
        /// <summary>
        /// Switch off the robot deactivating movement.
        /// </summary>
        /// <returns></returns>
        public void Off() => IsActivated = false;
        /// <summary>
        /// Move the active robot one unit at the faced direction. Validation is perform in the commander not here.
        /// </summary>
        /// <returns></returns>
        public bool Move(int stepSize)
        {
            //  Change position.
            if (IsPlaced && IsActivated)
            {
                // Keep the current position so it could be revert if validation is wrong.
                LastPosition = CurrentPosition.Clone();
                switch (CurrentPosition.FaceTo)
                {
                    case OrientationEnum.NORTH:
                        CurrentPosition.Y += stepSize;
                        break;
                    case OrientationEnum.SOUTH:
                        CurrentPosition.Y -= stepSize;
                        break;
                    case OrientationEnum.EAST:
                        CurrentPosition.X += stepSize;
                        break;
                    case OrientationEnum.WEST:
                        CurrentPosition.X -= stepSize;
                        break;
                }
            }
            //  Add log entry.
            Log.Add(new LogEntry { Time = DateTime.Now, Position = CurrentPosition.Clone() });
            return true;
        }
        /// <summary>
        /// Turn the face of the active robot 90ª to the left.
        /// </summary>
        /// <returns></returns>
        public bool TurnLeft()
        {
            //  Change position.
            if (IsPlaced && IsActivated)
            {
                LastPosition = CurrentPosition.Clone();
                switch (CurrentPosition.FaceTo) {
                    case OrientationEnum.NORTH:
                        CurrentPosition.FaceTo = OrientationEnum.WEST;
                        break;
                    case OrientationEnum.SOUTH:
                        CurrentPosition.FaceTo = OrientationEnum.EAST;
                        break;
                    case OrientationEnum.EAST:
                        CurrentPosition.FaceTo = OrientationEnum.NORTH;
                        break;
                    case OrientationEnum.WEST:
                        CurrentPosition.FaceTo = OrientationEnum.SOUTH;
                        break;
                }
            }
            //  Add log entry.
            Log.Add(new LogEntry { Time = DateTime.Now, Position = CurrentPosition.Clone() });
            return true;
        }
        /// <summary>
        /// Turn the face of the active robot 90ª to the right.
        /// </summary>
        /// <returns></returns>
        public bool TurnRight()
        {
            //  Change position
            if (IsPlaced && IsActivated)
            {
                LastPosition = CurrentPosition.Clone();
                switch (CurrentPosition.FaceTo)
                {
                    case OrientationEnum.NORTH:
                        CurrentPosition.FaceTo = OrientationEnum.EAST;
                        break;
                    case OrientationEnum.SOUTH:
                        CurrentPosition.FaceTo = OrientationEnum.WEST;
                        break;
                    case OrientationEnum.EAST:
                        CurrentPosition.FaceTo = OrientationEnum.SOUTH;
                        break;
                    case OrientationEnum.WEST:
                        CurrentPosition.FaceTo = OrientationEnum.NORTH;
                        break;
                }
            }
            //  Add log entry.
            Log.Add(new LogEntry { Time = DateTime.Now, Position = CurrentPosition.Clone() });
            return true;
        }
        /// <summary>
        /// Reset the postions to the initial position of the robot. This operation clear the robot movements history.
        /// </summary>
        /// <returns></returns>
        public void Reboot()
        {
            //  Change position.
            CurrentPosition = InitialPosition;
            //  Clear old log and add a new log entry.          
            Log.Clear();
            Log.Add(new LogEntry { Time = DateTime.Now, Position = CurrentPosition.Clone() });
        }
        /// <summary>
        /// Remove the active robot from the playground and deactivate it.
        /// This operation clear the robot movements history.
        /// </summary>
        /// <returns></returns>
        public void Remove()
        {
            IsPlaced = false;
            Log.Clear();
        }
        /// <summary>
        /// Show the current position and orientation of the robot.
        /// </summary>
        /// <returns></returns>
        public Position Report() => CurrentPosition;
        /// <summary>
        /// Revert the current position to the last position.
        /// </summary>
        public void Undo()
        {
            //  Revert
            CurrentPosition = LastPosition.Clone();
            //  Add log entry.
            Log.Add(new LogEntry { Time = DateTime.Now, Position = CurrentPosition.Clone() });
        }
        /// <summary>
        /// Release any resources used by the robot instance.
        /// </summary>
        public void Dispose()
        {
            //Release resources.
        }
    }
}
