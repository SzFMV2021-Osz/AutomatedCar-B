// <copyright file="Pedals.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Behaviour
{
    using AutomatedCar.SystemComponents.Packets;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
            this.pedalPacket = new PedalPacket();
            virtualFunctionBus.ReadonlyPedalPacket = this.pedalPacket;
        }

        private PedalPacket pedalPacket {get; }

        /// <summary>
        /// Gets the gas pedal.
        /// </summary>
        public Pedal Gas { get; } = new Pedal();

        /// <summary>
        /// Gets the gas pedal.
        /// </summary>
        public Pedal Break { get; } = new Pedal();

        /// <inheritdoc/>
        public override void Process()
        {
            this.pedalPacket.BrakePedal = Convert.ToInt32(Math.Round(this.Break.Position / Pedal.MaxPedalPosition * 100));
            this.pedalPacket.GasPedal = Convert.ToInt32(Math.Round(this.Gas.Position / Pedal.MaxPedalPosition * 100));
        }


    }
}
