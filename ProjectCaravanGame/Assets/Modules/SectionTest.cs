using System;

namespace ProjectGeorge
{
    class SectionTests
    {
        public static bool buildingsConstructor(){
            Buildings shop1 = new Buildings("George's", "Best IR researcher");
            Console.WriteLine(shop1);

            Buildings shop2 = new Buildings("George'2s");
            Console.WriteLine(shop2);

            return true;
        }


    }
}