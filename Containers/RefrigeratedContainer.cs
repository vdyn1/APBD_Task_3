namespace Containers;

public class RefrigeratedContainer : Container
{
    public double CurrentTemperature { get; set; }

    public RefrigeratedContainer(double currentTemperature, double maxPayload, double tareWeight, double height, double depth)
        : base("C", maxPayload, tareWeight, height, depth)
    {
        CurrentTemperature = currentTemperature;
    }

    public override void Loading(double mass, string productName, double? requiredTemperature)
    {
        if (mass <= 0)
            throw new ArgumentException("Cargo mass must be positive.");

        if (mass + CargoMass > MaxPayload)
            throw new OverfillException($"Container {SerialNumber} is overloaded. Max payload: {MaxPayload} kg.");

        if (ProductName == null)
        {
            ProductName = productName;
        }
        else if (ProductName != productName)
        {
            throw new InvalidOperationException($"Container {SerialNumber} already contains {ProductName}.");
        }

        if (requiredTemperature != null && CurrentTemperature < requiredTemperature)
        {
            throw new InvalidOperationException(
                $"Container temperature ({CurrentTemperature}°C) is below required ({requiredTemperature}°C) for {ProductName}.");
        }

        CargoMass += mass;
        Console.WriteLine($"{mass} kg of {productName} loaded into container {SerialNumber}. Required temp: {requiredTemperature}°C.");
    }

    public void ChangeTemperature(double newTemperature)
    {
        if (ProductName != null)
        {
            throw new InvalidOperationException(
                $"Temperature of container {SerialNumber} cannot be changed while it contains cargo ({ProductName}).");
        }

        CurrentTemperature = newTemperature;
        Console.WriteLine($"Temperature of container {SerialNumber} successfully changed to {newTemperature}°C.");
    }

    public override void Emptying()
    {
        Console.WriteLine($"Container {SerialNumber} unloaded. Was: {CargoMass} kg -> 0 kg.");
        CargoMass = 0;
        ProductName = null;
    }

    // public override void CleanContainer()
    // {
    //     if (CargoMass > 0)
    //     {
    //         throw new InvalidOperationException($"Container {SerialNumber} is not empty. Unload first.");
    //     }
    //
    //     Console.WriteLine($"Container {SerialNumber} cleaned. Ready for new cargo.");
    //     ProductName = null;
    // }
}
