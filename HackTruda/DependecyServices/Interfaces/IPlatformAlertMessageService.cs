using System;
namespace HackTruda.DependecyServices.Interfaces
{
    public interface IPlatformAlertMessageService
    {
        void LongAlert(string message);

        void ShortAlert(string message);
    }
}
