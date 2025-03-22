using ContainersApp;

namespace Containers
{
    class Program
    {
        static ContainerManager containerManager = new ContainerManager();
        static ShipManager shipManager = new ShipManager();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" APP MENU ");
                Console.WriteLine("1. Create container");
                Console.WriteLine("2. Load cargo into container");
                Console.WriteLine("3. Create ship");
                Console.WriteLine("4. Load container onto ship");
                Console.WriteLine("5. Load multiple containers onto ship");
                Console.WriteLine("6. Remove container from ship");
                Console.WriteLine("7. Unload container");
                Console.WriteLine("8. Replace container on ship");
                Console.WriteLine("9. Transfer container between ships");
                Console.WriteLine("10. Show container info");
                Console.WriteLine("11. Show ship and its containers info");
                // Console.WriteLine("12. Clean container");
                Console.WriteLine("13. Exit");
                Console.Write("Select an option: ");
                string? choice = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            containerManager.CreateContainer();
                            break;
                        case "2":
                            containerManager.LoadCargoIntoContainer();
                            break;
                        case "3":
                            shipManager.CreateShip();
                            break;
                        case "4":
                            containerManager.PrintContainers();
                            Console.Write("Select container number to load: ");
                            int cIndex = int.Parse(Console.ReadLine()!) - 1;
                            if (cIndex >= 0 && cIndex < containerManager.Containers.Count)
                                shipManager.LoadContainerOntoShip(containerManager.Containers[cIndex]);
                            else
                                Console.WriteLine("Invalid container selection.");
                            break;
                        case "5":
                            shipManager.LoadMultipleContainersOntoShip(containerManager.Containers);
                            break;
                        case "6":
                            shipManager.RemoveContainer();
                            break;
                        case "7":
                            containerManager.UnloadContainer();
                            break;
                        case "8":
                            containerManager.PrintContainers();
                            Console.Write("Select new container number for replacement: ");
                            int newIndex = int.Parse(Console.ReadLine()!) - 1;
                            if (newIndex >= 0 && newIndex < containerManager.Containers.Count)
                                shipManager.ReplaceContainer(containerManager.Containers[newIndex]);
                            else
                                Console.WriteLine("Invalid container selection.");
                            break;
                        case "9":
                            shipManager.TransferContainer();
                            break;
                        case "10":
                            containerManager.PrintContainerInfo();
                            break;
                        case "11":
                            shipManager.PrintShipInfo();
                            break;
                        // case "12":
                        // I had an idea that would allow us to clear it
                        // with another fuel or gas but I m having trouble with GasContainer. 

                        // containerManager.CleanContainer();
                        // break;
                        case "13":
                            return;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}