namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;

    public class VelocityPacket : ReactiveObject, IReadonlyVelocityPacket
    {
        private double velocity;

        public double Velocity
        {
            get => this.velocity;
            set => this.RaiseAndSetIfChanged( ref this.velocity, value);
        }
    }
}
