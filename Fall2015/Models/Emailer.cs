using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fall2015.Models
{
    public interface IEmailer
    {
        void Send(String message);
    }

    public class Emailer : IEmailer
    {
        public void Send(string message)
        {
            //normally I would write code here that sends out emails.
            Console.WriteLine(message);
        }
    }
}







