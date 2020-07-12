using System;
using HackTruda.Definitions.Enums;

namespace HackTruda.Definitions.Exceptions
{
    /// <summary>
    /// Исключение BL.
    /// </summary>
    public class BusinessLogicException : TypedException
    {
        public BusinessLogicException()
        {
        }

        public BusinessLogicException(LogicExceptionType type)
            : this(type, null, null)
        {
        }

        public BusinessLogicException(LogicExceptionType type, Exception exception)
            : this(type, exception, null)
        {
        }

        public BusinessLogicException(LogicExceptionType type, string message)
            : this(type, null, message)
        {
        }

        public BusinessLogicException(LogicExceptionType type, Exception exception, string message)
            : base(type, exception, message) =>
            Type = type;

        public new LogicExceptionType Type { get; }
    }
}
