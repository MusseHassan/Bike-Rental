using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_Rental
{
    public class Vehicle

    {
        // fordonets nummer
        public int Number { get; set; }
        //Pris per timme
        public decimal HourlyPrice { get; set; }
        // Tillgänglighet
        public bool IsAvailable { get; set; }
        public Vehicle(int number, decimal hourlyPrice, bool isAvailable)
        {
            Number = number;
            HourlyPrice = hourlyPrice;
            IsAvailable = isAvailable;
        }

    } 

}
