using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlsLibrary
{
    public class DiscountsPanelFilter
    {
        public enum FilterTypes
        {
            Title,
            PublishDate,
            EndDate,
            Worth,
            AsIs
        }

        public static List<DiscountPanel> FilterPanels(List<DiscountPanel> panels, FilterTypes filter)
        {
            if (filter == FilterTypes.Title)
            {

            }
            return panels;
        }
    }
}
