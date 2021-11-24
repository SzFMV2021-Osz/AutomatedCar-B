namespace AutomatedCar.SystemComponents.Behaviour
{
    using System;
    using AutomatedCar.SystemComponents.Packets;
    using ReactiveUI;

    /// <summary>
    /// The logic behind the car's velocity.
    /// </summary>
    public class SpeedCalculator : SystemComponent
    {
        private SpeedPacket packet;

        const double SpeedScale = 0.02;

        /// <summary>
        /// The slowing factor.
        /// </summary>
        const double Drag = 0.99;

        private double speed;

        public double Speed { get => this.speed; private set => this.RaiseAndSetIfChanged(ref this.speed, value); }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeedCalculator"/> class.
        /// </summary>
        /// <param name="virtualFunctionBus">The virtual function bus.</param>
        public SpeedCalculator(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.packet = new SpeedPacket();
            this.virtualFunctionBus.ReadonlyVelocityPacket = this.packet;
        }

        /// <summary>
        /// Calculates the velocity vector's length.
        /// </summary>
        /// <returns>The velocity.</returns>
        public double CalculateVelocity()
        {
            switch (this.virtualFunctionBus.ReadonlyGearboxPacket.CurrentGear)
            {
                case Gear.Park:
                    return 0;
                case Gear.Reverse:
                    return -this.virtualFunctionBus.ReadonlyGearboxPacket.Torque * SpeedScale;
                case Gear.Neutral:
                    return this.Speed;
                case Gear.Drive:
                    return this.virtualFunctionBus.ReadonlyGearboxPacket.Torque * SpeedScale;
            }

            throw new Exception("Invalid gear.");
        }

        /// <inheritdoc/>
        public override void Process()
        {
            this.Speed = this.CalculateVelocity() * Drag;
            this.packet.Speed = this.Speed;
        }
    }
}
