using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.ToyRobot.Configuration;

namespace Lm.ToyRobot.Core.Validation
{
    /// <summary>
    /// Validator class to perform validations and keep a error history-
    /// </summary>
    public class Validator : IValidator
    {
        /// <summary>
        /// Keep the boolean result of a validation.
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// Keep a collection of errors produced during the validation.
        /// </summary>
        public IList<Error> Errors { get; set; }
        /// <summary>
        /// Ctor
        /// </summary>
        public Validator()
        {
            Errors = new List<Error>();
            IsValid = false;
        }
        /// <summary>
        /// Validate the coordinates of position values according to the setup of the playground.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public IValidator Validate(Position position, Playground playground)
        {
            IValidator result = new Validator();
            //Initialize validation to true and change to false on errors.
            result.IsValid = true;
            //
            if (position.X > playground.Width)
                result.Errors.Add(
                    new Error
                    {
                        Number = 1,
                        Message = $"Value X is greater than the playground limit of {playground.Width}."
                    });
            if (position.X < 0)
                result.Errors.Add(
                    new Error
                    {
                        Number = 5,
                        Message = $"Value X is smaller than 0, negative coordinates are not allowed."
                    });
            if (position.X == playground.Width && !playground.AllowedBoundaries)
                result.Errors.Add(
                    new Error
                    {
                        Number = 11,
                        Message = $"Boundaries are not allowed and value X is equal to the playground limit of {playground.Width}."
                    });
            if (position.Y > playground.Height)
                result.Errors.Add(
                    new Error
                    {
                        Number = 2,
                        Message = $"Value Y is greater than the playground limit of {playground.Height}."
                    });
            if (position.Y < 0)
                result.Errors.Add(
                    new Error
                    {
                        Number = 6,
                        Message = $"Value Y is smaller than 0 and negative coordinates are not allowed."
                    });
            if (position.Y == playground.Height && !playground.AllowedBoundaries)
                result.Errors.Add(
                    new Error
                    {
                        Number = 12,
                        Message = $"Boundaries are not allowed and value Y is equal to the playground limit of {playground.Height}."
                    });
            if (result.Errors.Count > 0) result.IsValid = false;
            return result;
        }
    }
}
