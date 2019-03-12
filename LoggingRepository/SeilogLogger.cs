using System;
using System.Runtime.CompilerServices;
using RepositoryRule.LoggerRepository;
using Serilog.Core;

namespace LoggingRepository
{
    public class SeilogLogger : ILoggerRepository
    {
        private Logger log;
        private ILoggerRepository _log;
        public SeilogLogger(Logger log, ILoggerRepository logger)
        {
            this.log = log;
            _log = logger;
        }

      
        public void CatchError(string text, long time, object obj, Exception exception, string methodName, string GUID, [CallerLineNumber] int linenumber = 0)
        {
            log.Error(exception, text + "methodName:" + methodName, new object[] { obj });
        }

       
        public void Logging(string text, long time, object obj, string methodName, [CallerLineNumber] int linenumber = 0)
        {
            log.Information(text + "methodName:{2}, data:{1}, tiem:{0}: lineNumber:{3} ", time, obj, methodName, linenumber);

        }

        public void StartFunction(string text, long time, object obj, string methodName, string guid,[CallerLineNumber] int linenumber = 0)
        {
            Console.WriteLine("sdcsdcd");
        }

        public void StartFunction(string text, long time, object obj, string methodName, [CallerLineNumber] int linenumber = 0)
        {
            
        }
    }
}
