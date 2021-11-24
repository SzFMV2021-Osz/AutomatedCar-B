// <copyright file="CarRefPoint.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Sensors
{
    /// <summary>
    /// Car reference point storage.
    /// </summary>
    internal class CarRefPoint
    {
        /// <summary>
        /// Gets or sets point type container.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets coordinate of point X.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets coordinate of point Y.
        /// </summary>
        public int Y { get; set; }
    }
}
