using System;
using System.Collections;

namespace Garage;

public class Garage<T> : IGarage<T>
    where T : class, IVehicle
{
    private List<T> _vehicles;

    public int Capacity { get; set; }
    public IEnumerable<T> Vehicles => _vehicles;

    public Garage(int capacity)
    {
        Capacity = capacity;
        _vehicles = new List<T>(capacity);
    }

    public void AddVehicle(T vehicle)
    {
        // Because Lists do not have a fixed size, the method must now check if Vehicles.Count and Capacity are equal to see if the garage is full.
        if (_vehicles.Count == Capacity)
        {
            throw new InvalidOperationException("Error: No free space in the garage.");
        }
        else if (GetVehicleByRegistrationNumber(vehicle.RegistrationNumber, out _) is not null)
        {
            throw new InvalidOperationException(
                "Error: Vehicle with that registration number already exists."
            );
        }
        _vehicles.Add(vehicle);
    }

    public T? GetVehicleByRegistrationNumber(string registrationNumber, out int index)
    {
        index = _vehicles.FindIndex(vehicle =>
            string.Equals(
                vehicle.RegistrationNumber,
                registrationNumber,
                StringComparison.OrdinalIgnoreCase
            )
        );
        return index < 0 ? null : _vehicles[index];
    }

    public void RemoveVehicle(string registrationNumber)
    {
        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            throw new ArgumentException("Error: Registration number must be filled in.");
        }
        if (GetVehicleByRegistrationNumber(registrationNumber, out int index) is null)
        {
            throw new InvalidOperationException(
                $"Error: Vehicle with registration number {registrationNumber} could not be found."
            );
        }
        _vehicles.RemoveAt(index);
    }

    // Helper function for search that validates that the search string is provided in the correct format of [key-value key-value]
    private (List<string>, List<string>) ValidateSearchString(string search)
    {
        List<string> searchList = [.. search.Split(' ', StringSplitOptions.RemoveEmptyEntries)];
        List<string> searchKeys = [];
        List<string> searchValues = [];

        foreach (string query in searchList)
        {
            // Search queries must include a dash that separates a search key and value
            int dashIndex = query.IndexOf('-');
            if (dashIndex <= 0 || dashIndex == query.Length - 1)
            {
                throw new ArgumentException(
                    $"Invalid search '{query}'. Please use key-value format."
                );
            }

            string searchKey = query.Substring(0, dashIndex);
            string searchValue = query.Substring(dashIndex + 1);

            switch (searchKey)
            {
                case "registration":
                case "color":
                case "type":
                    break;
                case "wheels":
                    if (!int.TryParse(searchValue, out _))
                    {
                        throw new ArgumentException(
                            "Error: Number of wheels must be a whole integer."
                        );
                    }
                    break;
                default:
                    throw new ArgumentException(
                        $"Error: Search key '{searchKey}' is not a valid key.'"
                    );
            }
            searchKeys.Add(searchKey);
            searchValues.Add(searchValue);
        }
        return (searchKeys, searchValues);
    }

    public IEnumerable<T> Search(string searchString)
    {
        List<T> matches = [];
        (List<string> validatedSearchKeys, List<string> validatedSearchValues) =
            ValidateSearchString(searchString);
        if (validatedSearchKeys.Count <= 0 && validatedSearchValues.Count <= 0)
        {
            throw new ArgumentException("Error: No valid search queries");
        }
        foreach (T vehicle in _vehicles)
        {
            var isMatch = true;
            for (int i = 0; i < validatedSearchKeys.Count && isMatch; i++)
            {
                switch (validatedSearchKeys[i])
                {
                    case "registration":
                        isMatch = string.Equals(
                            vehicle.RegistrationNumber,
                            validatedSearchValues[i],
                            StringComparison.OrdinalIgnoreCase
                        );
                        break;
                    case "color":
                        isMatch = string.Equals(
                            vehicle.Color,
                            validatedSearchValues[i],
                            StringComparison.OrdinalIgnoreCase
                        );
                        break;
                    case "type":
                        isMatch = string.Equals(
                            vehicle.GetType().Name,
                            validatedSearchValues[i],
                            StringComparison.OrdinalIgnoreCase
                        );
                        break;
                    case "wheels":
                        isMatch = string.Equals(
                            vehicle.NumberOfWheels.ToString(),
                            validatedSearchValues[i],
                            StringComparison.OrdinalIgnoreCase
                        );
                        break;
                }
            }
            if (isMatch)
            {
                matches.Add(vehicle);
            }
        }
        return matches;
    }

    // Required enumerator methods from implementing IEnumerable
    public IEnumerator<T> GetEnumerator()
    {
        return _vehicles.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
