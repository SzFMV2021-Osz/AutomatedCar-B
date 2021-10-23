namespace AutomatedCar.Models
{
    using Avalonia.Media;
  using ReactiveUI;
  using SystemComponents;

    public class AutomatedCar : Car
    {
        private static int WHEEL_MIN = -100;
        private static int WHEEL_MAX = 100;

        private int wheel;

        private VirtualFunctionBus virtualFunctionBus;

        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.virtualFunctionBus = new VirtualFunctionBus();
            this.ZIndex = 10;
            this.wheel = 0;
        }

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        public int wheelPosition
        {
            get => this.wheel;
            set => this.RaiseAndSetIfChanged(ref this.wheel, value);
        }

        public int Revolution { get; set; }

        public int Velocity { get; set; }

        public Geometry Geometry { get; set; }

        /// <summary>Starts the automated car by starting the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Start()
        {
            this.virtualFunctionBus.Start();
        }

        /// <summary>Stops the automated car by stopping the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Stop()
        {
            this.virtualFunctionBus.Stop();
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