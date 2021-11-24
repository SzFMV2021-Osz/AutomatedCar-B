namespace AutomatedCar.SystemComponents.Packets
{
    public interface IReadonlyCollisionSensorPacket
    {
        public CollisionType CollisionType { get; }
    }
}