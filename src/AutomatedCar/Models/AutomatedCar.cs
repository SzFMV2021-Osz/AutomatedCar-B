namespace AutomatedCar.Models
{
    using System;
    using System.Threading;
    using Avalonia;
    using Avalonia.Media;
    using ReactiveUI;
    using global::AutomatedCar.SystemComponents;
    using global::AutomatedCar.SystemComponents.Behaviour;
    using global::AutomatedCar.SystemComponents.Packets;
  
    public class AutomatedCar : Car
    {
        public VirtualFunctionBus VirtualFunctionBus { get; }
        
        public int Revolution { get; set; }

        public Geometry Geometry { get; set; }

        /// <summary>
        /// Gets the busines loggic behind the car's pedals.
        /// </summary>
        public Pedals Pedals { get; }

        /// <summary>
        /// Gets the busines logic of the car's movement.
        /// </summary>
        public VelocityVectorCalculator VelocityVectorCalculator { get; }
        
        private static int WHEEL_MIN = -100;
        private static int WHEEL_MAX = 100;

        private int wheel;

        public int wheelPosition
        {
            get => this.wheel;
            set => this.RaiseAndSetIfChanged(ref this.wheel, value);
        }
        
        private VirtualFunctionBus virtualFunctionBus;


        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.VirtualFunctionBus = new VirtualFunctionBus(this);
            this.Pedals = new Pedals(this.VirtualFunctionBus);
            this.VelocityVectorCalculator = new VelocityVectorCalculator(this.VirtualFunctionBus);
            this.ZIndex = 10;
            this.wheel = 0;
        }
        
        /// <summary>Starts the automated cor by starting the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>

        public void Start()
        {
            this.VirtualFunctionBus.Start();
        }

        /// <summary>Stops the automated car by stopping the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Stop()
        {
            this.VirtualFunctionBus.Stop();
        }

        /// <summary>
        /// Rotates the steering wheel left.
        /// </summary>
        public void RotateSteeringWheelLeft()
        {
            if (this.wheelPosition - 1 >= WHEEL_MIN)
            {
                this.wheelPosition--;
            }
        }

        /// <summary>
        /// Rotates the steering wheel right.
        /// </summary>
        public void RotateSteeringWheelRight()
        {
            if (this.wheelPosition + 1 <= WHEEL_MAX)
            {
                this.wheelPosition++;
            }
        }

        /// <summary>
        /// Resets steering wheel to zero position.
        /// </summary>
        public void ReleaseSteeringWheel()
        {
            if (this.wheelPosition > 0)
            {
                this.wheelPosition--;
            }

            if (this.wheelPosition < 0)
            {
                this.wheelPosition++;
            }
        }
    }
}
