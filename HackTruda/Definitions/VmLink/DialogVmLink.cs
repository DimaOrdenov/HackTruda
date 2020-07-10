using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HackTruda.Definitions.Models;
using HackTruda.ViewModels.Messages;

namespace HackTruda.Definitions.VmLink
{
    public class DialogVmLink
    {
        public DialogVmLink(DialogItemViewModel dialogItem)
        {
            DialogItem = dialogItem;
        }

        public DialogItemViewModel DialogItem { get; }
    }
}
