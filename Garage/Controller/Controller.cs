using System;
using System.Reflection.Metadata;
using Garage;

namespace Garage;

public class Controller : IController
{
    private IUI _ui;
    private IGarageHandler _garageHandler;

    public Controller(IUI ui, IGarageHandler garageHandler)
    {
        _ui = ui;
        _garageHandler = garageHandler;
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
            _ui.DisplayInstructions();
            string cmd = _ui.Ask("Please input a command: ");
            switch (cmd)
            {
                case "0":
                {
                    running = false;
                    _ui.Show("Exiting application");
                    break;
                }
                case "1":
                {
                    int capacity = _ui.AskInt(
                        "Please define garage size: ",
                        "Garage size must be an integer."
                    );
                    try
                    {
                        _garageHandler.CreateGarage(capacity);
                    }
                    catch (ArgumentException ex)
                    {
                        _ui.Show(ex.Message);
                    }
                    break;
                }
                case "2":
                    {
                        AddVehicle(_garageHandler);
                    }
                    break;
                case "3":
                {
                    if (_garageHandler.Garage is not null)
                    {
                        foreach (Vehicle vehicle in _garageHandler.Garage)
                        {
                            _ui.Show(vehicle.ToString());
                        }
                    }
                    break;
                }
                case "4":
                {
                    RemoveVehicle(_garageHandler);
                    break;
                }
                case "5":
                {
                    SearchVehicle(_garageHandler);
                    break;
                }
                case "6":
                {
                    ListVehiclesByType(_garageHandler);
                    break;
                }
            }
        }
    }

    private void ListVehiclesByType(IGarageHandler garageHandler)
    {
        if (garageHandler.Garage is not null)
        {
            IEnumerable<IGrouping<string, IVehicle>> types = garageHandler.Garage.GroupBy(v =>
                v.GetType().Name
            );
            foreach (var type in types)
            {
                _ui.Show(type.Key);
                foreach (var vehicle in type)
                {
                    // Vehicle ToString method is not nullable so I supress the null warning here
                    _ui.Show(vehicle.ToString()!);
                }
            }
        }
    }

    private void AddVehicle(IGarageHandler garageHandler)
    {
        if (garageHandler.Garage is null)
        {
            _ui.Show("Error: Garage has not been initialized.");
        }
        else
        {
            _ui.Show("1. Car");
            _ui.Show("2. Motorcycle");
            _ui.Show("3. Bus");
            _ui.Show("4. Boat");
            _ui.Show("5. Airplane");
            _ui.Show("6. Other");
            string vehicleType = _ui.Ask("Please input vehicle type: ");
            string registrationNumber = _ui.Ask("Please input registration number: ");
            string color = _ui.Ask("Please input color: ");
            int numberOfWheels = _ui.AskInt(
                "Please input number of wheels: ",
                "Number of wheels must be an integer."
            );
            if (string.IsNullOrWhiteSpace(registrationNumber) || string.IsNullOrWhiteSpace(color))
            {
                _ui.Show("Error: registration number or color can not be empty.");
                return;
            }
            else
            {
                try
                {
                    switch (vehicleType)
                    {
                        case "1":
                            string fuelType = _ui.Ask("Please input fuel type: ");
                            if (!string.IsNullOrWhiteSpace(fuelType))
                            {
                                garageHandler.Garage.AddVehicle(
                                    new Car(registrationNumber, color, numberOfWheels, fuelType)
                                );
                            }
                            else
                            {
                                _ui.Show("Error: Fuel type can not be empty.");
                            }
                            break;
                        case "2":
                            string hasSidecar = _ui.Ask(
                                "Does the motorcycle have a sidecar attached?(y/n): "
                            );
                            switch (hasSidecar)
                            {
                                case "y":
                                    garageHandler.Garage.AddVehicle(
                                        new Motorcycle(
                                            registrationNumber,
                                            color,
                                            numberOfWheels,
                                            true
                                        )
                                    );
                                    break;
                                case "n":
                                    garageHandler.Garage.AddVehicle(
                                        new Motorcycle(
                                            registrationNumber,
                                            color,
                                            numberOfWheels,
                                            false
                                        )
                                    );
                                    break;
                                default:
                                    _ui.Show(
                                        "You must specify whether the motorcycle has a sidecar by writing [y] or [n]."
                                    );
                                    break;
                            }
                            break;
                        case "3":
                            int numberOfSeats = _ui.AskInt(
                                "Please input number of seats: ",
                                "Number of seats must be an integer."
                            );
                            garageHandler.Garage.AddVehicle(
                                new Bus(registrationNumber, color, numberOfWheels, numberOfSeats)
                            );
                            break;
                        case "4":
                            double length = _ui.AskDouble(
                                "Please input length in meters: ",
                                "Error: Please input a valid length."
                            );
                            garageHandler.Garage.AddVehicle(
                                new Boat(registrationNumber, color, numberOfWheels, length)
                            );
                            break;
                        case "5":
                            int numberOfEngines = _ui.AskInt(
                                "Please input number of engines: ",
                                "Error: Number of engines must be a whole integer."
                            );
                            garageHandler.Garage.AddVehicle(
                                new Airplane(
                                    registrationNumber,
                                    color,
                                    numberOfWheels,
                                    numberOfEngines
                                )
                            );
                            break;
                        case "6":
                            garageHandler.Garage.AddVehicle(
                                new Vehicle(registrationNumber, color, numberOfWheels)
                            );
                            break;
                        default:
                            _ui.Show(
                                "Error: We did not understand your command. Please select from the available options."
                            );
                            break;
                    }
                }
                catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
                {
                    _ui.Show(ex.Message);
                }
            }
        }
    }

    private void RemoveVehicle(IGarageHandler garageHandler)
    {
        string registrationNumber = _ui.Ask(
            "Please provide the registration number for the vehicle you would like to remove: "
        );
        if (!string.IsNullOrWhiteSpace(registrationNumber) && garageHandler.Garage is not null)
        {
            try
            {
                garageHandler.Garage.RemoveVehicle(registrationNumber);
                Console.WriteLine($"Vehicle '{registrationNumber}' succesfully removed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine(
                "Error: Please ensure garage is initialized and registration number are provided correctly."
            );
        }
    }

    private void SearchVehicle(IGarageHandler garageHandler)
    {
        _ui.Show("To search for a vehicle, input a property followed by a dash and your search.");
        _ui.Show("You can use multiple properties together by separating them with a space.");
        _ui.Show("Ex. registration-abc123 color-red wheels-2 type-motorcycle");
        string search = _ui.Ask("Please enter a search: ");
        bool matchFound = false;
        if (!string.IsNullOrWhiteSpace(search) && garageHandler.Garage != null)
        {
            try
            {
                foreach (Vehicle vehicle in garageHandler.Garage.Search(search))
                {
                    _ui.Show(vehicle.ToString());
                    matchFound = true;
                }
            }
            catch (ArgumentException ex)
            {
                _ui.Show(ex.Message);
            }
        }
        else
        {
            _ui.Show("Error: Ensure garage is initialized and search is not null");
        }
        if (matchFound == false)
        {
            _ui.Show("No matches found.");
        }
    }
}
