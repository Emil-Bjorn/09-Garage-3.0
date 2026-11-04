using Garage;
using Xunit.Sdk;

namespace Test;

public class UnitTest1
{
    [Fact]
    public void Search_Color_ReturnsExpectedVehicles()
    {
        // Arrange
        var garage = new Garage<Vehicle>(5);
        garage.AddVehicle(new Vehicle("ABC123", "Red", 4));
        garage.AddVehicle(new Car("AAA111", "Blue", 4, "Diesel"));
        garage.AddVehicle(new Motorcycle("BBB222", "Red", 2, false));
        IEnumerable<string> expected = ["ABC123", "BBB222"];
        // Act
        var actual = garage.Search("color-red").Select(v => v.RegistrationNumber);
        // Assert
        Assert.Equal(expected, actual);
    }
}
