// <copyright file="Pedals.cs" company="PlaceholderCompany">
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

    /// <summary>
    /// The behaviour of pedals.
    /// </summary>
    public class Pedals : SystemComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pedals"/> class.
        /// </summary>
        /// <param name="virtualFunctionBus">The virtual function bus.</param>
        public Pedals(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.PedalPacket = new PedalPacket();
            virtualFunctionBus.ReadonlyPedalPacket = this.PedalPacket;
        }

        private PedalPacket PedalPacket { get; }

        /// <summary>
        /// Gets the gas pedal.
        /// </summary>
        public Pedal Gas { get; } = new Pedal();

        /// <summary>
        /// Gets the gas pedal.
        /// </summary>
        public Pedal Brake { get; } = new Pedal();

        /// <inheritdoc/>
        public override void Process()
        {
            this.PedalPacket.BrakePedal = Convert.ToInt32(Math.Round(this.Brake.Position / Pedal.MaxPedalPosition * 100));
            this.PedalPacket.GasPedal = Convert.ToInt32(Math.Round(this.Gas.Position / Pedal.MaxPedalPosition * 100));
        }


    }
}
