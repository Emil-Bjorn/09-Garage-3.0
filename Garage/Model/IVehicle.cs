using System;

namespace Garage;

public interface IVehicle
{
    public string RegistrationNumber { get; }
    public string Color { get; set; }
    public int NumberOfWheels { get; set; }
}
