// <copyright file="IGearboxPacket.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Packets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutomatedCar.SystemComponents.Behaviour;

    /// <summary>
    /// A packet updated by the gearbox each run.
    /// </summary>
    public interface IReadonlyGearboxPacket
    {
        /// <summary>
        /// Gets the torque applied by the motor and the current gear.
        /// </summary>
        double Torque { get; }

        /// <summary>
        /// Gets the direction of the current gearshift. -1 if down, +1 if up, otherwise 0.
        /// </summary>
        public short ShiftDirection { get; }

        /// <summary>
        /// Gets the current gear.
        /// </summary>
        public Gear CurrentGear { get; }
    }
}
