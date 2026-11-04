using System;

namespace Garage;

public class Boat : Vehicle
{
    public double Length { get; set; }

    public Boat(string registrationNumber, string color, int numberOfWheels, double length)
        : base(registrationNumber, color, numberOfWheels)
    {
        Length = length;
    }
}
