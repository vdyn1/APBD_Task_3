
using Containers;

namespace ContainersApp
{
    public class ContainerManager
    {
        public List<Container> Containers { get; } = new List<Container>();

        public void CreateContainer()
        {
            Console.WriteLine("Select container type:");
            Console.WriteLine("1. Liquid");
            Console.WriteLine("2. Gas");
            Console.WriteLine("3. Refrigerated");
            string? typeChoice = Console.ReadLine();

            Console.Write("Enter max payload (kg): ");
            double maxPayload = double.Parse(Console.ReadLine()!);
            Console.Write("Enter tare weight (kg): ");
            double tareWeight = double.Parse(Console.ReadLine()!);
            Console.Write("Enter height (cm): ");
            double height = double.Parse(Console.ReadLine()!);
            Console.Write("Enter depth (cm): ");
            double depth = double.Parse(Console.ReadLine()!);

            Container? container = typeChoice switch
            {
                "1" => new LiquidContainer(maxPayload, tareWeight, height, depth),
                "2" => new GasContainer(maxPayload, tareWeight, height, depth),
                "3" => CreateRefrigeratedContainer(maxPayload, tareWeight, height, depth),
                _   => null
            };

            if (container == null)
            {
                Console.WriteLine("Invalid container type.");
                return;
            }

            Containers.Add(container);
            Console.WriteLine($"Container created: {container.SerialNumber}");
        }
        
        

        private Container CreateRefrigeratedContainer(double maxPayload, double tareWeight, double height, double depth)
        {
            Console.Write("Enter current temperature (°C): ");
            double currentTemp = double.Parse(Console.ReadLine()!);
            return new RefrigeratedContainer(currentTemp, maxPayload, tareWeight, height, depth);
        }
        
        

        public void LoadCargoIntoContainer()
        {
            if (Containers.Count == 0)
            {
                Console.WriteLine("No containers available.");
                return;
            }

            PrintContainers();
            Console.Write("Select container number: ");
            int index = int.Parse(Console.ReadLine()!) - 1;
            if (index < 0 || index >= Containers.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Container container = Containers[index];
            Console.Write("Enter cargo mass (kg): ");
            double mass = double.Parse(Console.ReadLine()!);
            Console.Write("Enter cargo name: ");
            string? productName = Console.ReadLine();

            try
            {
                if (container is GasContainer)
                {
                    Console.Write("Enter pressure (atm): ");
                    double pressure = double.Parse(Console.ReadLine()!);
                    container.Loading(mass, productName!, pressure);
                }
                else if (container is RefrigeratedContainer)
                {
                    Console.Write("Enter required temperature (°C): ");
                    double reqTemp = double.Parse(Console.ReadLine()!);
                    container.Loading(mass, productName!, reqTemp);
                }
                else if (container is LiquidContainer)
                {
                    Console.Write("Is cargo hazardous? (1 - yes, 0 - no): ");
                    double isHazardous = double.Parse(Console.ReadLine()!);
                    container.Loading(mass, productName!, isHazardous);
                }
                else
                {
                    container.Loading(mass, productName!);
                }
            }
            catch (OverfillException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        

        public void UnloadContainer()
        {
            if (Containers.Count == 0)
            {
                Console.WriteLine("No containers available.");
                return;
            }

            PrintContainers();
            Console.Write("Select container number to unload: ");
            int index = int.Parse(Console.ReadLine()!) - 1;
            if (index < 0 || index >= Containers.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Containers[index].Emptying();
        }
        
        
        

        // public void CleanContainer()
        // {
        //     if (Containers.Count == 0)
        //     {
        //         Console.WriteLine("No containers available.");
        //         return;
        //     }
        //
        //     PrintContainers();
        //     Console.Write("Select container number to clean: ");
        //     int index = int.Parse(Console.ReadLine()!) - 1;
        //     if (index < 0 || index >= Containers.Count)
        //     {
        //         Console.WriteLine("Invalid selection.");
        //         return;
        //     }
        //
        //     try
        //     {
        //         Containers[index].CleanContainer();
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //     }
        // }
        
        
        public void DeleteContainer()   
        {
            if (Containers.Count == 0)
            {
                Console.WriteLine("No containers to delete.");
                return;
            }

            PrintContainers();
            Console.Write("Select container number to delete: ");
            int index = int.Parse(Console.ReadLine()!) - 1;
            if (index < 0 || index >= Containers.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            var container = Containers[index];
            Containers.RemoveAt(index);
            Console.WriteLine($"Container {container.SerialNumber} deleted.");
        }
        
        

        public void PrintContainers()
        {
            Console.WriteLine("List of containers:");
            for (int i = 0; i < Containers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Containers[i]}");
            }
        }
        
        

        public void PrintContainerInfo()
        {
            if (Containers.Count == 0)
            {
                Console.WriteLine("No containers available.");
                return;
            }

            PrintContainers();
            Console.Write("Select container number: ");
            int index = int.Parse(Console.ReadLine()!) - 1;
            if (index < 0 || index >= Containers.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Console.WriteLine(Containers[index]);
        }
    }
}
