using System;
namespace HackTruda.DependencyServices.Interfaces
{
    /// <summary>
    /// Платформозависимый сервис для алертов.
    /// </summary>
    public interface IPlatformAlertMessageService
    {
        void LongAlert(string message);

        void ShortAlert(string message);
    }
}
