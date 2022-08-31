using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using UnityEngine;

namespace ProjectGeorge
{
    class Caravan : MonoBehaviour
    {

        // Speed be float or double?
        // Can the speed of caravan be changed after set up?
        public float speed;

        // Is this refer to the ID of caravan?
        public string name
        {
            get;
            private set;
        }
        // Can we change destination half-way?
        // Maybe private setter
        public (City, City) terminals
        {
            get;
            set;
        }

        public City start_city;
        public City end_city;

        // Propose to use 1-100 scale and calculate out remain distance
        public double travelledTime
        {
            get;
            private set;
            // travelled time updated only by calling internal function
        }

        private float city_distance;

        public float distance_travelled
        {
            get;
            private set;
        }


        // False when update is invalid?
        // TODO design signature
        public bool UpdateTravelledTime(){
            this.travelledTime -= 1;
            return true;
        }


        public double totalDistance
        {
            get; // This should call a function with signature
            // double getDistance((City, City))
            private set;
        }

        public Vector3 CalculateMoveVector((City, City) cities)
        {

            return cities.Item2.transform.position - cities.Item1.transform.position;
        }

        public Vector3 CalculateCurrentPosition(City start_city, City end_city, float distance_travelled)
        {
            Vector3 travel_vector = end_city.transform.position - start_city.transform.position;
            float fraction_travalled = distance_travelled / city_distance;
            return start_city.transform.position + travel_vector * fraction_travalled;
        }

        public List<Goods> inventory
        {
            get;
            set;
        }



        // util function to change settings
        public void UpdateCaravan()
        {

        }

        public void Initialize(float speed, string name, (City, City) terminals, List<Goods> inventory) 
        {
            this.speed = speed;
            this.name = name;
            this.terminals = terminals;
            this.inventory = inventory;
            this.travelledTime = 0;
            this.totalDistance = CalculateDistance(this.terminals);

            this.city_distance = (end_city.transform.position - start_city.transform.position).magnitude;

            Console.WriteLine("Caravan set up succeed");
        }

        private double CalculateDistance((City, City) terminals)
        {
            return 100;
        }

        private void Start()
        {
            Initialize(0.1f, "Lorence", (null, null), new List<Goods>());
        }

        private void Update()
        {
            if (distance_travelled < city_distance)
            {
                distance_travelled += speed;
            }
            
            
            transform.position = CalculateCurrentPosition(start_city, end_city, distance_travelled);
        }

    }
}