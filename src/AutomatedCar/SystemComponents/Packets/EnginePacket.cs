namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;

    class EnginePacket : ReactiveObject, IReadonlyEnginePacket
    {
        private int engineRPM;

        public int EngineRPM
        {
            get => this.engineRPM;
            set => this.RaiseAndSetIfChanged( ref this.engineRPM, value);
        }
    }
}
