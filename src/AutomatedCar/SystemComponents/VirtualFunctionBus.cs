namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.SystemComponents.Packets;
    using AutomatedCar.Models;
    using System.Collections.Generic;

    public class VirtualFunctionBus : GameBase
    {
        private List<SystemComponent> components = new List<SystemComponent>();

        public IReadonlyGearboxPacket GearboxPacket;

        public IReadonlyPedalPacket ReadonlyPedalPacket;

        public IReadonlyVelocityPacket ReadonlyVelocityPacket;

        public SteeringPacket SteeringPacket { get; set; }

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