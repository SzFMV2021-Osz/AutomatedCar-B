namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;
    public class CollisionSensorPacket : ReactiveObject, IReadonlyCollisionSensorPacket
    {
        private CollisionType collisionType;

        public CollisionType CollisionType
        {
            get => this.collisionType;
            set => this.RaiseAndSetIfChanged(ref this.collisionType, value);
        }
    }
} 