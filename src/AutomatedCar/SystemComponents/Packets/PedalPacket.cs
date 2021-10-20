namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;

    public class PedalPacket : ReactiveObject, IReadonlyPedalPacket
    {
        private int gaspedal;
        private int brakePedal;

        public int BrakePedal
        {
            get => this.brakePedal;
            set => this.RaiseAndSetIfChanged( ref this.brakePedal, value);
        }

        public int GasPedal
        {
            get => this.gaspedal;
            set => this.RaiseAndSetIfChanged(ref this.gaspedal, value);
        }
    }
}
