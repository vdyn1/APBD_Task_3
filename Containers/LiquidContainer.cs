namespace Containers;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; private set; }

    public LiquidContainer(double maxPayload, double tareWeight, double height, double depth)
        : base("L", maxPayload, tareWeight, height, depth)
    {
        IsHazardous = false; 
    }

    public override void Loading(double mass, string productName, double? isHazardous = null)
    {
        if (mass <= 0)
            throw new ArgumentException("Error! Cargo mass must be positive.");

        if (ProductName == null)
        {
            ProductName = productName;
            IsHazardous = isHazardous == 1.0;

            if (IsHazardous)
            {
                NotifyHazard($" Hazardous liquid is being loaded into container {SerialNumber}.");
            }
        }
        else if (ProductName != productName)
        {
            NotifyHazard($"DANGER! Attempt to load {productName} into container {SerialNumber} which already contains {ProductName}.");
            throw new InvalidOperationException($"Error! Container {SerialNumber} already contains {ProductName}.");
        }

        double maxAllowedLoad = IsHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;

        if (mass + CargoMass > maxAllowedLoad)
        {
            NotifyHazard($" DANGER! Attempt to overfill container {SerialNumber} with hazardous liquid.");
            throw new OverfillException($"Error! Max allowed for this container: {maxAllowedLoad} kg.");
        }

        CargoMass += mass;
        Console.WriteLine($"{mass} kg of {productName} loaded into container {SerialNumber}. (Hazardous: {IsHazardous})");
    }



    public override void Emptying()
    {
        Console.WriteLine($"Container {SerialNumber} unloaded. Previous load: {CargoMass} kg -> 0 kg.");
        CargoMass = 0;
        ProductName = null;
        IsHazardous = false; 
    }

    // public override void CleanContainer()
    // {
    //     if (CargoMass > 0)
    //         throw new InvalidOperationException($"Error! Container {SerialNumber} is not empty.");
    //
    //     Console.WriteLine($"Container {SerialNumber} cleaned.");
    //     ProductName = null;
    //     IsHazardous = false;
    // }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"!!!  HAZARD !!!! {message}");
    }

    public override string ToString()
    {
        return $"{base.ToString()}, {(IsHazardous ? " !!! Hazardous !!!" : " Safe ")}";
    }
}

