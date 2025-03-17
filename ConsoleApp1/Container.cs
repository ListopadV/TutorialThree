namespace ConsoleApp1
{
public class Container
{
    public double CargoMass { get; set; }
    public double ContainerHeight { get; set; }
    public double TareWeight { get; set; }
    public double ContainerDepth { get; set; }
    public string SerialNumber { get; private set; }
    public double MaxPayload { get; set; }

    public Container(double cargoMass, double containerHeight, double tareWeight, double containerDepth, char type, int number, double maxPayload)
    {
        SerialNumber = $"KON-{type}-{number}";
        CargoMass = cargoMass;
        ContainerHeight = containerHeight;
        TareWeight = tareWeight;
        ContainerDepth = containerDepth;
        MaxPayload = maxPayload;
    }

    public void EmptyCargo() => CargoMass = 0;
    
    public void LoadContainer(double cargoMass)
    {
        if (cargoMass > MaxPayload)
            throw new Exception("OverfillException");
        CargoMass = cargoMass;
    }

    public void Notify(string message) => Console.WriteLine(message);
}

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; set; }

    public LiquidContainer(double cargoMass, double containerHeight, double tareWeight, double containerDepth, char type, int number, double maxPayload, bool isHazardous)
        : base(cargoMass, containerHeight, tareWeight, containerDepth, type, number, maxPayload)
    {
        IsHazardous = isHazardous;
        if (IsHazardous && cargoMass > MaxPayload / 2)
            Notify("Hazardous mass exceeds 50% of container capacity");
    }

    public void Notify(string message) => Console.WriteLine(message);
}

public interface IHazardNotifier
{
    void Notify(string message);
}

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; set; }

    public GasContainer(double cargoMass, double containerHeight, double tareWeight, double containerDepth, char type, int number, double maxPayload, double pressure)
        : base(cargoMass, containerHeight, tareWeight, containerDepth, type, number, maxPayload)
    {
        Pressure = pressure;
    }

    public void EmptyGas(double newWeight)
    {
        if (newWeight > 0.95)
            Notify("5% of the gas should be left");
        else if (newWeight > 1)
            Notify("Too much gas");
        CargoMass += newWeight;
    }

    public void Notify(string message) => Console.WriteLine(message + SerialNumber);
}

public class RefrigeratedContainer : Container, IHazardNotifier
{
    public string ProductType { get; set; }
    public double Temperature { get; set; }

    public RefrigeratedContainer(double cargoMass, double containerHeight, double tareWeight, double containerDepth, char type, int number, double maxPayload, string productType, double temperature)
        : base(cargoMass, containerHeight, tareWeight, containerDepth, type, number, maxPayload)
    {
        ProductType = productType;
        Temperature = temperature;
    }

    public void Notify(string message) => Console.WriteLine(message);
}

public class Product
{
    public string Name { get; set; }
    public double Temperature { get; set; }

    public Product(string name, double temperature)
    {
        Name = name;
        Temperature = temperature;
    }
}

public class Gas
{
    public double Pressure { get; set; }

    public Gas(double pressure)
    {
        Pressure = pressure;
    }
}

public class Ship
{
    public List<Container> Containers { get; set; }
    public int MaxContainers { get; set; }
    public double MaxContainersMass { get; set; }
    public double MaxSpeed { get; set; }

    public Ship(List<Container> containers, int maxContainers, double maxSpeed, double maxContainersMass)
    {
        Containers = containers;
        MaxContainers = maxContainers;
        MaxSpeed = maxSpeed;
        MaxContainersMass = maxContainersMass;
    }
}

}
