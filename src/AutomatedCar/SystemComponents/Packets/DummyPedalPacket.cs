namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;

    class DummyPedalPacket : ReactiveObject, IReadonlyDummyPedalPacket
    {
        private double gaspedal;
        private double brakepedal;

        public double Gaspeadl
        {
            get => gaspedal;
            set => this.RaiseAndSetIfChanged( ref this.gaspedal, value);
        }

        public double Brakepedal
        {
            get => brakepedal;
            set => this.RaiseAndSetIfChanged(ref this.brakepedal, value);
        }
    }
}
