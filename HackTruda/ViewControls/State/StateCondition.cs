using System;
using HackTruda.Definitions.Enums;
using Xamarin.Forms;

namespace HackTruda.ViewControls.State
{
    [ContentProperty(nameof(Content))]
    public class StateCondition : View
    {
        public PageStateType PageState { get; set; }

        public View Content { get; set; }
    }
}
