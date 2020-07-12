using HackTruda.ViewModels.Messages;

namespace HackTruda.Definitions.VmLink
{
    /// <summary>
    /// VmLink для общения с <see cref="DialogViewModel"/>.
    /// </summary>
    public class DialogVmLink
    {
        public DialogVmLink(DialogItemViewModel dialogItem)
        {
            DialogItem = dialogItem;
        }

        public DialogItemViewModel DialogItem { get; }
    }
}
