using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsControlsClassLibrary
{
    public class FilterFactory
    {
        public static List<IFilter> GetFilters()
        {
            // create empty IFilter list to fill it up with all filters
            List<IFilter> list = new()
            {
                // adding all filters implementing IFilter into the list
                GetWorthFilter(),
                GetTitleFilter(),
                GetPublishedDateFilter(),
                GetEndDateFilter(),
                GetDeviceFilter(),
                GetTypeFilter()
            };

            return list;
        }

        public static IFilter GetWorthFilter()
        {
            return new WorthFilter();
        }

        public static IFilter GetTitleFilter()
        {
            return new TitleFilter();
        }

        public static IFilter GetPublishedDateFilter()
        {
            return new PublishedDateFilter();
        }

        public static IFilter GetEndDateFilter()
        {
            return new EndDateFilter();
        }

        public static IFilter GetDeviceFilter()
        {
            return new DeviceFilter();
        }

        public static IFilter GetTypeFilter()
        {
            return new TypeFilter();
        }
    }
}
