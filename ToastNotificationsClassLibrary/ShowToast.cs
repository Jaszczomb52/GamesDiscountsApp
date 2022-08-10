using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToastNotificationsClassLibrary
{
    public class ShowToast
    {
        public static void CreateToast()
        {
            new ToastContentBuilder()
                .AddText("Andrew sent you a picture")
                .AddText("Check this out, The Enchantments in Washington!")
                .Show();
        }
    }
}
