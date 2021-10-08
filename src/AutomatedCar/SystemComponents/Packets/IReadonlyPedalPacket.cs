namespace AutomatedCar.SystemComponents.Packets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IReadonlyPedalPacket
    {

        public int GasPedal { get; }
        public int BrakePedal { get; }
    }
}
