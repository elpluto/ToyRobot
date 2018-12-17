using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.ToyRobot.Controller.Enumerations;
using Lm.ToyRobot.Core.Enumerations;

namespace Lm.ToyRobot.Controller
{
    /// <summary>
    /// Command parser class.
    /// This class is responsible of the checking and validation of the input commands and transform these inputs into a valid command object which could be executed by the commander.
    /// </summary>
    public class CommandParser : ICommandParser
    {
        /// <summary>
        /// /// Parst the input to create a command.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ICommand ParseInput(string input)
        {         
            var inputParts = input.Split(' ');
            //
            var command = new Command();
            //Checking and parsing the command keyword.
            command.Key = ParseCommand(inputParts[0]);
            //Checking  and parsing arguments, if no arguments set commands args collection to null.
            if (inputParts.Length > 1)
            {
                command.Arguments = ParseArguments(command.Key, inputParts[1]);
            }
            else //no arguments.
            {
                command.Arguments = null;
            }
            
            return command;
        }
        /// <summary>
        /// Checking validation and parsing arguments.
        /// Only check validation for commands which accept arguments.
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="inputArgs"></param>
        /// <returns></returns>
        private IList<int> ParseArguments(CommandEnum commandKey, string inputArgs)
        {
            //Get raw arguments by splitting the input predicate.
            var args = inputArgs.Split(',');
            //Creates an arguments list parsing string values to integer equivalences.
            var commandArgs = new List<int>();
            switch (commandKey) {
                case CommandEnum.HELP:
                    if (args.Length > 0)
                    {
                        throw new ArgumentException("Invalid number of arguments. HELP has not arguments.");
                    }
                    break;
                case CommandEnum.LIST:
                    if (args.Length > 0)
                    {
                        throw new ArgumentException("Invalid number of arguments. LIST has not arguments.");
                    }
                    break;
                case CommandEnum.USE:
                    if (args.Length > 1)
                    {
                        throw new ArgumentException("Invalid number of argument for a USE commands\n. Only one argument is allowed.");
                    }
                    // Id argument.
                    commandArgs.Add(Convert.ToInt16(args[0]));
                    break;
                case CommandEnum.PLACE:
                    if (args.Length != 3)
                    {
                        throw new ArgumentException("Invalid number of argument for a PLACE commands.\n 3 arguments are expected: \"PLACE x,y,F\", where F has valid values: north, south, east or west.");
                    }
                    // X
                    commandArgs.Add(Convert.ToInt16(args[0]));
                    // Y
                    commandArgs.Add(Convert.ToInt16(args[1]));
                    // F argument: values allowed from orientation enumeration.
                    if (Enum.IsDefined(typeof(OrientationEnum), args[2].ToUpper()))
                    {
                        //Get the int value of the enumeration equivalent to the input argument.
                        commandArgs.Add((int)Enum.Parse(typeof(OrientationEnum), args[2].ToUpper()));
                    }
                    else
                    {
                        throw new ArgumentException("Invalid orientation argument for PLACE command.\n Allowed values are: north, south, east, west.");
                    }
                    break;
                case CommandEnum.MOVE:
                    if (args.Length > 0)
                    {
                        throw new ArgumentException("Invalid number of arguments. MOVE has not arguments.");
                    }
                    break;
                case CommandEnum.LEFT:
                    if (args.Length > 0)
                    {
                        throw new ArgumentException("Invalid number of arguments. LEFT has not arguments.");
                    }
                    break;
                case CommandEnum.RIGHT:
                    if (args.Length > 0)
                    {
                        throw new ArgumentException("Invalid number of arguments. RIGHT has not arguments.");
                    }
                    break;
                case CommandEnum.REPORT:
                    if (args.Length > 1)
                    {
                        throw new ArgumentException("Invalid number of argument for a REBOOT commands\n. Only one argument is allowed.");
                    }
                    // Id argument.
                    commandArgs.Add(Convert.ToInt16(args[0]));
                    break;
                case CommandEnum.REBOOT:
                    if (args.Length > 1)
                    {
                        throw new ArgumentException("Invalid number of argument for a REBOOT commands\n. Only one argument is allowed.");
                    }
                    // Id argument.
                    commandArgs.Add(Convert.ToInt16(args[0]));
                    break;
                case CommandEnum.REMOVE:
                    if (args.Length > 1)
                    {
                        throw new ArgumentException("Invalid number of argument for a REMOVE commands\n. Only one argument is allowed.");
                    }
                    // Id argument.
                    commandArgs.Add(Convert.ToInt16(args[0]));
                    break;
                case CommandEnum.DESTROY:
                    if (args.Length > 1)
                    {
                        throw new ArgumentException("Invalid number of argument for a DESTROY commands\n. Only one argument is allowed.");
                    }
                    // Id argument.
                    commandArgs.Add(Convert.ToInt16(args[0]));
                    break;
                case CommandEnum.SHOWLOG:
                    if (args.Length > 1)
                    {
                        throw new ArgumentException("Invalid number of argument for a SHOWLOG commands\n. Only one argument is allowed.");
                    }
                    // Id argument.
                    commandArgs.Add(Convert.ToInt16(args[0]));
                    break;
                case CommandEnum.ON:
                    if (args.Length > 1)
                    {
                        throw new ArgumentException("Invalid number of argument for a ON commands\n. Only one argument is allowed.");
                    }
                    break;
                case CommandEnum.OFF:
                    if (args.Length > 1)
                    {
                        throw new ArgumentException("Invalid number of argument for a OFF commands\n. Only one argument is allowed.");
                    }
                    break;
                case CommandEnum.UNDO:
                    if (args.Length > 1)
                    {
                        throw new ArgumentException("Invalid number of argument for a UNDO commands\n. Only one argument is allowed.");
                    }
                    break;
            }

            return commandArgs;
        }
        /// <summary>
        /// Parsing a keyword to a valid command. 
        /// Throwing and exception if its a not valid keyword or the keyword not correspond to a existing command.
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private CommandEnum ParseCommand(string keyWord)
        {
            if (Enum.IsDefined(typeof(CommandEnum), keyWord.ToUpper()))
            {
                return Enum.Parse<CommandEnum>(keyWord.ToUpper());
            }
            else
            {
                throw new ArgumentException("Syntax error or command not found. The command keyword introduced is not valid.");
            }
        }
    }
}
