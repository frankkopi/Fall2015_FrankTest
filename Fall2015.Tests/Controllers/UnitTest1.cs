using System;
using Fall2015.Ducks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fall2015.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IFlyBehaviour flyBehaviour = new FlyBehaviour();

            MallardDuck mallardDuck = new MallardDuck(flyBehaviour);
            RedheadDuck redheadDuck = new RedheadDuck(flyBehaviour);

            IFlyBehaviour noFlyBehaviour = new NoFlyBehaviour();
            RubberDuck rubberDuck = new RubberDuck(noFlyBehaviour);


            mallardDuck.Display();
            mallardDuck.Fly();

            redheadDuck.Display();
            redheadDuck.Fly();

            rubberDuck.Display();
            rubberDuck.Fly();

            rubberDuck.ChangeFlyBehaviour(flyBehaviour);
            rubberDuck.Fly();

            redheadDuck.Swim();
            mallardDuck.Swim();
            rubberDuck.Swim();

        }
    }
}
