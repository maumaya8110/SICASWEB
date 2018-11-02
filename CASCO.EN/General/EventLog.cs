using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CASCO.EN.General
{
    public static class EventLog
    {
        public static System.Diagnostics.EventLog eventLog1;

        public static void Init_EventLog()
        {
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(Utils.EventSource))
            {
                System.Diagnostics.EventLog.CreateEventSource(Utils.EventSource, Utils.EventLog);
            }
            eventLog1.Source = Utils.EventSource;
            eventLog1.Log = Utils.EventLog;
        }

        public static void WriteInfo(string msg)
        {
            eventLog1.WriteEntry(msg, EventLogEntryType.Information);
        }

        public static void WriteError(string msg)
        {
            eventLog1.WriteEntry(msg, EventLogEntryType.Error);
        }
    }
}
