namespace ConsoleApp1
{
    public class Container
    {
         double cargoMass;
         double containerHeight;
         double tareWeight;
         double containerDepth;
         string serialNumber;
         double maxPayload;

        public double CargoMass
        {
            get { return cargoMass; }
            set { cargoMass = value; }
        }

        public double ContainerHeight
        {
            get { return containerHeight; }
            set { containerHeight = value; }
        }

        public double TareWeight
        {
            get { return tareWeight; }
            set { tareWeight = value; }
        }

        public double ContainerDepth
        {
            get { return containerDepth; }
            set { containerDepth = value; }
        }

        public string SerialNumber
        {
            get { return serialNumber; }
            private set { serialNumber = value; }
        }

        public double MaxPayload
        {
            get { return maxPayload; }
            set { maxPayload = value; }
        }

        public Container(double mass, double height, double tareWeight, double depth, char type, int number, double maxPayload)
        {
            this.serialNumber = $"KON-{type}-{number}";
            this.cargoMass = mass;
            this.containerHeight = height;
            this.tareWeight = tareWeight;
            this.containerDepth = depth;
            this.maxPayload = maxPayload;
        }

        public void EmptyCargo()
        {
            this.CargoMass = 0;
        }

        public void LoadContainer(double cargoMass)
        {
            if (cargoMass > MaxPayload)
            {
                throw new Exception("OverfillException");
            }
            else
            {
                this.CargoMass = cargoMass;
            }
        }

        public void Notify(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class LiquidContainer : Container, IHazardNotifier
    {
        private bool isHazardous;

        public bool IsHazardous
        {
            get { return isHazardous; }
            set { isHazardous = value; }
        }

        public LiquidContainer(double cargoMass, double containerHeight, double tareWeight, double containerDepth, char type, int number, double maxPayload, bool isHazardous)
            : base(cargoMass, containerHeight, tareWeight, containerDepth, type, number, maxPayload)
        {
            this.isHazardous = isHazardous;
            if (this.isHazardous && cargoMass > this.MaxPayload / 2)
            {
                Notify("Hazardous mass exceeds 50% of container capacity");
            }
        }

        public void Notify(string message)
        {
            Console.WriteLine(message);
        }
    }

    public interface IHazardNotifier
    {
        void Notify(string message);
    }

    public class GasContainer : Container, IHazardNotifier
    {
        private double pressure;

        public double Pressure
        {
            get { return pressure; }
            set { pressure = value; }
        }

        public GasContainer(double cargoMass, double containerHeight, double tareWeight, double containerDepth, char type, int number, double maxPayload, double pressure)
            : base(cargoMass, containerHeight, tareWeight, containerDepth, type, number, maxPayload)
        {
            this.pressure = pressure;
        }

        public void EmptyGas(double newWeight)
        {
            if (newWeight > 0.95)
            {
                Notify("5% of the gas should be left");
            }
            else if (newWeight > 1)
            {
                Notify("Too much gas");
            }
            this.CargoMass += newWeight;
        }

        public void Notify(string message)
        {
            Console.WriteLine(message + this.SerialNumber);
        }
    }

    public class RefrigeratedContainer : Container, IHazardNotifier
    {
        private string type;
        private double temperature;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public double Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        public RefrigeratedContainer(double cargoMass, double containerHeight, double tareWeight, double containerDepth, char type, int number, double maxPayload, string productType, double temperature)
            : base(cargoMass, containerHeight, tareWeight, containerDepth, type, number, maxPayload)
        {
            this.type = productType;
            this.temperature = temperature;
        }

        public void Notify(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class Product
    {
        private string name;
        private double temperature;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }
    }

    public class Ship
    {
        private List<Container> containers;
        private int maxContainers;
        private double maxContainersMass;
        private double maxSpeed;

        public List<Container> Containers
        {
            get { return containers; }
            set { containers = value; }
        }

        public int MaxContainers
        {
            get { return maxContainers; }
            set { maxContainers = value; }
        }

        public double MaxContainersMass
        {
            get { return maxContainersMass; }
            set { maxContainersMass = value; }
        }

        public double MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        public Ship(List<Container> containers, int maxContainers, double maxSpeed, double maxContainersMass)
        {
            this.containers = containers;
            this.maxContainers = maxContainers;
            this.maxSpeed = maxSpeed;
            this.maxContainersMass = maxContainersMass;
        }
    }
}
