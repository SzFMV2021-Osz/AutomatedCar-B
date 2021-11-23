namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;

    public class SpeedPacket : ReactiveObject, IReadonlyVelocityPacket
    {
        private double speed;

        public double Speed
        {
            get => this.speed;
            set => this.RaiseAndSetIfChanged(ref this.speed, value);
        }
    }
}
