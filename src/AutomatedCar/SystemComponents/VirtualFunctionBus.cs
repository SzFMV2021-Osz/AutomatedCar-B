namespace AutomatedCar.SystemComponents
{
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;

    public class VirtualFunctionBus : GameBase
    {
        private List<SystemComponent> components = new List<SystemComponent>();

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