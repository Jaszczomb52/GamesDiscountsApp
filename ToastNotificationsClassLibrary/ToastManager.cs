using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToastNotificationsClassLibrary
{
    public class ToastManager : IToastManager
    {
        public ToastContentBuilder toast { get; set; }

        public ToastManager()
        {
            toast = new ToastContentBuilder();
        }

        public void ConfigureToast(ToastScenario scenario, ToastDuration duration, string textInToast)
        {
            toast.SetToastScenario(scenario);
            toast.SetToastDuration(duration);
            toast.AddText(textInToast);
        }

        public void ShowToast()
        {
            toast.Show();
        }
    }
}
