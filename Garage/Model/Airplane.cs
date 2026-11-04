using System;

namespace Garage;

public class Airplane : Vehicle
{
    public int NumberOfEngines { get; set; }

    public Airplane(string registrationNumber, string color, int numberOfSeats, int numberOfEngines)
        : base(registrationNumber, color, numberOfSeats)
    {
        NumberOfEngines = numberOfEngines;
    }
}
