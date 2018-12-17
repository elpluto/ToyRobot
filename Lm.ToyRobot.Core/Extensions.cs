using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.ToyRobot.Core
{
    /// <summary>
    ///  Extensions of core classes.
    /// Position members could not be copied 
    /// </summary>
    public static class CoreExtensions
    {
        /// <summary>
        /// Extend position class with a method to clone position objects allowing assigns positions by value.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Position Clone(this Position source)
        {
            var newPosition = new Position();
            newPosition.X = source.X;
            newPosition.Y = source.Y;
            newPosition.FaceTo = source.FaceTo;
            return newPosition;
        }
    }
}
