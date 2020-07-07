using System;
namespace HackTruda.Definitions.Exceptions
{
    public abstract class TypedException : Exception
    {
        public TypedException(Enum type, Exception exception, string message = "")
            : base(message, exception)
        {
            Type = type;
        }

        public TypedException(Enum type, string message)
            : this(type, null, message)
        {
        }

        public TypedException(Enum type)
            : this(type, exception: null)
        {
        }

        public Enum Type { get; }
    }
}
