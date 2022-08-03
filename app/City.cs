using System;
using System.Text;

namespace ProjectGeorge
{
    class City
    {
        public bool isVisited
        {
            get;
            private set;
        }

        public bool hasCaravan
        {
            get;
            set;
        }

        public string name
        {
            get;
            private set;
        }
        
        public string descriptions
        {
            get;
            private set;
        }

        public string flavourTest
        {
            get;
            private set;
        }

        public List<Buildings> buildings
        {
            get;
            private set;
        }


        public City(string name, string descriptions, List<Buildings> buildings) 
        {
            this.name = name;
            this.descriptions = descriptions;
            this.flavourTest = "default values";
            this.buildings = buildings;
            this.hasCaravan = false;
            this.isVisited = false;
        }

    }
}