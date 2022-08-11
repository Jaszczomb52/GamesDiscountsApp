﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIServicesClassLibrary.Models;

namespace FormsControlsClassLibrary
{
    public class WorthFilter : IAscDescFilter, IWorthFilter
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public WorthFilter()
        {
            Name = "Filter by worth";
            Description = "Filtering by the worth of item.";
        }

        public List<IGameGiveawayConvertedModel> FilterASC(List<IGameGiveawayConvertedModel> input)
        {
            input.OrderBy(x => x.worth);
            return input;
        }

        public List<IGameGiveawayConvertedModel> FilterDESC(List<IGameGiveawayConvertedModel> input)
        {
            input.OrderByDescending(x => x.worth);
            return input;
        }
    }
}
