using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fall2015.Ducks
{
    public class FlyBehaviour : IFlyBehaviour
    {
        public void Fly()
        {
            Console.WriteLine("Uhuhuhu, I am flying. It's great!");
        }
    }
}