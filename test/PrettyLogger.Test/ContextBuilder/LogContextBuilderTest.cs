using FluentAssertions;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.InMemory;
using Serilog.Sinks.InMemory.Assertions;
using Xunit;

namespace PrettyLogger.Test.ContextBuilder
{
    public sealed class LogContextBuilderTest
    {
        private readonly ILoggerFactory _loggerFactory;

        public LogContextBuilderTest()
        {
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(
                    new LoggerConfiguration()
                        .WriteTo.InMemory()
                        .CreateLogger());
            });
        }

        [Theory]
        [InlineData("Message template", "Number", 0)]
        public void ForContextSimpleTest(string messageTemplate, string propertyName, int value)
        {
            var logger = _loggerFactory.CreateLogger<LogContextBuilderTest>();

            logger.ForContext(propertyName, value).LogInformation(messageTemplate);

            InMemorySink.Instance
                .Should()
                .HaveMessage(messageTemplate)
                .Appearing().Once()
                .WithProperty("SourceContext").WithValue(typeof(LogContextBuilderTest).FullName)
                .And
                .WithProperty(propertyName).WhichValue<int>().Should().Be(value);
        }
    }
}
