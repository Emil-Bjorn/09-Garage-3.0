using Garage;

namespace Garage;

class Program
{
    static void Main(string[] args)
    {
        IUI ui = new UI();
        IGarageHandler garageHandler = new GarageHandler();
        IController controller = new Controller(ui, garageHandler);
        controller.Run();
    }
}
