namespace AutomatedCar.SystemComponents.Behaviour
{
    using ReactiveUI;

    public class SteeringLogic : ReactiveObject
    {
        private static int WHEEL_MIN = -100;
        private static int WHEEL_MAX = 100;

        private int wheel;

        public int WheelPosition
        {
            get => this.wheel;
            set => this.RaiseAndSetIfChanged(ref this.wheel, value);
        }

        /// <summary>
        /// Rotates the steering wheel left.
        /// </summary>
        public void RotateSteeringWheelLeft()
        {
            if (this.WheelPosition - 1 >= WHEEL_MIN)
            {
                this.WheelPosition--;
            }
        }

        /// <summary>
        /// Rotates the steering wheel right.
        /// </summary>
        public void RotateSteeringWheelRight()
        {
            if (this.WheelPosition + 1 <= WHEEL_MAX)
            {
                this.WheelPosition++;
            }
        }

        /// <summary>
        /// Resets steering wheel to zero position.
        /// </summary>
        public void ReleaseSteeringWheel()
        {
            if (this.WheelPosition > 0)
            {
                this.WheelPosition--;
            }

            if (this.WheelPosition < 0)
            {
                this.WheelPosition++;
            }
        }
    }
}
