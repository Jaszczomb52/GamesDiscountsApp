using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsControlsClassLibrary
{
    public class FilterManager
    {
        List<IFilter> _filters;
        public FilterManager()
        {
            _filters = FilterFactory.GetFilters();
        }

        public List<IAscDescFilter> GetSorts()
        {
            List<IAscDescFilter> output = new List<IAscDescFilter>();
            foreach(IFilter filter in _filters)
            {
                if (filter is IAscDescFilter p)
                    output.Add(p);
            }
            return output;
        }

        public List<IChoiceFilter> GetFilters()
        {
            List<IChoiceFilter> output = new List<IChoiceFilter>();
            foreach (IFilter filter in _filters)
            {
                if (filter is IChoiceFilter p)
                    output.Add(p);
            }
            return output;
        }
    }
                              
}
