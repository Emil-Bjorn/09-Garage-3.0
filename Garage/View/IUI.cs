using System;

namespace Garage;

public interface IUI
{
    public void DisplayInstructions();
    public string Ask(string request);
    public int AskInt(string request, string error);
    public double AskDouble(string request, string error);
    public void Show(string message);
}
