using System.Drawing;
using TickerQ.Utilities.Base;
using TickerQ.Utilities.Models;

namespace ResearchTickerQ
{
    public class MyJobs
    {
        private readonly ILogger<MyJobs> _logger;

        public MyJobs(ILogger<MyJobs> logger)
        {
            _logger = logger;
        }

        [TickerFunction("Job1", "*/1 * * * *")]
        public void Job1()
        {
            JobLocker.Run("Job1", () =>
            {
                _logger.LogInformation("Running Job 1...");
                Thread.Sleep(65000);
                _logger.LogInformation("Ending Job 1...");
            });
        }

        [TickerFunction("Job2", "*/1 * * * *")]
        public void Job2()
        {
            JobLocker.Run("Job2", () =>
            {
                _logger.LogInformation("Running Job 2...");
                Thread.Sleep(65000);
                _logger.LogInformation("Ending Job 2...");
            });
        }

        [TickerFunction("WithObject")]
        public void WithObject(TickerFunctionContext<Point> tickerContext)
        {
            _logger.LogInformation("Method called {Point}", tickerContext.Request);
        }
    }
}
