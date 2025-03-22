
namespace Containers
{ public class GasContainer : Container, IHazardNotifier
    {
        public string? GasType { get; private set; }

        public double Pressure { get; private set; }

        public GasContainer(double maxPayload, double tareWeight, double height, double depth)
            : base("G", maxPayload, tareWeight, height, depth)
        {
            GasType = null;
            Pressure = 0;
        }

        public override void Loading(double mass, string productName, double? pressure = null)
        {
            if (mass <= 0)
                throw new ArgumentException("Cargo mass must be positive.");

            if (mass + CargoMass > MaxPayload)
                throw new OverfillException($"Container {SerialNumber} is overloaded. Max payload: {MaxPayload} kg.");

            
            if (CargoMass == 0)
            {
                ProductName = productName;
                Pressure = pressure ?? throw new ArgumentException("Pressure must be provided for gas containers.");
            }
            else
            {
                if (productName != ProductName)
                {
                    throw new InvalidOperationException($"Container {SerialNumber} already contains {ProductName}.");
                }

                if (pressure != null && pressure != Pressure)
                {
                    throw new InvalidOperationException($"Container {SerialNumber} already has pressure set to {Pressure} atm.");
                }
            }

            NotifyHazard($" Hazardous gas is being loaded into container {SerialNumber}.");

            CargoMass += mass;
            Console.WriteLine($" {mass} kg of {ProductName} added to container {SerialNumber} at {Pressure} atm.");
        }



        public override void Emptying()
        {
            double remainingMass = CargoMass * 0.05;
            Console.WriteLine($"Container {SerialNumber} emptied. 5% ({remainingMass} kg) remains.");
            CargoMass = remainingMass;
        }

        // public override void CleanContainer()
        // {
        //     Console.WriteLine($"Container {SerialNumber} cleaned.");
        //     ProductName = null;
        //     GasType = null;
        //     Pressure = 0;
        // }

        public void NotifyHazard(string message)
        {
            Console.WriteLine($"!!!  HAZARD !!!! {message}");        }
        

    }
}
