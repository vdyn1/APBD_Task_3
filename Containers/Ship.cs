namespace Containers
{
    public class Ship
    {
        public string Name { get; }
        public double MaxSpeed { get; }           
        public int MaxContainers { get; }
        public double MaxWeight { get; }          
        private List<Container> Containers { get; }

        public Ship(string name, double maxSpeed, int maxContainers, double maxWeight)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            MaxContainers = maxContainers;
            MaxWeight = maxWeight;
            Containers = new List<Container>();
        }

        public void PrintContainers()
        {
            Console.WriteLine($"\n Containers on ship {Name}:");
            if (Containers.Count == 0)
            {
                Console.WriteLine(" No containers onboard.");
            }
            else
            {
                foreach (var container in Containers)
                {
                    Console.WriteLine($"    {container}");
                }
            }
        }

        public void LoadContainer(Container container)
        {
            if (Containers.Count >= MaxContainers)
            {
                throw new InvalidOperationException($"Ship {Name} is already at full container capacity ({MaxContainers}).");
            }

            double totalWeight = Containers.Sum(c => c.CargoMass + c.TareWeight);
            if (totalWeight + container.CargoMass + container.TareWeight > MaxWeight * 1000)
            {
                throw new InvalidOperationException($"Adding this container would exceed ship's max weight of {MaxWeight} tons.");
            }

            Containers.Add(container);
            Console.WriteLine($"Container {container.SerialNumber} loaded onto ship {Name}.");
        }

        public void RemoveContainer(Container container)
        {
            if (!Containers.Contains(container))
            {
                throw new InvalidOperationException($"Container {container.SerialNumber} not found on ship {Name}.");
            }

            Containers.Remove(container);
            Console.WriteLine($"Container {container.SerialNumber} removed from ship {Name}.");
        }

        public List<Container> LoadedContainers => new(Containers);

        public override string ToString()
        {
            double totalWeightKg = Containers.Sum(c => c.CargoMass + c.TareWeight);
            double totalWeightTons = totalWeightKg / 1000.0;

            return $"Ship {Name}: Speed {MaxSpeed} knots | " +
                   $"Containers {Containers.Count}/{MaxContainers} | " +
                   $"Load {totalWeightTons:F2}/{MaxWeight} tons";
        }
    }
}
