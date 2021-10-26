// <copyright file="Engine.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Behaviour
{
    using AutomatedCar.SystemComponents.Packets;

    public class Engine : SystemComponent
    {
        private const int MaxRPM = 7000; // Maximálisan megengedett fordulatszám
        private const int BaseRPM = 500; // Naggyáboli alapjárati érték
        private const double PedalScaling = 0.2;
        private const double BrakePedalScaling = 5;
        private const int RPMReduction = -50;

        private double gasPedalValue; // Gázpedál állásának tárolása
        private double breakPedalValue; // Fékpedál állásának tárolása
        private EngineRPM rpm;
        private Gear currentGear;
        private EnginePacket enginePacket;

        public Engine(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.gasPedalValue = virtualFunctionBus.ReadonlyPedalPacket.GasPedal;
            this.breakPedalValue = virtualFunctionBus.ReadonlyPedalPacket.BrakePedal;
            this.currentGear = Gear.Park;
            this.rpm = new EngineRPM();
            this.rpm.RPM = BaseRPM;
            this.enginePacket = new EnginePacket();
            virtualFunctionBus.ReadonlyEnginePacket = this.enginePacket;
        }

        private int CalculateRPMChange()
        {
            if (this.gasPedalValue != 0)
            {
                if (this.breakPedalValue == 0)
                {
                    return (int)(this.gasPedalValue * PedalScaling);
                }
                else
                {
                    return (int)(this.breakPedalValue * PedalScaling) * -1;
                }
            }
            else
            {
                return RPMReduction - (int)(this.breakPedalValue * BrakePedalScaling);
            }
        }

        private int UpdateRPMValue()
        {
            switch (this.currentGear)
            {
                case Gear.Park:
                case Gear.Neutral:
                    this.rpm.RPM = BaseRPM;
                    return 0;
                case Gear.Drive:
                case Gear.Reverse:
                    int tempRPM = this.rpm.RPM + this.CalculateRPMChange();
                    if (tempRPM < MaxRPM)
                    {
                        if (tempRPM <= BaseRPM)
                        {
                            this.rpm.RPM = BaseRPM;
                            if (this.breakPedalValue != 0)
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            this.rpm.RPM = tempRPM;
                        }
                    }

                    return (int)this.rpm.RPM;
                default:
                    return 0;
            }
        }

        public override void Process()
        {
            this.gasPedalValue = virtualFunctionBus.ReadonlyPedalPacket.GasPedal;
            this.breakPedalValue = virtualFunctionBus.ReadonlyPedalPacket.BrakePedal;
            this.currentGear = virtualFunctionBus.GearboxPacket.Gear;
            int transmissionToRPM = this.UpdateRPMValue();
            enginePacket.EngineRPM = transmissionToRPM;
        }
    }
}
