namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;

    public class SteeringPacket : ReactiveObject
    {
        private int wheel;

        public int WheelPosition
        {
            get => this.wheel;
            set => this.RaiseAndSetIfChanged(ref this.wheel, value);
        }
    }
}