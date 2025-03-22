
using Containers;

namespace ContainersApp
{
    public class ShipManager
    {
        public List<Ship> Ships { get; } = new();

        public void CreateShip()
        {
            Console.Write("Enter ship name: ");
            string? name = Console.ReadLine();
            Console.Write("Enter max speed (knots): ");
            double maxSpeed = double.Parse(Console.ReadLine()!);
            Console.Write("Enter max number of containers: ");
            int maxContainers = int.Parse(Console.ReadLine()!);
            Console.Write("Enter max total weight (tons): ");
            double maxWeight = double.Parse(Console.ReadLine()!);

            Ship ship = new(name!, maxSpeed, maxContainers, maxWeight);
            Ships.Add(ship);
            Console.WriteLine($"Ship \"{ship.Name}\" created successfully.");
        }

        
        
        public void LoadContainerOntoShip(Container container)
        {
            if (Ships.Count == 0)
            {
                Console.WriteLine("No available ships.");
                return;
            }

            PrintShips();
            Console.Write("Select ship number: ");
            int index = int.Parse(Console.ReadLine()!) - 1;
            
            try
            {
                Ships[index].LoadContainer(container);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        

        public void LoadMultipleContainersOntoShip(List<Container> containers)
        {
            if (Ships.Count == 0)
            {
                Console.WriteLine("No available ships.");
                return;
            }

            PrintShips();
            Console.Write("Select ship number: ");
            int shipIndex = int.Parse(Console.ReadLine()!) - 1;
            
            PrintContainerList(containers);
            Console.Write("Enter container numbers to load (comma-separated): ");
            string? input = Console.ReadLine();

            var indices = input?.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (indices == null) return;

            foreach (var idx in indices)
            {
                if (int.TryParse(idx.Trim(), out int containerIndex))
                {
                    containerIndex -= 1;
                    if (IsValidIndex(containerIndex, containers.Count))
                    {
                        try
                        {
                            Ships[shipIndex].LoadContainer(containers[containerIndex]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid container number: {idx}");
                    }
                }
            }
        }

        
        public void RemoveContainer()
        {
            if (Ships.Count == 0)
            {
                Console.WriteLine(" No available ships.");
                return;
            }

            PrintShips();
            Console.Write("Select ship number: ");
            int shipIndex = int.Parse(Console.ReadLine()!) - 1;

            if (!IsValidIndex(shipIndex, Ships.Count))
            {
                Console.WriteLine(" Invalid ship selection.");
                return;
            }

            Ship ship = Ships[shipIndex];

            Console.WriteLine($"\n Containers on ship \"{ship.Name}\":");
            if (ship.LoadedContainers.Count == 0)
            {
                Console.WriteLine("   - No containers onboard.");
                return;
            }

            foreach (var c in ship.LoadedContainers)
            {
                Console.WriteLine($"   {c.SerialNumber} | {c.ProductName ?? "Empty"} | {c.CargoMass} kg");
            }

            Console.Write("\nEnter container serial number to remove: ");
            string? serial = Console.ReadLine();

            Container? container = ship.LoadedContainers.FirstOrDefault(c => c.SerialNumber == serial);
            if (container == null)
            {
                Console.WriteLine("Container not found on ship.");
                return;
            }

            try
            {
                ship.RemoveContainer(container);
            }
            catch (Exception ex)
            {
                Console.WriteLine($" {ex.Message}");
            }
        }
        
        


        public void ReplaceContainer(Container newContainer)
        {
            if (Ships.Count == 0)
            {
                Console.WriteLine("No available ships.");
                return;
            }

            PrintShips();
            Console.Write("Select ship number: ");
            int shipIndex = int.Parse(Console.ReadLine()!) - 1;

            if (!IsValidIndex(shipIndex, Ships.Count))
            {
                Console.WriteLine("Invalid ship selection.");
                return;
            }

            Ship ship = Ships[shipIndex];

            Console.Write("Enter serial number of container to replace: ");
            string? oldSerial = Console.ReadLine();

            Container? oldContainer = ship.LoadedContainers.FirstOrDefault(c => c.SerialNumber == oldSerial);
            if (oldContainer == null)
            {
                Console.WriteLine("Container not found on ship.");
                return;
            }

            try
            {
                ship.RemoveContainer(oldContainer);
                ship.LoadContainer(newContainer);
                Console.WriteLine("Container replaced successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Replacement failed: {ex.Message}");
            }
        }

        
        
        public void TransferContainer()
        {
            if (Ships.Count < 2)
            {
                Console.WriteLine(" At least two ships are required for a transfer.");
                return;
            }

            PrintShips();
            Console.Write("Select sender ship number: ");
            int fromIndex = int.Parse(Console.ReadLine()!) - 1;
            Console.Write("Select receiver ship number: ");
            int toIndex = int.Parse(Console.ReadLine()!) - 1;

            if (!IsValidIndex(fromIndex, Ships.Count) || !IsValidIndex(toIndex, Ships.Count) || fromIndex == toIndex)
            {
                Console.WriteLine(" Invalid ship selection.");
                return;
            }

            Ship fromShip = Ships[fromIndex];
            Ship toShip = Ships[toIndex];

            Console.WriteLine($"\n Containers on ship \"{fromShip.Name}\":");
            if (fromShip.LoadedContainers.Count == 0)
            {
                Console.WriteLine("    No containers to transfer.");
                return;
            }

            foreach (var c in fromShip.LoadedContainers)
            {
                Console.WriteLine($"   {c.SerialNumber} | {c.ProductName ?? "Empty"} | {c.CargoMass} kg");
            }

            Console.Write("Enter serial number of container to transfer: ");
            string? serial = Console.ReadLine();

            Container? container = fromShip.LoadedContainers.FirstOrDefault(c => c.SerialNumber == serial);
            if (container == null)
            {
                Console.WriteLine(" Container not found on sender ship.");
                return;
            }

            try
            {
                fromShip.RemoveContainer(container);
                toShip.LoadContainer(container);
                Console.WriteLine(" Container transferred successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Transfer failed: {ex.Message}");
            }
        }
        

        public void PrintShipInfo()
        {
            if (Ships.Count == 0)
            {
                Console.WriteLine("No ships available.");
                return;
            }

            PrintShips();
            Console.Write("Select ship number: ");
            int index = int.Parse(Console.ReadLine()!) - 1;
            
            Ship ship = Ships[index];
            Console.WriteLine();
            Console.WriteLine(ship);
            ship.PrintContainers();
        }

        
        
        
        public void PrintShips()
        {
            Console.WriteLine("\n  List of ships:");
            for (int i = 0; i < Ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Ships[i].Name}");
            }
        }
        
        
        

        private void PrintContainerList(List<Container> containers)
        {
            Console.WriteLine("\n Available containers:");
            for (int i = 0; i < containers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {containers[i]}");
            }
        }

        private bool IsValidIndex(int index, int count)
        {
            return index >= 0 && index < count;
        }
    }
}
