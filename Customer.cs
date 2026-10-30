using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_Rental
{
    public class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsMember { get; set; }

        public Customer(string name, int age, bool isMember)
        {
            Name = name;
            Age = age;
            IsMember = isMember;
            

            
        }
        
    }

} 
