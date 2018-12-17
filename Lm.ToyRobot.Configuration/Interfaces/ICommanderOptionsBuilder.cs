using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Lm.ToyRobot.Configuration
{
    public interface ICommanderOptionsBuilder
    {
        ICommanderOptions Build(IConfiguration configuration);
    }
}
