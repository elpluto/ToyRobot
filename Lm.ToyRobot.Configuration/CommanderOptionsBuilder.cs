using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Lm.ToyRobot.Configuration
{
    public class CommanderOptionsBuilder : ICommanderOptionsBuilder
    {
        /// <summary>
        /// Setup a commander options using the configuration from the appsettings file.
        /// A configuration builded object is provided.
        /// </summary>
        /// <param name="configuration"></param>
        public ICommanderOptions Build(IConfiguration configuration)
        {
            ICommanderOptions options = new CommanderOptions();
            //Set options from settings.
            options.MaxRobots = Convert.ToInt16(configuration["Commander:MaxRobots"]);
            options.PlaygroundWidth = Convert.ToInt16(configuration["Playground:Width"]);
            options.PlaygroundHeight = Convert.ToInt16(configuration["Playground:Height"]);
            options.AllowedBoundaries = Convert.ToBoolean(configuration["Commander:AllowedBoundaries"]);
            options.CollisionsDetected = Convert.ToBoolean(configuration["Commander:CollisionsDetected"]);
            options.StepSize = Convert.ToInt16(configuration["Robot:StepSize"]);
            return options;
        }
    }
}
