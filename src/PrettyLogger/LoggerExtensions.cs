using Microsoft.Extensions.Logging;
using PrettyLogger.ContextBuilder;

namespace PrettyLogger
{
    public static class LoggerExtensions
    {
        /// <summary>
        /// Create a logger that enriches log events with the specified property.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        /// <param name="propertyName">The name of the property. Must be non-empty.</param>
        /// <param name="value">The property value.</param>
        /// <returns>A log context builder that will enrich log events as specified.</returns>
        public static ILogContextBuilder ForContext(this ILogger logger, string propertyName, object? value)
        {
            var builder = logger as ILogContextBuilder ?? new LogContextBuilder(logger);
            return builder.ForContext(propertyName, value);
        }
    }
}
