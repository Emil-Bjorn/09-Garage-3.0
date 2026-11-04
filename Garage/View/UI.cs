using System;

namespace Garage;

public class UI : IUI
{
    public void DisplayInstructions()
    {
        Console.WriteLine("0. Exit application");
        Console.WriteLine("1. Create new garage.");
        Console.WriteLine("2. Add new vehicle");
        Console.WriteLine("3. List all parked vehicles");
        Console.WriteLine("4. Remove vehicle");
        Console.WriteLine("5. Search for vehicle");
        Console.WriteLine("6. List vheicles by type");
    }

    public string Ask(string request)
    {
        Console.Write(request);
        // If Console.ReadLine is null we just return an empty string
        return Console.ReadLine() ?? string.Empty;
    }

    public int AskInt(string request, string error)
    {
        while (true)
        {
            if (int.TryParse(Ask(request), out int input))
            {
                return input;
            }
            Show(error);
        }
    }

    public double AskDouble(string request, string error)
    {
        while (true)
        {
            if (double.TryParse(Ask(request), out double input))
            {
                return input;
            }
            Show(error);
        }
    }

    public void Show(string message)
    {
        Console.WriteLine(message);
    }
}
