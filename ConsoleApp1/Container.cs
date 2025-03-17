namespace ConsoleApp1
{
    public class Container
    {
        private static int counter = 0;
         static int GetNextNumber() => ++counter;

        public double CargoMass { get;  set; }
        public double ContainerHeight { get; set; }
        public double TareWeight { get; set; }
        public double ContainerDepth { get; set; }
        public string SerialNumber { get; set; }
        public double MaxPayload { get; set; }

        public Container(double cargoMass, double containerHeight, double tareWeight, double containerDepth, char type, double maxPayload)
        {
            SerialNumber = $"KON-{type}-{GetNextNumber()}";
            CargoMass = cargoMass;
            ContainerHeight = containerHeight;
            TareWeight = tareWeight;
            ContainerDepth = containerDepth;
            MaxPayload = maxPayload;
        }

        public virtual void EmptyCargo()
        {
            CargoMass = 0;
        }

        public virtual void LoadContainer(double cargoMass)
        {
            double allowed = 0.9 * MaxPayload;
            if (cargoMass > allowed)
            {
                Notify($"Error loading {cargoMass} kg, limit is exceeded.");
                throw new Exception("OverfillException");
            }
            CargoMass = cargoMass;
        }

        public virtual void Notify(string message)
        {
            Console.WriteLine(message);
        }
    }

    public interface IHazardNotifier
    {
        void Notify(string message);
    }

    public class LiquidContainer : Container, IHazardNotifier
    {
        public bool IsHazardous { get; set; }

        public LiquidContainer(double cargoMass, double containerHeight, double tareWeight, double containerDepth, char type,
            double maxPayload, bool isHazardous)
            : base(cargoMass, containerHeight, tareWeight, containerDepth, type, maxPayload)
        {
            IsHazardous = isHazardous;
            if (IsHazardous && cargoMass > 0.5 * MaxPayload)
                Notify("Hazard cargo exceeds 50% of capacity!");
        }

        public override void LoadContainer(double cargoMass)
        {
            if (IsHazardous)
            {
                double allowed = 0.5 * MaxPayload;
                if (cargoMass > allowed)
                {
                    Notify($"Error loading {cargoMass} kg, limit is exceeded.");
                    throw new Exception("OverfillException");
                }
                CargoMass = cargoMass;
            }
            else
            {
                base.LoadContainer(cargoMass);
            }
        }

        public override void Notify(string message)
        {
            Console.WriteLine($"{message} (Контейнер {SerialNumber})");
        }
    }

    public class GasContainer : Container, IHazardNotifier
    {
        public double Pressure { get; set; }

        public GasContainer(double cargoMass, double containerHeight, double tareWeight, double containerDepth, char type,
            double maxPayload, double pressure)
            : base(cargoMass, containerHeight, tareWeight, containerDepth, type, maxPayload)
        {
            Pressure = pressure;
        }

        public void EmptyGas()
        {
            double remaining = 0.05 * CargoMass;
            Notify($"5% of gas in ({remaining} kg) is left");
            CargoMass = remaining;
        }

        public override void Notify(string message)
        {
            Console.WriteLine($"{message} (Container {SerialNumber})");
        }
    }

    public class RefrigeratedContainer : Container, IHazardNotifier
    {
        public string ProductType { get; set; }
        public double Temperature { get; set; }

        public RefrigeratedContainer(double cargoMass, double containerHeight, double tareWeight, double containerDepth, char type,
            double maxPayload, string productType, double temperature)
            : base(cargoMass, containerHeight, tareWeight, containerDepth, type, maxPayload)
        {
            ProductType = productType;
            Temperature = temperature;
        }

        public void LoadContainer(Product product, double cargoMass)
        {
            if (product.Name != ProductType)
            {
                Notify("Product type does not match container type");
                throw new Exception("ProductTypeMismatchException");
            }
            if (Temperature < product.Temperature)
            {
                Notify("Product temperature is lower than container temperture");
                throw new Exception("TemperatureException");
            }
            base.LoadContainer(cargoMass);
        }

        public override void Notify(string message)
        {
            Console.WriteLine($"{message} (Container {SerialNumber})");
        }
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

        public Ship(int maxContainers, double maxSpeed, double maxContainersMass)
        {
            Containers = new List<Container>();
            MaxContainers = maxContainers;
            MaxSpeed = maxSpeed;
            MaxContainersMass = maxContainersMass;
        }

        public void AddContainer(Container container)
        {
            if (Containers.Count >= MaxContainers)
                throw new Exception("Too many contianers.");
            double currentMassTons = TotalContainersMass() / 1000.0;
            if (currentMassTons + (container.TareWeight + container.CargoMass) / 1000.0 > MaxContainersMass)
                throw new Exception("Max weight of containers is exceeded..");
            Containers.Add(container);
        }

        public void RemoveContainer(string serialNumber)
        {
            var container = Containers.Find(c => c.SerialNumber == serialNumber);
            if (container != null)
                Containers.Remove(container);
            else
                Console.WriteLine("Container not found");
        }

        public double TotalContainersMass()
        {
            double sum = 0;
            foreach (var c in Containers)
            {
                sum += c.TareWeight + c.CargoMass;
            }
            return sum;
        }

        public void PrintShipInfo()
        {
            Console.WriteLine($"Speed = {MaxSpeed}, containers  = {Containers.Count}/{MaxContainers}, total mass = {TotalContainersMass() / 1000.0} tons");
            foreach (var container in Containers)
            {
                Console.WriteLine($" Container {container.SerialNumber}: cargo = {container.CargoMass} kg, container capacity = {container.MaxPayload} kg");
            }
        }
    }
}
