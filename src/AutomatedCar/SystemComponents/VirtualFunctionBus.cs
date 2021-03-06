namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.SystemComponents.Packets;
    using AutomatedCar.Models;
    using System.Collections.Generic;

    public class VirtualFunctionBus : GameBase
    {
        private List<SystemComponent> components = new List<SystemComponent>();

        public IReadonlyGearboxPacket ReadonlyGearboxPacket;

        public IReadonlyPedalPacket ReadonlyPedalPacket;

        public IReadonlyEnginePacket ReadonlyEnginePacket;

        public IReadonlyVelocityPacket ReadonlyVelocityPacket;

        public IRadarPacket RadarPacket;

        public SteeringPacket SteeringPacket { get; set; }

        public CameraPacket CameraPacket { get; set; }

        public IReadonlyCollisionSensorPacket CollisionSensorPacket { get; set; }

        public WorldObject Owner { get; }

        public VirtualFunctionBus(WorldObject owner)
        {
            this.Owner = owner;
        }

        public void RegisterComponent(SystemComponent component)
        {
            this.components.Add(component);
        }

        protected override void Tick()
        {
            foreach (SystemComponent component in this.components)
            {
                component.Process();
            }
        }
    }
}
