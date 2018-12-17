using System;
using System.Collections.Generic;
using Lm.ToyRobot.Configuration;
using Lm.ToyRobot.Controller.Enumerations;
using Lm.ToyRobot.Core;
using Lm.ToyRobot.Core.Enumerations;
using Lm.ToyRobot.Core.Validation;

namespace Lm.ToyRobot.Controller
{
    /// <summary>
    /// Commander class. Control and manage robots
    /// </summary>
    public class Commander : ICommander
    {
        /// <summary>
        /// Maximum number of robots allowed in parking. Is set by confiuration.
        /// </summary>
        public int MaxRobots { get; set; }
        /// <summary>
        /// Minimun size of a movement step.
        /// </summary>
        public int StepSize { get; set; }
        /// <summary>
        /// Keep a store of existing robots. Destroyed robots will be removed from the store.
        /// </summary>
        public IDictionary<int,Robot> RobotParking { get; }
        /// <summary>
        /// Keep a counter of created robots to assign a new id and key in the robotParking.
        /// </summary>
        public int RobotCounter { get; set; }
        /// <summary>
        /// Set the active robot, by default is set to null which means no active robot.
        /// </summary>
        public int ActiveRobot { get; set; }
        /// <summary>
        /// Keep playground properties.
        /// </summary>
        public Playground Playground { get; set; }
        /// <summary>
        /// internal field to reference a commands parser.
        /// </summary>
        private readonly ICommandParser _commandParser;
        /// <summary>
        /// Comander constructor.
        /// </summary>
        /// <param name="options"></param>
        public Commander(ICommanderOptions options)
        {
            //Private members initialization.
            _commandParser = new CommandParser();
            //Setup the playground.
            Playground = new Playground()
            {
                Width = options.PlaygroundWidth,
                Height = options.PlaygroundHeight,
                AllowedBoundaries = options.AllowedBoundaries,
                CollisionsDetected = options.CollisionsDetected
            };
            //Public properties initialization.
            MaxRobots = options.MaxRobots;
            StepSize = options.StepSize;
            RobotParking = new Dictionary<int,Robot>();
            RobotCounter = 1;   
        }
        /// <summary>
        /// Parse a command and arguments from a string input through the commandParser.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ICommand ParseCommand(string input)
        {
            return _commandParser.ParseInput(input);
        }     
        /// <summary>
        /// Execute a command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool ExecuteCommand(ICommand command)
        {
            switch (command.Key)
            {
                case CommandEnum.QUIT:
                    Quit();
                    break;
                case CommandEnum.HELP:
                    ShowHelp();
                    break;
                case CommandEnum.LIST:
                    List();
                    break;
                //  USE Command.
                case CommandEnum.USE:
                    if (command.Arguments == null)
                    {
                        Use();
                    }
                    else
                    {
                        Use(command.Arguments[0]);
                    }
                    break;
                case CommandEnum.PLACE:
                    var placePosition = new Position
                    {
                        X = command.Arguments[0],
                        Y = command.Arguments[1],
                        FaceTo = (OrientationEnum)command.Arguments[2]
                    };
                    Place(placePosition);
                    break;
                case CommandEnum.LEFT:
                    if (command.Arguments == null)
                    {
                        TurnLeft();
                    }
                    else
                    {
                        TurnLeft(command.Arguments[0]);
                    }
                    break;
                case CommandEnum.RIGHT:
                    if (command.Arguments == null)
                    {
                        TurnRight();
                    }
                    else
                    {
                        TurnRight(command.Arguments[0]);
                    }
                    break;
                case CommandEnum.MOVE:
                    if (command.Arguments == null)
                    {
                        Move();
                    }
                    else
                    {
                        Move(command.Arguments[0]);
                    }
                    break;
                case CommandEnum.REPORT:
                    if (command.Arguments == null)
                    {
                        Report();
                    }
                    else
                    {
                        Report(command.Arguments[0]);
                    }
                    break;
                case CommandEnum.REBOOT:
                    if (command.Arguments == null)
                    {
                        Reboot();
                    }
                    else
                    {
                        Reboot(command.Arguments[0]);
                    }
                    break;
                case CommandEnum.REMOVE:
                    if (command.Arguments == null)
                    {
                        Remove();
                    }
                    else
                    {
                        Remove(command.Arguments[0]);
                    }
                    break;
                case CommandEnum.DESTROY:
                    if (command.Arguments == null)
                    {
                        Destroy();
                    }
                    else
                    {
                        Destroy(command.Arguments[0]);
                    }
                    break;
                case CommandEnum.SHOWLOG:
                    if (command.Arguments == null)
                    {
                        ShowLog();
                    }
                    else
                    {
                        ShowLog(command.Arguments[0]);
                    }
                    break;
                case CommandEnum.UNDO:
                    if (command.Arguments == null)
                    {
                        Undo();
                    }
                    else
                    {
                        Undo(command.Arguments[0]);
                    }
                    break;
                case CommandEnum.ON:
                    if (command.Arguments == null)
                    {
                        SwitchOn();
                    }
                    else
                    {
                        SwitchOn(command.Arguments[0]);
                    }
                    break;
                case CommandEnum.OFF:
                    if (command.Arguments == null)
                    {
                        SwitchOff();
                    }
                    else
                    {
                        SwitchOff(command.Arguments[0]);
                    }
                    break;
                default:
                    break;
                    
            }
            return true;
        }
        /// <summary>
        /// Set the active robot to 0 which allow to create a new robot using the PLACE command.
        /// </summary>
        public void Use() => ActiveRobot = 0;
        /// <summary>
        /// Set a specific robot as the active robot.
        /// All movements commands are relative to the active robot.
        /// All management commands without the Id argument are executed over the active robot.
        /// If not exits leave untouched the current active robot.
        /// </summary>
        /// <param name="id"></param>
        public void Use(int id)
        {
            if (RobotParking.ContainsKey(id))
            {
                ActiveRobot = id;
            }
            else
            {
                //  Provided Id not exist in the roboparking dictionary.
                Console.WriteLine("A robot with that Id does not exist.");
            }
        }
        /// <summary>
        /// List all created robots showing their ids and if were placed.
        /// </summary>
        public void List()
        {
            int counter=1; //For list index purposes.
            Console.WriteLine("");
            Console.WriteLine(" ------------------------------------------------------------------");
            if (RobotParking.Count == 0) Console.WriteLine("  There's no robots in the parking.");
            foreach (var robotParked in RobotParking)
            {
                var robot = robotParked.Value;
                if (robot.Id == ActiveRobot)
                {
                    Console.Write($"  {counter}: *[Id={robotParked.Key}]");
                }
                else
                {
                    Console.Write($"  {counter}:  [Id={robotParked.Key}]");
                }
                // Write current position.
                if (robot.IsPlaced)
                {
                    Console.Write($"; Position: {robot.CurrentPosition.X},{robot.CurrentPosition.Y},{Enum.GetName(typeof(OrientationEnum), robot.CurrentPosition.FaceTo)}");
                }
                Console.Write(robot.IsActivated? "; ON": "; OFF");
                Console.Write(robot.IsPlaced?"; PLACED":"; REMOVED");
                Console.WriteLine("");
                counter++;
            }
            Console.WriteLine(" -----------------------------------------------------------------");
            Console.WriteLine("");
        }
        /// <summary>
        /// Place overload using a Position type argument.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="placePosition"></param>
        public void Place(Position placePosition)
        {
            //Validate the coordinate values of the place position againt the playground constraints.
            IValidator positionValidator = new Validator().Validate(placePosition, Playground);
            if (positionValidator.IsValid)
            {
                //Check if exist an active robot. PLACE a new robot or USE command enable a robot.
                if (ActiveRobot != 0)
                {
                    //Place a existing robot if it is not placed because has been removed.
                    if (!RobotParking[(int)ActiveRobot].IsPlaced)
                    {
                        RobotParking[(int)ActiveRobot].Place(placePosition);
                    }
                    else
                    {
                        Console.WriteLine("Currently active robot is placed, try USE comamnd to reset the active robot or USE [Id] to place a removed robot.");
                    }
                }
                else
                {
                    //First check if the max robots limit is reached before create a new robot.
                    if (RobotParking.Count <= (MaxRobots - 1))
                    {
                        var newId = RobotCounter;
                        var newRobot = new Robot() { Id = newId, IsActivated = true };
                        //Place a new robot on the playground.
                        if (newRobot.Place(placePosition)) ActiveRobot = newId;
                        RobotParking.Add(newId, newRobot);
                        RobotCounter++;
                    }
                    else
                    {
                        Console.WriteLine($"The limit of robots existing simultaneously was reached. The limit is {MaxRobots}");
                    }
                }
            }
            else
            {
                Console.WriteLine("The next position of the robot is not valid.");
                Console.WriteLine("Error summary:");
                foreach (var error in positionValidator.Errors)
                {
                    Console.WriteLine($"({error.Number}):{error.Message}");
                }
            }
        }
        /// <summary>
        /// Move a specific robot to the face direction by id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Move(int id)
        {
            if (RobotParking.ContainsKey(id))
            {
                var predictedPosition = new Position();
                predictedPosition.X = RobotParking[id].CurrentPosition.X;
                predictedPosition.Y = RobotParking[id].CurrentPosition.Y;
                predictedPosition.FaceTo = RobotParking[id].CurrentPosition.FaceTo;
                //  Predicted new position.
                switch (predictedPosition.FaceTo)
                {
                    case OrientationEnum.NORTH:
                        predictedPosition.Y += StepSize;
                        break;
                    case OrientationEnum.SOUTH:
                        predictedPosition.Y -= StepSize;
                        break;
                    case OrientationEnum.EAST:
                        predictedPosition.X += StepSize;
                        break;
                    case OrientationEnum.WEST:
                        predictedPosition.X -= StepSize;
                        break;
                }
                //  Check if predicted position is valid according playground contraints.
                var positionValidator = new Validator().Validate(predictedPosition, Playground);
                if (positionValidator.IsValid)
                {
                    //  Do a real movement.
                    RobotParking[id].Move(StepSize);
                }
                else
                {
                    Console.WriteLine("The next position of the robot is not valid.");
                    Console.WriteLine("Error summary:");
                    foreach (var error in positionValidator.Errors)
                    {
                        Console.WriteLine($"({error.Number}):{error.Message}");
                    }
                }
            }
            else
            {
                //  Provided Id not exist in the roboparking dictionary.
                Console.WriteLine("A robot with that Id does not exist.");
            }
            
        }
        /// <summary>
        /// Move the active robot to the face direction.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Move()
        {
            if (ActiveRobot != 0)
            {
                //  Use overload by id.
                Move(ActiveRobot);
            }
            else
            {
                // There's not an active robot.
                Console.WriteLine("There's no active robot. Try USE [Id] to select a robot.");
            }
        }
        /// <summary>
        /// Turn the face of the active robot 90ª to the left.
        /// </summary>
        /// <returns></returns>
        public void TurnLeft()
        {
            if (ActiveRobot != 0)
            {
                TurnLeft(ActiveRobot);
            }
            else
            {
                // There's not an active robot.
                Console.WriteLine("There's no active robot. Try USE [Id] to select a robot.");
            }
        }
        /// <summary>
        /// Turn the face of a specific robot by its id, 90º to the left.
        /// </summary>
        /// <param name="id"></param>
        public void TurnLeft(int id)
        {
            if (RobotParking.ContainsKey(id))
            {
                RobotParking[id].TurnLeft();
            }
            else
            {
                //  Provided Id not exist in the roboparking dictionary.
                Console.WriteLine("A robot with that Id does not exist.");
            }
        }
        /// <summary>
        /// Turn the face of the active robot 90ª to the right.
        /// </summary>
        /// <returns></returns>
        public void TurnRight()
        {
            if (ActiveRobot != 0)
            {
                TurnRight(ActiveRobot);
            }
            else
            {
                // There's not an active robot.
                Console.WriteLine("There's no active robot. Try USE [Id] to select a robot.");
            }
        }
        /// <summary>
        /// Turn the face of a specific robot by its id, 90º to the right
        /// </summary>
        /// <param name="id"></param>
        public void TurnRight(int id)
        {
            if (RobotParking.ContainsKey(id))
            {
                RobotParking[id].TurnRight();
            }
            else
            {
                //  Provided Id not exist in the roboparking dictionary.
                Console.WriteLine("A robot with that Id does not exist.");
            }
        }
        /// <summary>
        /// Show position of the current active robot.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Report()
        {
            if (ActiveRobot != 0)
            {
                var position = RobotParking[ActiveRobot].Report();
                Console.WriteLine($"Position ({ActiveRobot}): {position.X},{position.Y}, {Enum.GetName(typeof(OrientationEnum), position.FaceTo)}");
            }

            else
            {
                // There's not an active robot.
                Console.WriteLine("There's no active robot. Try USE [Id] to select a robot.");
            }
        }
        /// <summary>
        /// Show the position of a specific robot by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Report(int id)
        {
            if (RobotParking.ContainsKey(id))
            {
                var position = RobotParking[id].Report();
                Console.WriteLine("");
                Console.Write($"  Position ({id}): {position.X},{position.Y}, {Enum.GetName(typeof(OrientationEnum), position.FaceTo)}");
                Console.WriteLine("");
            }
            else
            {
                //  Provided Id not exist in the roboparking dictionary.
                Console.WriteLine("A robot with that Id does not exist.");
            }
        }
        /// <summary>
        /// Show the log of the active robot.
        /// </summary>
        public void ShowLog()
        {
            if (ActiveRobot != 0)
            {
                ShowLog(ActiveRobot);
            }
            else
            {
                // There's not an active robot.
                Console.WriteLine("There's no active robot. Try USE [Id] to select a robot.");
            }
        }
        /// <summary>
        /// Show the log of a specific robot.
        /// </summary>
        /// <param name="id"></param>
        public void ShowLog(int id)
        {
            if (RobotParking.ContainsKey(id))
            {
                Console.WriteLine("");
                Console.WriteLine(" Log Summary Robot[{id}]:");
                Console.WriteLine(" ------------------------------------------------------------------");
                foreach (var entry in RobotParking[id].Log)
                {
                    Console.WriteLine($" {entry.Time}:{entry.Position.X}, {entry.Position.Y}, {Enum.GetName(typeof(OrientationEnum), entry.Position.FaceTo)}");
                }
                Console.WriteLine(" ------------------------------------------------------------------");
                Console.WriteLine("");
            }
            else
            {
                //  Provided Id not exist in the roboparking dictionary.
                Console.WriteLine("A robot with that Id does not exist.");
            }
        }
        /// <summary>
        /// Remove the active robot from the robot parking.
        /// </summary>
        public void Destroy()
        {
            if (ActiveRobot != 0)
            {
                //Remove the robot from the parking and reset the active robot.
                RobotParking.Remove(ActiveRobot);
                ActiveRobot = 0;
            }
            else
            {
                // There's not an active robot.
                Console.WriteLine("There's no active robot. Try USE [Id] to select a robot.");
            }
        }
        /// <summary>
        /// Remove a specific robot from the robot parking by its id.
        /// </summary>
        /// <param name="id"></param>
        public void Destroy(int id)
        {
            if (RobotParking.ContainsKey(id))
            {
                //Remove the robot from the parking.
                RobotParking.Remove(id);
                //if the id is the current active robot, reset the active robot. If not remains the current value.
                if(ActiveRobot == id) ActiveRobot = 0;
            }
            else
            {
                //  Provided Id not exist in the roboparking dictionary.
                Console.WriteLine("A robot with that Id does not exist.");
            }
        }
        /// <summary>
        /// Reset the position of the active robot to the initial position. Log will be cleared.
        /// </summary>
        public void Reboot()
        {
            if (ActiveRobot != 0)
            {
                Reboot(ActiveRobot);
            }
            else
            {
                // There's not an active robot.
                Console.WriteLine("There's no active robot. Try USE [Id] to select a robot.");
            }
        }
        /// <summary>
        /// Reset the position of a specific robot to the initial position. Log will be cleared.
        /// </summary>
        /// <param name="id"></param>
        public void Reboot(int id)
        {
            if (RobotParking.ContainsKey(id))
            {
                RobotParking[id].Reboot();
            }
            else
            {
                //  Provided Id not exist in the roboparking dictionary.
                Console.WriteLine("A robot with that Id does not exist.");
            }
        }
        /// <summary>
        /// Remove the active robor from the playgrond. Log will be cleared.
        /// </summary>
        public void Remove()
        {
            if (ActiveRobot != 0)
            {
                Remove(ActiveRobot);
            }
            else
            {
                // There's not an active robot.
                Console.WriteLine("There's no active robot. Try USE [Id] to select a robot.");
            }
        }
        /// <summary>
        /// Remove a specific robot from the playground. Log will be cleared.
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            if (RobotParking.ContainsKey(id))
            {
                RobotParking[id].Remove();
            }
            else
            {
                //  Provided Id not exist in the roboparking dictionary.
                Console.WriteLine("A robot with that Id does not exist.");
            }
        }
        /// <summary>
        /// Switch on the active robot.
        /// </summary>
        public void SwitchOn()
        {
            if (ActiveRobot != 0)
            {
                SwitchOn(ActiveRobot);
            }
            else
            {
                // There's not an active robot.
                Console.WriteLine("There's no active robot. Try USE [Id] to select a robot.");
            }
        }
        /// <summary>
        /// Switch off a specific robot by its id.
        /// </summary>
        /// <param name="id"></param>
        public void SwitchOn(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Switch off the active robot.
        /// </summary>
        public void SwitchOff()
        {
            if (ActiveRobot != 0)
            {
                SwitchOff(ActiveRobot);
            }
            else
            {
                // There's not an active robot.
                Console.WriteLine("There's no active robot. Try USE [Id] to select a robot.");
            }
        }
        /// <summary>
        /// Switch off a specific robot by its id.
        /// </summary>
        /// <param name="id"></param>
        public void SwitchOff(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Revert the active roboto to the last position.
        /// </summary>
        public void Undo()
        {
            if (ActiveRobot != 0)
            {
                Undo(ActiveRobot);
            }
            else
            {
                // There's not an active robot.
                Console.WriteLine("There's no active robot. Try USE [Id] to select a robot.");
            }
        }
        /// <summary>
        /// Revert a specific robot to the last position.
        /// </summary>
        /// <param name="id"></param>
        public void Undo(int id)
        {
            if (RobotParking.ContainsKey(id))
            {
                RobotParking[id].Undo();
            }
            else
            {
                //  Provided Id not exist in the roboparking dictionary.
                Console.WriteLine("A robot with that Id does not exist.");
            }
        }
        /// <summary>
        /// Exit to the application.
        /// </summary>
        public void Quit()
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// Show help of the CLI use.
        /// </summary>
        private void ShowHelp()
        {
            Console.WriteLine("");
            Console.WriteLine(" Command List:");
            Console.WriteLine(" ---------------------------------------------------------------------------");
            Console.WriteLine(" USE [id]: Select and active robot by id. using without arguments leave all selections.");
            Console.WriteLine("           \"use [id]\", samples \"use 2\", \"use\".");
            Console.WriteLine("");
            Console.WriteLine(" LIST: List all robots existing in the parking.");
            Console.WriteLine("");
            Console.WriteLine(" PLACE x,y,F: Place a robot in the playground at a position and faced to a direction, created a new one if not exits.");
            Console.WriteLine("              \"place 2,4,north\". F argument could be: north, south, east, west.");
            Console.WriteLine("");
            Console.WriteLine(" MOVE [id]: Move the active robot or a specific robot one step in the face direction.");
            Console.WriteLine("            \"MOVE 3\", \"MOVE\"");
            Console.WriteLine("");
            Console.WriteLine(" LEFT [id]: Turn the face direction 90º to the left.");
            Console.WriteLine("");
            Console.WriteLine(" REPORT [id]: Show the current position of the active robot or a specific robot..");
            Console.WriteLine("");
            Console.WriteLine(" REBOOT [id]: Reset the position of the active robot or a specific robot to the initial position.");
            Console.WriteLine("");
            Console.WriteLine(" DESTROY [id]: Delete the active robot or a specific robot..");
            Console.WriteLine("");
            Console.WriteLine(" REMOVE [id]: Remove the active robot or a specific robot from the playground.");
            Console.WriteLine("");
            Console.WriteLine(" SHOWLOG [id]: Show the movements and changes log of the active robot or a specific robot..");
            Console.WriteLine("");
            Console.WriteLine(" ON [id]: Switch on the active robot or a specific robot.");
            Console.WriteLine("");
            Console.WriteLine(" OFF [id]: Switch off the active robot or a specific robot.");
            Console.WriteLine("");
            Console.WriteLine(" QUIT: Exit the application.");
            Console.WriteLine("");
            Console.WriteLine(" HELP: Show command help.");
            Console.WriteLine("");
        }
    }
}
