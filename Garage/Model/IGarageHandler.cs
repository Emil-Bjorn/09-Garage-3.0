using System;

namespace Garage;

public interface IGarageHandler
{
    public IGarage<IVehicle>? Garage { get; }
    public void CreateGarage(int capacity);
}
