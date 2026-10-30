
using System;
using System.Collections.Generic;
using Bike_Rental;

// Skapar cyklarna
Bicycle bicycle1 = new Bicycle(1);
Bicycle bicycle2 = new Bicycle(2);

ElectricBicycle electricBicycle1 = new ElectricBicycle(3, 80);
ElectricBicycle electricBicycle2 = new ElectricBicycle(4, 50);

// Lista med alla cyklar
List<Vehicle> vehicles = new List<Vehicle>();
vehicles.Add(bicycle1);
vehicles.Add(bicycle2);
vehicles.Add(electricBicycle1);
vehicles.Add(electricBicycle2);

// Listor för kunder och aktiva uthyrningar
List<Customer> customers = new List<Customer>();
List<Rental> rentals = new List<Rental>();

Customer customer = null;
int choice = 0;

// Menyn upprepas tills användaren väljer att avsluta
while (choice != 6)
{
    Console.WriteLine();
    Console.WriteLine("=== Kajens Cykeluthyrning ===");
    Console.WriteLine("1. Visa cyklar");
    Console.WriteLine("2. Registrera kund");
    Console.WriteLine("3. Hyr cykel");
    Console.WriteLine("4. Lämna tillbaka cykel");
    Console.WriteLine("5. Ladda elcykel");
    Console.WriteLine("6. Avsluta");

    Console.Write("Välj ett alternativ: ");
    choice = Convert.ToInt32(Console.ReadLine());

    // 1. Visar alla cyklar och deras status
    if (choice == 1)
    {
        Console.WriteLine();
        Console.WriteLine("=== Cyklar ===");

        foreach (Vehicle vehicle in vehicles)
        {
            Console.WriteLine("Cykelnummer: " + vehicle.Number);
            Console.WriteLine("Pris per timme: " + vehicle.HourlyPrice + " kr");
            Console.WriteLine("Ledig: " + vehicle.IsAvailable);

            // Visar batterinivån om fordonet är en elcykel
            if (vehicle is ElectricBicycle)
            {
                ElectricBicycle electricBike = (ElectricBicycle)vehicle;
                Console.WriteLine("Batteri: " + electricBike.BatteryLevel + "%");
            }

            Console.WriteLine("--------------------");
        }
    }

    // 2. Registrerar en kund
    if (choice == 2)
    {
        Console.WriteLine();
        Console.WriteLine("=== Registrera kund ===");

        Console.Write("Kundens namn: ");
        string name = Console.ReadLine();

        Console.Write("Kundens ålder: ");
        int age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Är kunden medlem? (ja/nej): ");
        string memberAnswer = Console.ReadLine();

        bool isMember = memberAnswer.ToLower() == "ja";

        customer = new Customer(name, age, isMember);
        customers.Add(customer);

        Console.WriteLine("Kunden är registrerad!");
    }

    // 3. Kontrollerar reglerna och hyr ut en cykel
    if (choice == 3)
    {
        if (customer == null)
        {
            Console.WriteLine("Registrera en kund först.");
        }
        else
        {
            Console.Write("Vilken cykel vill kunden hyra? (1-4): ");
            int bikeNumber = Convert.ToInt32(Console.ReadLine());

            Vehicle selectedVehicle = null;

            // Letar efter cykeln i listan
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.Number == bikeNumber)
                {
                    selectedVehicle = vehicle;
                }
            }

            if (selectedVehicle == null)
            {
                Console.WriteLine("Cykeln finns inte.");
            }
            else if (selectedVehicle.IsAvailable == false)
            {
                Console.WriteLine("Cykeln är redan uthyrd.");
            }
            // Vanlig cykel kräver minst 15 år
            else if (selectedVehicle is Bicycle && customer.Age < 15)
            {
                Console.WriteLine("Kunden måste vara minst 15 år.");
            }
            // Elcykel kräver minst 18 år
            else if (selectedVehicle is ElectricBicycle && customer.Age < 18)
            {
                Console.WriteLine("Kunden måste vara minst 18 år för att hyra elcykel.");
            }
            else
            {
                ElectricBicycle electricBike = null;

                if (selectedVehicle is ElectricBicycle)
                {
                    electricBike = (ElectricBicycle)selectedVehicle;

                    // Elcykeln måste ha minst 30 procent batteri
                    if (electricBike.BatteryLevel < 30)
                    {
                        Console.WriteLine("Elcykeln har för lite batteri.");
                    }
                }

                if (electricBike == null || electricBike.BatteryLevel >= 30)
                {
                    Console.Write("Hur många timmar vill kunden hyra? ");
                    int hours = Convert.ToInt32(Console.ReadLine());

                    if (hours <= 0)
                    {
                        Console.WriteLine("Du måste hyra minst 1 timme.");
                    }
                    else if (electricBike != null && electricBike.BatteryLevel < hours * 10)
                    {
                        Console.WriteLine("Batteriet räcker inte för så många timmar.");
                    }
                    else
                    {
                        selectedVehicle.IsAvailable = false;

                        // Elcykeln förbrukar 10 procent batteri per timme
                        if (electricBike != null)
                        {
                            electricBike.BatteryLevel =
                                electricBike.BatteryLevel - (hours * 10);
                        }

                        // Sparar kund, cykel och timmar i en uthyrning
                        Rental rental = new Rental(customer, selectedVehicle, hours);
                        rentals.Add(rental);

                        Console.WriteLine("Cykeln är nu uthyrd!");
                        Console.WriteLine("Kund: " + customer.Name);
                        Console.WriteLine("Cykelnummer: " + selectedVehicle.Number);
                        Console.WriteLine("Antal timmar: " + hours);
                    }
                }
            }
        }
    }

    // 4. Återlämnar cykeln och skriver ut kvitto
    if (choice == 4)
    {
        Console.Write("Vilken cykel lämnas tillbaka? (1-4): ");
        int returnBike = Convert.ToInt32(Console.ReadLine());

        Rental activeRental = null;

        // Hittar den aktiva uthyrningen
        foreach (Rental rental in rentals)
        {
            if (rental.Vehicle.Number == returnBike)
            {
                activeRental = rental;
            }
        }

        if (activeRental == null)
        {
            Console.WriteLine("Ingen aktiv uthyrning hittades för den cykeln.");
        }
        else
        {
            decimal totalPrice =
                activeRental.Vehicle.HourlyPrice * activeRental.Hours;

            decimal discount = 0;

            // Medlemmar får 10 procent rabatt
            if (activeRental.Customer.IsMember == true)
            {
                discount = totalPrice * 0.10m;
            }

            decimal finalPrice = totalPrice - discount;

            // Skriver ut kvittot
            Console.WriteLine();
            Console.WriteLine("========== KVITTO ==========");
            Console.WriteLine("Kund: " + activeRental.Customer.Name);
            Console.WriteLine("Cykelnummer: " + activeRental.Vehicle.Number);
            Console.WriteLine("Antal timmar: " + activeRental.Hours);
            Console.WriteLine("Pris per timme: " + activeRental.Vehicle.HourlyPrice + " kr");
            Console.WriteLine("Ordinarie pris: " + totalPrice + " kr");
            Console.WriteLine("Medlemsrabatt: " + discount + " kr");
            Console.WriteLine("Att betala: " + finalPrice + " kr");
            Console.WriteLine("============================");

            activeRental.Vehicle.IsAvailable = true;
            rentals.Remove(activeRental);

            Console.WriteLine("Cykeln är nu återlämnad.");
        }
    }

    // 5. Laddar en elcykel till 100 procent
    if (choice == 5)
    {
        Console.Write("Vilken elcykel vill du ladda? (3 eller 4): ");
        int chargeBike = Convert.ToInt32(Console.ReadLine());

        if (chargeBike == 3)
        {
            if (electricBicycle1.IsAvailable == true)
            {
                electricBicycle1.BatteryLevel = 100;
                Console.WriteLine("Elcykel 3 är nu fulladdad.");
            }
            else
            {
                Console.WriteLine("Elcykeln måste återlämnas innan den kan laddas.");
            }
        }
        else if (chargeBike == 4)
        {
            if (electricBicycle2.IsAvailable == true)
            {
                electricBicycle2.BatteryLevel = 100;
                Console.WriteLine("Elcykel 4 är nu fulladdad.");
            }
            else
            {
                Console.WriteLine("Elcykeln måste återlämnas innan den kan laddas.");
            }
        }
        else
        {
            Console.WriteLine("Välj elcykel 3 eller 4.");
        }
    }

    // 6. Avslutar programmet
    if (choice == 6)
    {
        Console.WriteLine("Avslutar programmet...");
    }
}




