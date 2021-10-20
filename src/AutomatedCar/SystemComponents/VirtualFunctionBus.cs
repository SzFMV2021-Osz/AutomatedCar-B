namespace AutomatedCar.SystemComponents
{
    using System.Collections.Generic;
    using SystemComponents.Packets;

    public class VirtualFunctionBus : GameBase
    {
        private List<SystemComponent> components = new List<SystemComponent>();

        public IReadonlyDummyPedalPacket ReadonlyDummyPedalPacket;

        public IReadonlyEnginePacket ReadonlyEnginePacket;

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