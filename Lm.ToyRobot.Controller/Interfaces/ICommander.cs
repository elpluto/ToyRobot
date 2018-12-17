using System.Collections.Generic;
using Lm.ToyRobot.Configuration;
using Lm.ToyRobot.Core;

namespace Lm.ToyRobot.Controller
{
    /// <summary>
    /// ICommander interface, define behavior and operations supported by a commander.
    /// </summary>
    public interface ICommander
    {
        /// <summary>
        /// Maximum number of robots allowed in parking. Is set by confiuration.
        /// </summary>
        int MaxRobots { get; set; }
        /// <summary>
        /// Minimun size of a movement step.
        /// </summary>
        int StepSize { get; set; }
        /// <summary>
        /// Keep a store of existing robots. Destroyed robots will be removed from the store.
        /// </summary>
        IDictionary<int, Robot> RobotParking { get; }
        /// <summary>
        /// Keep a counter of created robots to assign a new id and key in the robotParking.
        /// </summary>
        int RobotCounter { get; set; }
        /// <summary>
        /// Set the active robot, by default is set to null which means no active robot.
        /// </summary>
        int ActiveRobot { get; set; }
        /// <summary>
        /// Keep playground properties.
        /// </summary>
        Playground Playground { get; set; }
        /// <summary>
        /// Parse a command and arguments from a string input.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ICommand ParseCommand(string input);
        /// <summary>
        /// Execute a command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        bool ExecuteCommand(ICommand command);
        /// <summary>
        /// Set the active robot. If not id is provided, set active robot id to 0 which means no active robot.
        /// </summary>
        /// <param name="id"></param>
        void Use();
        /// <summary>
        /// Set a specific robot as the active robot.
        /// </summary>
        /// <param name="id"></param>
        void Use(int id);
        /// <summary>
        /// List all created robots showing their ids and if were placed.
        /// </summary>
        void List();
        /// <summary>
        /// Place a robot on the workspace, if there`s none active robot create a new one and place in the workspace.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="placePosition"></param>
        void Place(Position placePosition);
        /// <summary>
        /// Move a specific robot by its id one unit at the faced direction.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Move(int id);
        /// <summary>
        /// Move the active robot one unit at the faced direction.
        /// </summary>
        void Move();
        /// <summary>
        /// Turn the face of the active robot 90ª to the left.
        /// </summary>
        /// <returns></returns>
        void TurnLeft();
        /// <summary>
        /// Turn the face of a specific robot by its id, 90º to the left.
        /// </summary>
        /// <param name="id"></param>
        void TurnLeft(int id);
        /// <summary>
        /// Turn the face of the active robot 90ª to the right.
        /// </summary>
        /// <returns></returns>
        void TurnRight();
        /// <summary>
        /// Turn the face of a specific robot by its id, 90º to the right
        /// </summary>
        /// <param name="id"></param>
        void TurnRight(int id);
        /// <summary>
        /// Reset the position of the active robot to the initial position. Log will be cleared.
        /// </summary>
        void Reboot();
        /// <summary>
        /// Reset the position of a specific robot to the initial position. Log will be cleared.
        /// </summary>
        void Reboot(int id);
        /// <summary>
        /// Remove the active robor from the playgrond. Log will be cleared.
        /// </summary>
        void Remove();
        /// <summary>
        /// Remove a specific robot from the playground. Log will be cleared.
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);
        /// <summary>
        /// Remove the active robot from the robot parking.
        /// </summary>
        void Destroy();
        /// <summary>
        /// Remove a specific robot from the robot parking by its id.
        /// </summary>
        /// <param name="id"></param>
        void Destroy(int id);
        /// <summary>
        /// Show the position of the current active robot.
        /// </summary>
        /// <returns></returns>
        void Report();
        /// <summary>
        /// Show the position of a specific robot by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Report(int id);
        /// <summary>
        /// Show the log of the active robot.
        /// </summary>
        void ShowLog();
        /// <summary>
        /// Show the log of a specific robot.
        /// </summary>
        /// <param name="id"></param>
        void ShowLog(int id);
        /// <summary>
        /// Switch on the active robot.
        /// </summary>
        void SwitchOn();
        /// <summary>
        /// Switch off a specific robot by its id.
        /// </summary>
        /// <param name="id"></param>
        void SwitchOn(int id);
        /// <summary>
        /// Switch off the active robot.
        /// </summary>
        void SwitchOff();
        /// <summary>
        /// Switch off a specific robot by its id.
        /// </summary>
        /// <param name="id"></param>
        void SwitchOff(int id);
        /// <summary>
        /// Revert the active roboto to the last position.
        /// </summary>
        void Undo();
        /// <summary>
        /// Revert a specific robot to the last position.
        /// </summary>
        /// <param name="id"></param>
        void Undo(int id);
        /// <summary>
        /// Exit to the application.
        /// </summary>
        void Quit();
    }
}
