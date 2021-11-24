// <copyright file="IRadarPacket.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Packets
{
    using System.Collections.Generic;
    using AutomatedCar.Models;

    /// <summary>
    /// Definition of radar type sensor class.
    /// </summary>
    public interface IRadarPacket
    {
        /// <summary>
        /// Gets or sets definition of a generic list of detected objects.
        /// </summary>
        public IList<WorldObject> DetectedObjects { get; set; }

        /// <summary>
        /// Gets or sets definition of relevant objects.
        /// </summary>
        public IList<WorldObject> RelevantObjects { get; set; }

        /// <summary>
        /// Gets or sets The definition of the nearest objects.
        /// </summary>
        public IList<WorldObject> NearingObjects { get; set; }

        /// <summary>
        /// Gets or sets is the definition of the nearest object.
        /// </summary>
        public WorldObject NearestObject { get; set; }

        /// <summary>
        /// Gets or sets is the definition of the nearest object in the line.
        /// </summary>
        public WorldObject NearestObjectInLane { get; set; }
    }
}