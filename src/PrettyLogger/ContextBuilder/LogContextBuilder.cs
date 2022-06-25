using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace PrettyLogger.ContextBuilder
{
    internal sealed class LogContextBuilder : ILogContextBuilder
    {
        private readonly ILogger _logger;
        private readonly List<KeyValuePair<string, object?>> _properties = new();

        public LogContextBuilder(ILogger logger)
        {
            _logger = logger;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (_properties.Count == default)
            {
                _logger.Log(logLevel, eventId, state, exception, formatter);
                return;
            }

            using var d = _logger.BeginScope(_properties);
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _logger.BeginScope(state);
        }

        public ILogContextBuilder ForContext(string propertyName, object? value)
        {
            _properties.Add(new KeyValuePair<string, object?>(propertyName, value));
            return this;
        }
    }
}
