using Microsoft.Toolkit.Uwp.Notifications;

namespace ToastNotificationsClassLibrary
{
    public interface IToastManager
    {
        public ToastContentBuilder toast { get; set; }
        void ConfigureToast(ToastScenario scenario, ToastDuration duration, string textInToast);
        void ShowToast();
    }
}