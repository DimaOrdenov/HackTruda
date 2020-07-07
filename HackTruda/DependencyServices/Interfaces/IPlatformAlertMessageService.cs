using System;
namespace HackTruda.DependencyServices.Interfaces
{
    public interface IPlatformAlertMessageService
    {
        void LongAlert(string message);

        void ShortAlert(string message);
    }
}
