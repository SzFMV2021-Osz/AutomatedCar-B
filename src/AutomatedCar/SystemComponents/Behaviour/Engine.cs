// <copyright file="Engine.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Behaviour
{
    using System;
    using AutomatedCar.SystemComponents.Packets;
    using ReactiveUI;

    public class Engine : SystemComponent
    {
        /// <summary>
        /// The max allowed RPM.
        /// </summary>
        private const int MaxRPM = 7000;

        private const double GasPedalScaling = 0.2;
        private const double BrakePedalScaling = 5;
        private const int RPMReduction = -10;

        private int rpm;
        private EnginePacket enginePacket;

        /// <summary>
        /// Gets the RPM of the engine.
        /// </summary>
        public int RPM { get => this.rpm; private set => this.RaiseAndSetIfChanged(ref this.rpm, value); }

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="virtualFunctionBus">The VFB.</param>
        public Engine(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.enginePacket = new EnginePacket();
            virtualFunctionBus.ReadonlyEnginePacket = this.enginePacket;
        }

        /// <inheritdoc/>
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
        }

        /// <summary>
        /// Updates the RPM value based on the pedals values.
        /// </summary>
        private void UpdateRPM()
        {
            int deltaRPM = 0;

            if (this.virtualFunctionBus.ReadonlyPedalPacket.GasPedal == 0)
            {
                deltaRPM += RPMReduction;
            }

            deltaRPM += (int)(this.virtualFunctionBus.ReadonlyPedalPacket.GasPedal * GasPedalScaling);
            deltaRPM -= (int)(this.virtualFunctionBus.ReadonlyPedalPacket.BrakePedal * BrakePedalScaling);

            this.RPM = Math.Min(MaxRPM, Math.Max(0, this.RPM + deltaRPM));
        }

        /// <summary>
        /// Called when the gearbox drive subgear decreases.
        /// </summary>
        private void OnShiftDown()
        {
            this.RPM = AutomaticGearbox.MaxMotorRevolution;
        }

        /// <summary>
        /// Called when the gearbox drive subgear increases.
        /// </summary>
        private void OnShiftUp()
        {
            this.RPM = AutomaticGearbox.MinMotorRevolution;
        }
    }
}
