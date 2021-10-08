namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PedalPacket : ReactiveObject
    {
        private int gaspedal;
        private int brakePedal;

        public int Brakepedal
        {
            get => this.brakePedal;
            set => this.RaiseAndSetIfChanged( ref this.brakePedal, value);
        }
        public int GasPedal
        {
            get => this.gaspedal;
            set => this.RaiseAndSetIfChanged(ref this.gaspedal, value);
        }
    }
}
