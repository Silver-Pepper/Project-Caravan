using System;
using System.Text;

namespace ProjectGeorge
{
    class Buildings 
    {
        private static string PROMPT_NAME = "The name of this building is: ";
        private static string PROMPT_DES = "\n ";
        // Name of the building
        // The name stands for types of building of actual name?
        // can be Enumerate date type?
        public string Name
        {
            get;
            private set;
        }
        
        public string Descriptions
        {
            get;
            private set;
        }

        public override string ToString()
        {
            int out_str_len = Name.Length + Descriptions.Length + PROMPT_NAME.Length + PROMPT_DES.Length + 10;
            StringBuilder strbuilder = new StringBuilder(PROMPT_NAME, out_str_len);
            strbuilder.Append(Name);
            strbuilder.Append(PROMPT_DES);
            strbuilder.Append(Descriptions);
            return strbuilder.ToString();
        }

        public Buildings(string name, string descriptions) 
        {
            Name = name;
            Descriptions = descriptions;
        }

        public Buildings(string name) 
        {
            Name = name;
            Descriptions = "Empty descriptions";
        }

    }
}