using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToastNotificationsClassLibrary
{
    public class ToastFactory
    {
        public static IToastManager CreateToastManager()
        {
            return new ToastManager();
        }
    }
}
