using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Web;

namespace Fall2015.Ducks
{
    public class RedheadDuck : Duck
    {
        public RedheadDuck(IFlyBehaviour flyBehaviour) : base(flyBehaviour)
        {
            
        }

        public override void Display()
        {
            Console.WriteLine("I am a redhead duck, looking great by the way..");
        }
    }
}