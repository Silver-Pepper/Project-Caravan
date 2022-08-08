using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectGeorge
{
    class Caravan
    {

        // Speed be float or double?
        // Can the speed of caravan be changed after set up?
        public double speed
        {
            get;
            set;
        }

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

        // Propose to use 1-100 scale and calculate out remain distance
        public double travelledTime
        {
            get;
            private set;
            // travelled time updated only by calling internal function
        }


        // False when update is invalid?
        // TODO design signature
        public bool updateTravelledTime(){
            this.travelledTime -= 1;
            return true;
        }


        public double totalDistance
        {
            get; // This should call a function with signature
            // double getDistance((City, City))
            private set;
        }

        public static double calculateDistance((City, City) cities)
        {

            return 100;
        }


        public List<Goods> inventory
        {
            get;
            set;
        }



        // util function to change settings
        public void updateCaravan()
        {

        }

        public Caravan(double speed, string name, (City, City) terminals, List<Goods> inventory) 
        {
            this.speed = speed;
            this.name = name;
            this.terminals = terminals;
            this.inventory = inventory;
            this.travelledTime = 0;
            this.totalDistance = calculateDistance(this.terminals);


            Console.WriteLine("Caravan set up succeed");
        }



    }
}