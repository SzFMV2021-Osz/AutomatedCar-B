namespace AutomatedCar.Models
{
    using Avalonia.Media;
    using global::AutomatedCar.SystemComponents;
    using global::AutomatedCar.SystemComponents.Behaviour;
    using System;
    using System.Numerics;

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

        /// <summary>
        /// Gets the business logic of the car's steering.
        /// </summary>
        public Steering Steering { get; }

        private double deltaX;

        private double deltaY;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutomatedCar"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="filename"></param>
        public AutomatedCar(int x, int y, string filename)
                    : base(x, y, filename)
        {
            this.VirtualFunctionBus = new VirtualFunctionBus(this);
            this.Pedals = new Pedals(this.VirtualFunctionBus);
            this.VelocityVectorCalculator = new VelocityVectorCalculator(this.VirtualFunctionBus);
            this.Steering = new Steering(this.VirtualFunctionBus);
            this.ZIndex = 10;
            this.deltaX = 0;
            this.deltaY = 0;
        }

        public void CalculateNextPosition()
        {
            var velocity = this.VirtualFunctionBus.ReadonlyVelocityPacket.Velocity;
            var wheel = this.VirtualFunctionBus.SteeringPacket.WheelPosition;

            this.deltaX = (-1) * Math.Cos((this.Rotation + 90) * (Math.PI / 180)) * velocity;
            this.deltaY = (-1) * Math.Sin((this.Rotation + 90) * (Math.PI / 180)) * velocity;

            double steerRadius = (130 * Math.Tan(wheel)) + 90;

            this.X += (int)this.deltaX;
            this.Y += (int)this.deltaY;
        }

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
