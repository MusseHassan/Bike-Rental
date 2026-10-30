using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_Rental
{
     public class Rental    
    {
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
        public int Hours { get; set; }

        public Rental(Customer customer, Vehicle vehicle, int hours)
        {
            Customer = customer;
            Vehicle = vehicle;
            Hours = hours;
        }
    }

}
