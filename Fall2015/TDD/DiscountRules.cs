using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fall2015.TDD
{
    public class DiscountRules
    {
        public static double CalculateDiscount(double price)
        {
            if (price < 0)
                throw new Exception();

            if (price <= 500)
                return 0;
            else if (price <= 1000 && price > 500)
                return 10;
            else if (price <= 5000 && price > 1000)
                return 15;
            else 
                return 20;
            

        }
    }
}






