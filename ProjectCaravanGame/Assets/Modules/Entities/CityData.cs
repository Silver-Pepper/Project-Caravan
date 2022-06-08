using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectGeorge.Entities.Graph;

namespace ProjectGeorge.Entities
{
    public enum Religion
    {
        CATHOLIC,
        ORTHODOX,
        PAGAN,
        ISLAM
    }

    public class CityData : Vertex
    {
        // In this class, all properties are in lower case

        /// <summary>
        /// Readonly name of the city
        /// </summary>
        public string name
        {
            get;
            private set;
        }

        public int population
        {
            get;
            private set;
        }

        public readonly string FlavourText;

        public Religion Religion
        {
            get;
            private set;
        }

        public University University;
        public List<Bank> Banks;
        public TownHall TownHall;

        public float PublicInfrastructure
        {
            get;
            private set;
        }

        public readonly float Order;

        public CityData(string underlyingID, string name, int population) : base(underlyingID)
        {
            this.name = name;
            this.population = population;
        }

        /// <summary>
        /// Compute the wealth level of the city automatically using its data members
        /// </summary>
        private float ComputeWealth()
        {
            throw new System.NotImplementedException();
        }
    }
}