using ConsoleApp1;
using System;
using System.Collections.Generic;
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("RefrigeratedContainer");
        Product chicken = new Product("Chicken", -10.1);
        RefrigeratedContainer chickenContainer = new RefrigeratedContainer(
            cargoMass: 0,
            containerHeight: 250,
            tareWeight: 800,
            containerDepth: 60,
            type: 'F',
            maxPayload: 1000,
            productType: "Chicken",
            temperature: -10.1);
        try
        {
            chickenContainer.LoadContainer(chicken, 1800);
            Console.WriteLine($"Refrigerated container {chickenContainer.SerialNumber} was loaded with chicken.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.WriteLine("\nLiquidContainer");
        LiquidContainer liquidContainerSafe = new LiquidContainer(
            cargoMass: 0,
            containerHeight: 200,
            tareWeight: 1500,
            containerDepth: 500,
            type: 'L',
            maxPayload: 8000,
            isHazardous: false);
        LiquidContainer liquidContainerHazard = new LiquidContainer(
            cargoMass: 0,
            containerHeight: 200,
            tareWeight: 1500,
            containerDepth: 500,
            type: 'L',
            maxPayload: 8000,
            isHazardous: true);
        try
        {
            liquidContainerSafe.LoadContainer(7000);
            Console.WriteLine($"Safe liquid container {liquidContainerSafe.SerialNumber} was loaded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        try
        {
            liquidContainerHazard.LoadContainer(4500);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hazarded cargo in container {liquidContainerHazard.SerialNumber}: " + ex.Message);
        }

        Console.WriteLine("\nGasContainer");
        GasContainer gasContainer = new GasContainer(
            cargoMass: 5000,
            containerHeight: 180,
            tareWeight: 1400,
            containerDepth: 450,
            type: 'G',
            maxPayload: 9000,
            pressure: 5.5);
        try
        {
            gasContainer.LoadContainer(8000);
            Console.WriteLine($"Container {gasContainer.SerialNumber} was loaded with gas.");
            gasContainer.EmptyGas();
            Console.WriteLine($"Container {gasContainer.SerialNumber} has current gas mass {gasContainer.CargoMass}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.WriteLine("\nShips.");
        Ship ship = new Ship(maxContainers: 5, maxSpeed: 25, maxContainersMass: 50);
        try
        {
            ship.AddContainer(chickenContainer);
            ship.AddContainer(liquidContainerSafe);
            ship.AddContainer(gasContainer);
            Console.WriteLine("Added containers");
            ship.PrintShipInfo();
            ship.RemoveContainer(liquidContainerSafe.SerialNumber);
            Console.WriteLine("\nDeleted 1 container:");
            ship.PrintShipInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.WriteLine("\nContainer swap");
        Ship shipA = new Ship(maxContainers: 3, maxSpeed: 20, maxContainersMass: 30);
        Ship shipB = new Ship(maxContainers: 4, maxSpeed: 30, maxContainersMass: 60);
        try
        {
            shipA.AddContainer(liquidContainerSafe);
            Console.WriteLine($"Container {liquidContainerSafe.SerialNumber} was added to ship A.");
            shipA.RemoveContainer(liquidContainerSafe.SerialNumber);
            shipB.AddContainer(liquidContainerSafe);
            Console.WriteLine($"Container {liquidContainerSafe.SerialNumber} was added to ship B.");
            Console.WriteLine("\nShip A:");
            shipA.PrintShipInfo();
            Console.WriteLine("\nShip B:");
            shipB.PrintShipInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.WriteLine("\nPress any key to finish");
        Console.ReadKey();
    }
}
