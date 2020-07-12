using System;
namespace HackTruda.Definitions.Exceptions
{
    /// <summary>
    /// Абстрактный класс исключения с возможностью привязаться к перечислению.
    /// </summary>
    public abstract class TypedException : Exception
    {
        protected TypedException(Enum type, Exception exception, string message = "")
            : base(message, exception)
        {
            Type = type;
        }

        protected TypedException(Enum type, string message)
            : this(type, null, message)
        {
        }

        protected TypedException(Enum type)
            : this(type, exception: null)
        {
        }

        protected TypedException()
        {
        }

        public Enum Type { get; }
    }
}
