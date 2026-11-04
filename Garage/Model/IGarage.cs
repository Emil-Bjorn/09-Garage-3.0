using System;

namespace Garage;

public interface IGarage<T> : IEnumerable<T>
    where T : IVehicle
{
    public IEnumerable<T> Vehicles { get; }
    public int Capacity { get; set; }

    public void AddVehicle(T vehicle);
    public void RemoveVehicle(string registrationNumber);
    public T? GetVehicleByRegistrationNumber(string registrationNumber, out int index);
    public IEnumerable<T> Search(string searchString);
}
