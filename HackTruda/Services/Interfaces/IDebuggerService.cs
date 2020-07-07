using System;
using System.Runtime.CompilerServices;

namespace HackTruda.Services.Interfaces
{
    public interface IDebuggerService
    {
        void Log(Exception e, [CallerMemberName] object sender = null);

        void Log(string e, [CallerMemberName] object sender = null);

        void Log(object e, [CallerMemberName] object sender = null);
    }
}
