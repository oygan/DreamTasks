using System.Collections.Generic;
using System.Linq;

namespace Dream.Business.Extensions
{
    /// <summary>
    /// This extension works with the request arguments.
    /// </summary>
    public static class ArgumentsExtension
    {
        /// <summary>
        /// This extension extract specific model from the request arguments.
        /// </summary>
        public static TModel Extract<TModel>(this IDictionary<string, object> actionArguments)
        {
            return actionArguments.Values.Where(t => t is TModel).Cast<TModel>()
                .FirstOrDefault();
        }
    }
}