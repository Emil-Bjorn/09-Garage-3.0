using System;
using System.Text.RegularExpressions;

namespace Garage;

public class Vehicle : IVehicle
{
    public string RegistrationNumber { get; set; }

    public string Color { get; set; }
    public int NumberOfWheels { get; set; }

    public Vehicle(string registrationNumber, string color, int numberOfWheels)
    {
        if (!Regex.IsMatch(registrationNumber, @"\A[A-Za-z]{3}[0-9]{3}\z"))
        {
            throw new ArgumentException(
                "Registration number must be three letters followed by 3 digits (ABC123)."
            );
        }
        RegistrationNumber = registrationNumber;
        Color = color;
        NumberOfWheels = numberOfWheels;
    }

    public override string ToString()
    {
        return $"{RegistrationNumber} - {GetType().Name}, Color: {Color}, Number of wheels: {NumberOfWheels}";
    }
}
