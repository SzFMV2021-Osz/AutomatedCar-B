namespace AutomatedCar.SystemComponents.Packets
{
    class EnginePacket : IReadonlyEnginePacket
    {
        private int engineRPM;

        public int EngineRPM
        {
            get => this.engineRPM;
            set => this.engineRPM = value;
        }
    }
}
