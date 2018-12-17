using System;
using System.IO;
using Lm.ToyRobot.Configuration;
using Lm.ToyRobot.Controller;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Lm.ToyRobot.Robot.Shell
{
    class Program
    {
        private static ICommanderOptions _options;
        private static ICommander _commander;
        private static IConfiguration _config;
        private static ILogger _logger;
        private static ILoggerFactory _loggerFactory;
        //
        static void Main(string[] args)
        {
            SetupLogging();
            SetupCommanderOptions();
            SetupCommander();
            ShowTitle();
            while (true){
                try
                {
                    ShowPrompt();
                    var command = _commander.ParseCommand(Console.ReadLine());
                    var result = _commander.ExecuteCommand(command);
                    //Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
        /// <summary>
        /// Configure and enable the logging system.
        /// </summary>
        static void SetupLogging()
        {
            _loggerFactory = new LoggerFactory();
            _logger = _loggerFactory.CreateLogger<Commander>();
        }
        /// <summary>
        /// Show the title information for the CLI application.
        /// </summary>
        static void ShowTitle()
        {
            Console.WriteLine("****************************************************************");
            Console.WriteLine("                     Toy Robot Simulator                        ");
            Console.WriteLine("                        Version 1.0                             ");
            Console.WriteLine("                 Programmed by Rafa Hernández                   ");
            Console.WriteLine("****************************************************************");
        }
        /// <summary>
        /// Build and show the command prompt.
        /// </summary>
        static void ShowPrompt()
        {
            if (_commander.ActiveRobot != 0)
            {
                Console.Write($"Robocom ({_commander.ActiveRobot}):>");
            }
            else
            {
                Console.Write("Robocom:>");
            }
        }
        /// <summary>
        /// Create and configure a commander with a builded commander options object.
        /// </summary>
        static void SetupCommander()
        {
            _commander = new Commander(_options);
        }
        /// <summary>
        /// Setup the options to configure a commander using configuration api from a json file.
        /// </summary>
        static void SetupCommanderOptions()
        {
            //  Build the options and setup a commander.
            _config= new ConfigurationBuilder()
                .AddJsonFile(Directory.GetCurrentDirectory() + "/appsettings.json")
                .Build();
            _options = new CommanderOptionsBuilder().Build(_config);

        }
    }
}
