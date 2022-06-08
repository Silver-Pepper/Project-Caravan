using System;
using System.Collections.Generic;
using System.Linq;
using ProjectGeorge.Entities.Graph;

namespace ProjectGeorge.Entities
{
    class RoadData : Edge
    {
        public string FlavourText
        {
            get;
            private set;
        }

        public List<BezierRoute> Segments
        {
            get;
            private set;
        } = new List<BezierRoute>();

        public RoadData(CityData fromCity, CityData toCity, float weight, string flavour) : base(fromCity, toCity, weight)
        {
            FlavourText = flavour;
        }

        public RoadData(CityData fromCity, CityData toCity, float weight, string flavour, List<BezierRoute> segments) : base(fromCity, toCity, weight)
        {
            FlavourText = flavour;
            Segments = segments;
        }
    }
}
