using System;

namespace HackTruda.Definitions.PageStacks
{
    /// <summary>
    /// Абстрактный класс для связки страница-VM.
    /// </summary>
    public abstract class BasePageStack
    {
        public BasePageStack(Type pageClassType, Type viewModelClassType)
        {
            PageClassType = pageClassType;
            ViewModelClassType = viewModelClassType;
        }

        public Type PageClassType { get; }

        public Type ViewModelClassType { get; }
    }
}
