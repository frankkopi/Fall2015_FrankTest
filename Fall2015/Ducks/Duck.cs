using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fall2015.Ducks
{
    public abstract class Duck
    {
        public abstract void Display();
        private IFlyBehaviour _flyBehaviour;

        protected Duck(IFlyBehaviour flyBehaviour)
        {
            _flyBehaviour = flyBehaviour;
        }

        public void ChangeFlyBehaviour(IFlyBehaviour flyBehaviour)
        {
            _flyBehaviour = flyBehaviour;
        }


        public void Fly()
        {
            _flyBehaviour.Fly();
        }

        public void Swim()
        {
            Console.WriteLine("Yeah, I am swimming!");
        }
    }
}