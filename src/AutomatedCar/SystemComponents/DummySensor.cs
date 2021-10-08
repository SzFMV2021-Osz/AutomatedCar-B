namespace AutomatedCar.SystemComponents
{
    using System;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    public class DummySensor : SystemComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DummySensor"/> class.
        /// </summary>
        /// <param name="virtualFunctionBus">R.</param>
        public DummySensor(VirtualFunctionBus virtualfunctionbus) : base(virtualfunctionbus)
        {
            this.virtualFunctionBus = this.virtualFunctionBus;
            this.dummypacket = new DummyPacket();
            this.virtualFunctionBus.DummyPacket = this.dummypacket;
        }

        /// <summary>
        /// Gets or sets the dummypacket.
        /// </summary>
        public DummyPacket dummypacket { get; set; }

        public override void Process()
        {
            AutomatedCar car = World.Instance.ControlledCar;
            Circle c = (Circle)World.Instance.WorldObjects[0];
            this.dummypacket.DistanceX = Math.Abs(car.X - c.X);
            this.dummypacket.DistanceY = Math.Abs(car.Y - c.Y);
        }
    }
}
