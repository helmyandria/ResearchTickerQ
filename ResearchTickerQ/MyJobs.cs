using System.Drawing;
using TickerQ.Utilities.Base;
using TickerQ.Utilities.Models;

namespace ResearchTickerQ
{
    public class MyJobs
    {
        private readonly ILogger<MyJobs> _logger;
        private static bool _isRunningJob1 = false;
        private static bool _isRunningJob2 = false;

        public MyJobs(ILogger<MyJobs> logger)
        {
            _logger = logger;
        }

        [TickerFunction("Job1", "*/1 * * * *")]
        public void Job1()
        {
            if (_isRunningJob1)
            {
                _logger.LogWarning("Job 1 masih running, skip eksekusi baru.");
                return;
            }

            try
            {
                _isRunningJob1 = true;
                _logger.LogInformation($"{DateTime.Now} Job 1 Run....");

                Thread.Sleep(65000);

                _logger.LogInformation($"{DateTime.Now} End 1 Job....");
            }
            finally
            {
                _isRunningJob1 = false;
            }
        }

        [TickerFunction("Job2", "*/1 * * * *")]
        public void Job2()
        {
            if (_isRunningJob2)
            {
                _logger.LogWarning("Job 2 masih running, skip eksekusi baru.");
                return;
            }

            try
            {
                _isRunningJob2 = true;
                _logger.LogInformation($"{DateTime.Now} Job 2 Run....");

                Thread.Sleep(65000);

                _logger.LogInformation($"{DateTime.Now} End 2 Job....");
            }
            finally
            {
                _isRunningJob2 = false;
            }
        }

        [TickerFunction("WithObject")]
        public void WithObject(TickerFunctionContext<Point> tickerContext)
        {
            _logger.LogInformation("Method called {Point}", tickerContext.Request);
        }
    }
}
