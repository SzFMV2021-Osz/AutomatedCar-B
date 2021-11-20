// <copyright file="Engine.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Behaviour
{
    using AutomatedCar.SystemComponents.Packets;
    using ReactiveUI;
    using System;

    public class Engine : SystemComponent
    {
        /// <summary>
        /// The max allowed RPM.
        /// </summary>
        private const int MaxRPM = 7000;

        private const double PedalScaling = 0.2;
        private const int RPMReduction = -10;

        private int rpm;
        private EnginePacket enginePacket;

        /// <summary>
        /// Gets the RPM of the engine.
        /// </summary>
        public int RPM { get => this.rpm; private set => this.RaiseAndSetIfChanged(ref this.rpm, value); }

        public Engine(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.enginePacket = new EnginePacket();
            virtualFunctionBus.ReadonlyEnginePacket = this.enginePacket;
        }

        private void UpdateRPM()
        {
            int deltaRPM = 0;

            if (this.virtualFunctionBus.ReadonlyPedalPacket.GasPedal == 0)
            {
                deltaRPM += RPMReduction;
            }

            deltaRPM += (int)(this.virtualFunctionBus.ReadonlyPedalPacket.GasPedal * PedalScaling);
            deltaRPM -= (int)(this.virtualFunctionBus.ReadonlyPedalPacket.BrakePedal * PedalScaling);

            this.RPM = Math.Min(MaxRPM, Math.Max(0, this.RPM + deltaRPM));
            /*
            if (this.virtualFunctionBus.ReadonlyPedalPacket.GasPedal != 0)
            {
                if (this.breakPedalValue == 0)
                {
                    return (int)(this.virtualFunctionBus.ReadonlyPedalPacket.GasPedal * PedalScaling);
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
            */
        }
        /*
        private int UpdateRPMValue()
        {
            int tempRPM;

            switch (this.currentGear)
            {
                case Gear.Park:
                case Gear.Neutral:
                    tempRPM = this.RPM + this.CalculateRPMChange();
                    if (tempRPM <= BaseRPM)
                    {
                        this.RPM = BaseRPM;
                    }
                    else if (tempRPM >= MaxRPM)
                    {
                        this.RPM = MaxRPM;
                    }
                    else
                    {
                        this.RPM = tempRPM;
                    }

                    return 0;
                case Gear.Drive:
                case Gear.Reverse:
                    tempRPM = this.rpm.RPM + this.CalculateRPMChange();
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

                        return (int)this.rpm.RPM;
                    }

                    this.rpm.RPM = MaxRPM;

                    return (int)this.rpm.RPM;
                default:
                    return 0;
            }
        }
        */

        private void OnShiftDown()
        {
            this.RPM = AutomaticGearbox.MaxMotorRevolution;
        }

        private void OnShiftUp()
        {
            this.RPM = AutomaticGearbox.MinMotorRevolution;
        }

        public override void Process()
        {
            if (this.virtualFunctionBus.ReadonlyGearboxPacket.ShiftDirection == -1)
            {
                this.OnShiftDown();
            }
            else if (this.virtualFunctionBus.ReadonlyGearboxPacket.ShiftDirection == 1)
            {
                this.OnShiftUp();
            }

            this.UpdateRPM();

            this.enginePacket.EngineRPM = this.RPM;

            //int transmissionToRPM = this.UpdateRPMValue();
            //enginePacket.EngineRPM = transmissionToRPM;
        }
    }
}
