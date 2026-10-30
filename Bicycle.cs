using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Bike_Rental
{
    public class Bicycle : Vehicle
    {
        public Bicycle (int number) : base(number, 40m, true)
            { 
            }
            
        
    }
}
