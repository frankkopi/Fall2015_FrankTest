using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fall2015.Ducks
{
    public class NoFlyBehaviour : IFlyBehaviour
    {
        public void Fly()
        {
            Console.WriteLine("*sad* I can't fly.. *sobs*");
        }
    }
}