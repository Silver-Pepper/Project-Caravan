using ProjectGeorge.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ProjectGeorge
{
    public class City : MonoBehaviour
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

        public string city_name
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

        public CityData Data
        {
            get;
            private set;
        }

        [SerializeField]
        public Sprite cityPic;

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