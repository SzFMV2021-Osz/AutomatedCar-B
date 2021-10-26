// <copyright file="EngineComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Behaviour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutomatedCar.SystemComponents.Packets;
    using AutomatedCar.SystemComponents.Behaviour;

    public class Engine : SystemComponent
    {
        private const int MaxRPM = 7000; // Maximálisan megengedett fordulatszám
        private const int BaseRPM = 500; // Naggyáboli alapjárati érték
        private const double GasPedalScaling = 0.2;
        private const double BrakePedalScaling = 5;
        private const int RPMReduction = -50;

        private double gasPedalValue; // Gázpedál állásának tárolása
        private double breakPedalValue; // Fékpedál állásának tárolása
        private int RPM;
        private Gear CurrentGear;
        private EnginePacket enginePacket;

        public Engine(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.gasPedalValue = virtualFunctionBus.ReadonlyPedalPacket.GasPedal;
            this.breakPedalValue = virtualFunctionBus.ReadonlyPedalPacket.BrakePedal;
            this.CurrentGear = Gear.Drive;
            this.RPM = 0;
            this.enginePacket = new EnginePacket();
            virtualFunctionBus.ReadonlyEnginePacket = this.enginePacket;
        }

        private int CalculateRPMChange() 
        {
            if (this.gasPedalValue != 0)
            {
                return (int)(this.gasPedalValue * GasPedalScaling);
            }
            else
            {
                return RPMReduction - (int)(this.breakPedalValue * BrakePedalScaling);
            }
        }

        private void UpdateRPMValue() 
        {
            switch (this.CurrentGear)
            {
                case Gear.Park:
                    this.RPM = 0;
                    break;
                case Gear.Neutral:
                    this.RPM = BaseRPM;
                    break;
                case Gear.Drive:
                    int tempRPM = this.RPM + this.CalculateRPMChange();
                    if (tempRPM < MaxRPM)
                    {
                        if (tempRPM <= 0)
                        {
                            this.RPM = 0;
                        }
                        else
                        {
                            this.RPM = tempRPM;
                        }
                    }
                    else
                    {
                        this.RPM = MaxRPM;
                    }
                    break;
                case Gear.Reverse:
                    break;
            }
        }

        public override void Process()
        {
            this.gasPedalValue = virtualFunctionBus.ReadonlyPedalPacket.GasPedal;
            this.breakPedalValue = virtualFunctionBus.ReadonlyPedalPacket.BrakePedal;
            this.CurrentGear = Gear.Drive;
            this.UpdateRPMValue();
            enginePacket.EngineRPM = this.RPM;
        }
    }
}
