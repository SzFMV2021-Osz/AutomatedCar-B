// <copyright file="Pedal.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Behaviour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ReactiveUI;

    /// <summary>
    /// The behaviour of a pedal.
    /// </summary>
    public class Pedal : ReactiveObject
    {
        /// <summary>
        /// The maximum position of a pedal.
        /// </summary>
        public const double MaxPedalPosition = 100;
        private double position;

        /// <summary>
        /// Gets or sets the position of the pedal (0-100).
        /// </summary>
        public double Position { get => this.position; set => this.RaiseAndSetIfChanged(ref this.position, value); }

        /// <summary>
        /// Increases the pedal's position.
        /// </summary>
        /// <param name="incValue">The value to increase the pedal's position by.</param>
        public void Increase(double incValue = 1)
        {
            this.Position = Math.Min(this.Position + incValue, MaxPedalPosition);
        }

        /// <summary>
        /// Decrease the pedal's position.
        /// </summary>
        /// <param name="decValue">The value to decrease the pedal's position by.</param>
        public void Decrease(double decValue = 1)
        {
            this.Position = Math.Max(this.Position - decValue, 0);
        }
    }
}
