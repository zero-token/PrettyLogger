using Microsoft.Extensions.Logging;

namespace PrettyLogger
{
    public interface ILogContextBuilder : ILogger
    {
        /// <summary>
        /// Create a logger that enriches log events with the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property. Must be non-empty.</param>
        /// <param name="value">The property value.</param>
        /// <returns>A log context builder that will enrich log events as specified.</returns>
        ILogContextBuilder ForContext(string propertyName, object? value);
    }
}
