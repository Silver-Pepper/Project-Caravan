using System;

namespace ProjectGeorge
{
    class Goods
    {
        public string Name
        {
            get;
            private set;
        }


        public Goods(string name) 
        {
            Name = name;
            Console.WriteLine("The name of this item is " + Name);
        }


    }
}