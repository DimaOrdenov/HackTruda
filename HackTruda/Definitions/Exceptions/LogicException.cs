using System;
using HackTruda.Definitions.Enums;

namespace HackTruda.Definitions.Exceptions
{
    public class LogicException : TypedException
    {
        public LogicException()
        {
        }

        public LogicException(LogicExceptionType type)
            : this(type, null, null)
        {
        }

        public LogicException(LogicExceptionType type, Exception exception)
            : this(type, exception, null)
        {
        }

        public LogicException(LogicExceptionType type, string message)
            : this(type, null, message)
        {
        }

        public LogicException(LogicExceptionType type, Exception exception, string message)
            : base(type, exception, message) =>
            Type = type;

        public new LogicExceptionType Type { get; }
    }
}
