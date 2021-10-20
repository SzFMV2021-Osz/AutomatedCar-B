namespace AutomatedCar.Models
{
    using Avalonia.Media;
    using global::AutomatedCar.SystemComponents;
    using global::AutomatedCar.SystemComponents.Behaviour;

    public class AutomatedCar : Car
    {
        private VirtualFunctionBus virtualFunctionBus;
        private DummyPedals dummyPedals;
        private Engine engine;

        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.virtualFunctionBus = new VirtualFunctionBus();
            this.dummyPedals = new DummyPedals(this.virtualFunctionBus);
            this.engine = new Engine(this.virtualFunctionBus);
            this.ZIndex = 10;
        }

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        public int Revolution { get; set; }
        public int Velocity { get; set; }
        public Geometry Geometry { get; set; }

        public DummyPedals DummyPedals => this.dummyPedals;

        public Engine Engine { get { return this.engine; } }

        /// <summary>Starts the automated cor by starting the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Start()
        {
            this.virtualFunctionBus.Start();
        }

        /// <summary>Stops the automated cor by stopping the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Stop()
        {
            this.virtualFunctionBus.Stop();
        }
    }
}