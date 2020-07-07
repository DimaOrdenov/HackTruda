using System;
using System.Runtime.CompilerServices;
using System.Text;
using HackTruda.Services.Interfaces;

namespace HackTruda.Services
{
    public class DebuggerService : IDebuggerService
    {
        public void Log(Exception e, [CallerMemberName] object sender = null) =>
            WriteLine(e, sender);

        public void Log(string e, [CallerMemberName] object sender = null) =>
            WriteLine(e, sender);

        public void Log(object e, [CallerMemberName] object sender = null) =>
            WriteLine(e, sender);

        private void WriteLine(object e, object sender)
        {
            string output = e?.ToString();

            if (output?.Length > 4000)
            {
                output = output.Substring(0, 4000);
            }

            StringBuilder log = new StringBuilder();
            log.AppendLine();
            log.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm}]");
            log.AppendLine($"Sender: {sender?.ToString()}");
            log.AppendLine($"Argument type: {e?.GetType()} -->");
            log.AppendLine($"{output}");
            log.AppendLine();

#if DEBUG
            System.Diagnostics.Debug.
#else
            System.Diagnostics.Trace.
#endif
                WriteLine(log.ToString());
        }
    }
}
