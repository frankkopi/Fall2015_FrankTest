using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fall2015.TDD
{
    public class OurTimeSpan
    {
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }

        public Boolean Overlap(OurTimeSpan ourTimeSpan)
        {
            return FromTime >= ourTimeSpan.FromTime && FromTime < ourTimeSpan.ToTime
                   || ToTime <= ourTimeSpan.ToTime && ToTime > ourTimeSpan.FromTime
                   || FromTime < ourTimeSpan.FromTime && ToTime > ourTimeSpan.ToTime;
            ;

            //return true is ourTimeSpan overlaps the timespan
            //in this class.

        }
    }
}



