namespace AutomatedCar.Models
{
    using System;
    using System.Threading;
    using Avalonia;
    using Avalonia.Media;
    using global::AutomatedCar.SystemComponents;
    using global::AutomatedCar.SystemComponents.Behaviour;
    using global::AutomatedCar.SystemComponents.Packets;
    using ReactiveUI;

    public class AutomatedCar : Car, IControlledCar
    {
        private VirtualFunctionBus virtualFunctionBus;

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        public int Revolution { get; set; }

        public Geometry Geometry { get; set; }

        public Pedals Pedals { get; }

        public VelocityVectorCalculator VelocityVectorCalculator { get; }

        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.virtualFunctionBus = new VirtualFunctionBus(this);
            this.Pedals = new Pedals(this.virtualFunctionBus);
            this.VelocityVectorCalculator = new VelocityVectorCalculator(this.virtualFunctionBus);
            this.ZIndex = 10;
        }

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