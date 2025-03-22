using System;
using System.Threading;

namespace Containers;
public abstract class Container
{
    private static int nextId = 1;

    public string SerialNumber { get; }

    public double CargoMass { get; set; }

    public double MaxPayload { get; }

    public double TareWeight { get; }

    public double Height { get; }

    public double Depth { get; }

    public string? ProductName { get; set; }

    public Container(string containerType, double maxPayload, double tareWeight, double height, double depth)
    {
        SerialNumber = $"KON-{containerType}-{nextId++}";
        MaxPayload = maxPayload;
        TareWeight = tareWeight;
        Height = height;
        Depth = depth;
        CargoMass = 0;
        ProductName = null;
    }

    public abstract void Emptying();

    
    // I had an idea that would allow us to clear it
    // with another fuel or gas but I m having trouble with GasContainer.
    
    // public abstract void CleanContainer();

    public abstract void Loading(double mass, string productName, double? value = null);

    public override string ToString()
    {
        string productInfo = ProductName == null
            ? "Status: Empty"
            : $"Cargo: {ProductName}, Mass: {CargoMass} kg / {MaxPayload} kg";

        return $"[Container: {SerialNumber}]\n" +
               $"{productInfo}\n" +
               $"Height: {Height} cm, Depth: {Depth} cm\n" +
               $"Tare Weight: {TareWeight} kg\n";
    }

}