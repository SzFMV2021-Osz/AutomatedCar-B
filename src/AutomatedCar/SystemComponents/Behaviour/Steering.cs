namespace AutomatedCar.SystemComponents.Behaviour
{
    using AutomatedCar.SystemComponents.Packets;

    public class Steering : SystemComponent
    {

        private SteeringPacket packet { get; }

        public Steering(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.packet = new SteeringPacket();
            this.virtualFunctionBus.SteeringPacket = this.packet;
        }

        private static int WHEEL_MIN = -100;
        private static int WHEEL_MAX = 100;

        private int wheel;

        /// <summary>
        /// Rotates the steering wheel left.
        /// </summary>
        public void RotateSteeringWheelLeft(int value = 1)
        {
            if (this.wheel - value >= WHEEL_MIN)
            {
                this.wheel -= value;
            }
        }

        /// <summary>
        /// Rotates the steering wheel right.
        /// </summary>
        public void RotateSteeringWheelRight(int value = 1)
        {
            if (this.wheel + value <= WHEEL_MAX)
            {
                this.wheel += value;
            }
        }

        /// <summary>
        /// Resets steering wheel to zero position.
        /// </summary>
        public void ReleaseSteeringWheel()
        {
            if (this.wheel > 0)
            {
                this.wheel--;
            }

            if (this.wheel < 0)
            {
                this.wheel++;
            }
        }

        /// <inheritdoc/>
        public override void Process()
        {
            this.packet.WheelPosition = this.wheel;
        }
    }
}
