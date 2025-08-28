using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace ResearchTickerQ
{
    public class JobLocker
    {
        private static readonly ConcurrentDictionary<string, object> _locks = new();

        public static void Run(Action jobAction, [CallerMemberName] string jobName = "")
        {
            var lockObj = _locks.GetOrAdd(jobName, _ => new object());

            if (!Monitor.TryEnter(lockObj))
            {
                // Jika job masih running → skip
                Console.WriteLine($"Job {jobName} masih running, skip eksekusi baru.");
                return;
            }

            try
            {
                Console.WriteLine($"Job {jobName} mulai: {DateTime.Now}");
                jobAction();
                Console.WriteLine($"Job {jobName} selesai: {DateTime.Now}");
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }
    }
}
