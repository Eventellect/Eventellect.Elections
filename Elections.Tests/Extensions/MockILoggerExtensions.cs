using Microsoft.Extensions.Logging;

namespace Elections.Tests.Extensions;
public static class MockILoggerExtensions
{
    public static void VerifyLog<T>(this Mock<ILogger<T>> mockLogger, LogLevel logLevel, Func<Times> times,
        string? textContains = null)
        => VerifyLog(mockLogger, logLevel, times.Invoke(), textContains);

    public static void VerifyLog<T>(this Mock<ILogger<T>> mockLogger, LogLevel logLevel, Times times, string? textContains = null)
    {
        mockLogger.Verify(x => x.Log(
            logLevel,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => textContains == null || v.ToString()!.Contains(textContains)),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), 
            times);
    }
}
