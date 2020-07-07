using System;

namespace HackTruda.Definitions.PageStacks
{
    public abstract class BasePageStack
    {
        public BasePageStack(Type pageClassType, Type viewModelClassType)
        {
            PageClassType = pageClassType;
            ViewModelClassType = viewModelClassType;
        }

        public Type PageClassType { get; set; }

        public Type ViewModelClassType { get; set; }
    }
}
