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
    using ReactiveUI;

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
        private Gear CurrentGear;
        private EnginePacket enginePacket;

        public Engine(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.gasPedalValue = virtualFunctionBus.ReadonlyPedalPacket.GasPedal;
            this.breakPedalValue = virtualFunctionBus.ReadonlyPedalPacket.BrakePedal;
            this.CurrentGear = Gear.Park;
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
            switch (this.CurrentGear)
            {
                case Gear.Park:
                    this.rpm.RPM = BaseRPM;
                    return 0;
                case Gear.Neutral:
                    this.rpm.RPM = BaseRPM;
                    return 0;
                case Gear.Drive:
                    if (this.breakPedalValue == 0)
                    {
                        int tempRPM = this.rpm.RPM + this.CalculateRPMChange();
                        if (tempRPM < MaxRPM)
                        {
                            if (tempRPM <= BaseRPM)
                            {
                                this.rpm.RPM = BaseRPM;
                            }
                            else
                            {
                                this.rpm.RPM = tempRPM;
                            }
                        }
                        else
                        {
                            this.rpm.RPM = MaxRPM;
                        }
                    }
                    else
                    {
                        int tempRPM = this.rpm.RPM + this.CalculateRPMChange();
                        if (tempRPM < MaxRPM)
                        {
                            if (tempRPM <= BaseRPM)
                            {
                                this.rpm.RPM = BaseRPM;
                                return 0;
                            }
                            else
                            {
                                this.rpm.RPM = tempRPM;
                            }
                        }
                    }

                    return this.rpm.RPM;
                case Gear.Reverse:
                    if (this.breakPedalValue == 0)
                    {
                        int tempRPM = this.rpm.RPM + this.CalculateRPMChange();
                        if (tempRPM < MaxRPM)
                        {
                            if (tempRPM <= BaseRPM)
                            {
                                this.rpm.RPM = BaseRPM;
                            }
                            else
                            {
                                this.rpm.RPM = tempRPM;
                            }
                        }
                        else
                        {
                            this.rpm.RPM = MaxRPM;
                        }
                    }
                    else
                    {
                        int tempRPM = this.rpm.RPM + this.CalculateRPMChange();
                        if (tempRPM < MaxRPM)
                        {
                            if (tempRPM <= BaseRPM)
                            {
                                this.rpm.RPM = BaseRPM;
                                return 0;
                            }
                            else
                            {
                                this.rpm.RPM = tempRPM;
                            }
                        }
                    }

                    return (int)this.rpm.RPM;
                default:
                    return 0;
            }
        }

        public EngineRPM RPM { get => this.rpm; }

        public override void Process()
        {
            this.gasPedalValue = virtualFunctionBus.ReadonlyPedalPacket.GasPedal;
            this.breakPedalValue = virtualFunctionBus.ReadonlyPedalPacket.BrakePedal;
            this.CurrentGear = Gear.Reverse;
            int transmissionToRPM = this.UpdateRPMValue();
            enginePacket.EngineRPM = transmissionToRPM;
        }
    }
}
