using System.Drawing;
using TickerQ.Utilities.Base;
using TickerQ.Utilities.Models;

namespace ResearchTickerQ
{
    public class MyJobs
    {
        private readonly ILogger<MyJobs> _logger;
        private static bool _isRunning = false;

        public MyJobs(ILogger<MyJobs> logger)
        {
            _logger = logger;
        }

        [TickerFunction("CleanUp", "*/1 * * * *")]
        public void CleanUpLogs()
        {
            if (_isRunning)
            {
                _logger.LogWarning("Job masih running, skip eksekusi baru.");
                return;
            }

            try
            {
                _isRunning = true;
                _logger.LogInformation($"{DateTime.Now} Job Run....");

                Thread.Sleep(65000);

                _logger.LogInformation($"{DateTime.Now} End Job....");
            }
            finally
            {
                _isRunning = false;
            }
        }

        [TickerFunction("WithObject")]
        public void WithObject(TickerFunctionContext<Point> tickerContext)
        {
            _logger.LogInformation("Method called {Point}", tickerContext.Request);
        }
    }
}
