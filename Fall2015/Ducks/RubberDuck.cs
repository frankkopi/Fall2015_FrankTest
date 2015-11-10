using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fall2015.Ducks
{
    public class RubberDuck : Duck
    {
        public RubberDuck(IFlyBehaviour flyBehaviour) : base(flyBehaviour)
        {
            
        }

        
        public override void Display()
        {
            Console.WriteLine("I am a rubber duck");
        }
    }
}