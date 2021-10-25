namespace AutomatedCar.Models
{
    using Avalonia.Media;
    using global::AutomatedCar.SystemComponents.Behaviour;
    using SystemComponents;

    public class AutomatedCar : Car
    {
        public VirtualFunctionBus VirtualFunctionBus { get; }

        public Steering Steering { get; }

        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.VirtualFunctionBus = new VirtualFunctionBus();
            this.Steering = new Steering(this.VirtualFunctionBus);
            this.ZIndex = 10;
        }

        public int Revolution { get; set; }

        public int Velocity { get; set; }

        public Geometry Geometry { get; set; }

        /// <summary>Starts the automated car by starting the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Start()
        {
            this.VirtualFunctionBus.Start();
        }

        /// <summary>Stops the automated car by stopping the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Stop()
        {
            this.VirtualFunctionBus.Stop();
        }

    }
}