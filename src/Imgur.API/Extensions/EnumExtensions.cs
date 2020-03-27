using System;
using System.Collections.Generic;
using System.Text;

namespace Imgur.API.Extensions
{
    /// <summary>
    /// Class used to add enum extensions
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Method that return enum "Name" to lowercase.
        /// </summary>
        /// <param name="src">Current enum</param>
        /// <returns></returns>
        public static string ToLowerCase(this Enum src)
        {
            return src.ToString().ToLower();
        }
    }
}
