namespace AutomatedCar.SystemComponents.Behaviour
{
    using System;
    using AutomatedCar.SystemComponents.Packets;
    using ReactiveUI;

    /// <summary>
    /// The logic behind the car's velocity.
    /// </summary>
    public class VelocityVectorCalculator : SystemComponent
    {
        private VelocityPacket packet;

        private double speed;
        public double Speed { get => this.speed; private set => this.RaiseAndSetIfChanged(ref this.speed, value); }

        /// <summary>
        /// Initializes a new instance of the <see cref="VelocityVectorCalculator"/> class.
        /// </summary>
        /// <param name="virtualFunctionBus">The virtual function bus.</param>
        public VelocityVectorCalculator(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.packet = new VelocityPacket();
            this.virtualFunctionBus.ReadonlyVelocityPacket = this.packet;
        }

        /// <summary>
        /// Calculates the velocity vector's length.
        /// </summary>
        /// <returns>The velocity.</returns>
        public double CalculateVelocity()
        {
            return this.virtualFunctionBus.ReadonlyGearboxPacket.Torque / 50;
        }

        /// <inheritdoc/>
        public override void Process()
        {
            this.packet.Velocity = this.CalculateVelocity();

            // TODO this is stupid
            //((Models.AutomatedCar)this.virtualFunctionBus.Owner).Y -= Convert.ToInt32(Math.Round(this.packet.Velocity));
        }
    }
}
