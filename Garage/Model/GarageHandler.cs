using System;
using Garage;

namespace Garage;

public class GarageHandler : IGarageHandler
{
    private IGarage<IVehicle>? _garage;
    public IGarage<IVehicle>? Garage
    {
        get { return _garage; }
    }

    public void CreateGarage(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentException("Capacity can not be below 0.");
        }
        _garage = new Garage<IVehicle>(capacity);
    }
}
