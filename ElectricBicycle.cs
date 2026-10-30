using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_Rental
{
    public class ElectricBicycle : Vehicle
    {
        public int BatteryLevel { get; set; }

        public ElectricBicycle(int number, int batteryLevel)
               : base(number, 70m, true)
        {
            if (batteryLevel < 0 || batteryLevel > 100)
            {
                throw new ArgumentException("Batteriet måste vara mellan 0 och 100.");
            }

            BatteryLevel = batteryLevel;
        }




    }
    }            









