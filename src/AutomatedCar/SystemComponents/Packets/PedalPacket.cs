namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;

    /// <summary>
    /// The packet sent by pedals.
    /// </summary>
    public class PedalPacket : IReadonlyPedalPacket
    {
        /// <summary>
        /// Gets or sets the brake pedal's position (%).
        /// </summary>
        public int BrakePedal { get; set; }

        /// <summary>
        /// Gets or sets the gas pedal's position (%).
        /// </summary>
        public int GasPedal { get; set; }
    }
}
