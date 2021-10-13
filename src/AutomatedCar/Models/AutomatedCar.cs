namespace AutomatedCar.Models
{
    using Avalonia;
    using Avalonia.Media;
    using global::AutomatedCar.SystemComponents.Packets;
    using ReactiveUI;
    using System;
    using System.Threading;
    using SystemComponents;
    public class AutomatedCar : Car, IControlledCar
    {
        private VirtualFunctionBus virtualFunctionBus;

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        public PedalPacket PedalPacket;

        public IReadonlyPedalPacket readonlyPedalPacket;

        public int Revolution { get; set; }
        public Vector Velocity { get; set; }
        public Geometry Geometry { get; set; }

        const int Min_Pedal = 0;

        const int Max_Pedal = 100;

        public double Speed { get; set; }

        private const double Net = 0.8;

        public Vector Acceleration { get; set; }

        private int gasPosition;
        public int GasPosition
        {
            get { return this.gasPosition; }
            set
            {
                this.RaiseAndSetIfChanged(ref this.gasPosition, value);
            }
        }

        private int brakePosition;

        public int BrakePosition
        {
            get { return brakePosition; }
            set
            {
                this.RaiseAndSetIfChanged(ref this.brakePosition, value);
            }
        }

        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.virtualFunctionBus = new VirtualFunctionBus();
            this.ZIndex = 10;
            this.GasPosition = 0;
            this.BrakePosition = 0;
            this.Acceleration = new Vector();
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

        public void IncreaseGas()
        {
            if (GasPosition <= Max_Pedal)
            {
                GasPosition += 1;
            }
            else
            {
                GasPosition = 100;
            }
        }

        public void IncreaseBrake()
        {
            if (BrakePosition <= Max_Pedal)
            {
                BrakePosition += 1;
            }
            else
            {
                BrakePosition = 100;
            }
        }
        public void CalculateSpeed()
        {
            //speed = sqrt(v.x*v.x + v.y*v.y);
            Speed = (int)Math.Sqrt(Math.Pow(Velocity.X, 2) + Math.Pow(Velocity.Y, 2));
        }
    }
}