namespace AutomatedCar.Models
{
    using Avalonia.Media;
    using global::AutomatedCar.SystemComponents;
    using global::AutomatedCar.SystemComponents.Behaviour;

    public class AutomatedCar : Car
    {
        private VirtualFunctionBus virtualFunctionBus;

        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.virtualFunctionBus = new VirtualFunctionBus();
            this.Gearbox = new AutomaticGearbox(this.virtualFunctionBus);
            this.ZIndex = 10;
        }

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        /// <summary>
        /// Gets the automated car's gearbox.
        /// </summary>
        public IGearbox Gearbox { get; }

        public int Revolution { get; set; }

        public int Velocity { get; set; }

        public Geometry Geometry { get; set; }

        /// <summary>
        /// Starts the automated car by starting the ticker in the Virtual Function Bus, that cyclically calls the system components.
        /// </summary>
        public void Start()
        {
            this.virtualFunctionBus.Start();
        }

        /// <summary>
        /// Stops the automated car by stopping the ticker in the Virtual Function Bus, that cyclically calls the system components.
        /// </summary>
        public void Stop()
        {
            this.virtualFunctionBus.Stop();
        }
    }
}