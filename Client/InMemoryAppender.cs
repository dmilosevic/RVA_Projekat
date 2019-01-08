using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Client
{
    public class InMemoryAppender : MemoryAppender
    {
        public static readonly object dummyObj = new object();

        public static List<LogMessage> LogData { get; set; }

        static InMemoryAppender()
        {
            LogData = new List<LogMessage>();
            BindingOperations.EnableCollectionSynchronization(LogData, dummyObj);
        }


        protected override void Append(LoggingEvent loggingEvent)
        {
            LogData.Add(Convert(loggingEvent));
        }

        private LogMessage Convert(LoggingEvent loggingEvent)
        {
            return new LogMessage
            {
                Timestamp = loggingEvent.TimeStamp,
                Message = loggingEvent.RenderedMessage,
                Level = ConvertFrom(loggingEvent.Level)
            };

        }

        private string ConvertFrom(Level level)
        {
            if (level == Level.Error)
                return "ERROR";
            if (level == Level.Warn)
                return "WARN";
            if (level == Level.Info)
                return "INFO";

            return "UNKNOWN";
        }
    }


    public class LogMessage
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }

        public override string ToString()
        {
            return Timestamp.ToLongDateString() +" " + Level + " " +Message;
        }
    }
}
