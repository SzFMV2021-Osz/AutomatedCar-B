// <copyright file="IGearbox.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Behaviour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Describes the behaviour of a car's gearbox.
    /// </summary>
    public interface IGearbox
    {
        /// <summary>
        /// Gets or sets the current gear.
        /// </summary>
        Gear Gear { get; set; }

        /// <summary>
        /// Gets or sets the gear in drive mode.
        /// </summary>
        int DriveSubgear { get; set; }

        /// <summary>
        /// Shifts up one gear.
        /// </summary>
        void ShiftUp();

        /// <summary>
        /// Shifts down one gear.
        /// </summary>
        void ShiftDown();

        /// <summary>
        /// Calculates the velocity multiplier of the gearbox.
        /// </summary>
        /// <returns>The multiplier of the gearbox.</returns>
        double GetGearboxTorgue();

         /// <summary>
        /// Shifts up one main gear.
        /// </summary>
        void ManualShiftUp();

        /// <summary>
        /// Shifts down one main gear.
        /// </summary>
        void ManualShiftDown();
    }
}
