// <copyright file="RadarPacket.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Packets
{
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using ReactiveUI;

    /// <summary>
    /// Radar type sensor class.
    /// </summary>
    public class RadarPacket : ReactiveObject, IRadarPacket
    {
        private IList<WorldObject> detectedObjects;
        private IList<WorldObject> relevantObjects;
        private WorldObject nearestObject;
        private IList<WorldObject> approachingObjects;
        private WorldObject nearestObjectInLine;

        /// <summary>
        /// Initializes a new instance of the <see cref="RadarPacket"/> class.
        /// </summary>
        public RadarPacket()
        {
            this.detectedObjects = new List<WorldObject>();
            this.relevantObjects = new List<WorldObject>();
            this.approachingObjects = new List<WorldObject>();
        }

        /// <summary>
        /// Gets or sets list of detected objects.
        /// </summary>
        public IList<WorldObject> DetectedObjects
        {
            get => this.detectedObjects;
            set => this.RaiseAndSetIfChanged(ref this.detectedObjects, value);
        }

        /// <summary>
        /// Gets or sets a list of relevant objects.
        /// </summary>
        public IList<WorldObject> RelevantObjects
        {
            get => this.relevantObjects;
            set => this.RaiseAndSetIfChanged(ref this.relevantObjects, value);
        }

        /// <summary>
        /// Gets or sets to the nearest object.
        /// </summary>
        public WorldObject NearestObject
        {
            get => this.nearestObject;
            set => this.RaiseAndSetIfChanged(ref this.nearestObject, value);
        }

        /// <summary>
        /// Gets or sets a list of the nearest objects.
        /// </summary>
        public IList<WorldObject> NearingObjects
        {
            get => this.approachingObjects;
            set => this.RaiseAndSetIfChanged(ref this.approachingObjects, value);
        }

        /// <summary>
        /// Gets or sets a nearest object in lane.
        /// </summary>
        public WorldObject NearestObjectInLane
        {
            get => this.nearestObjectInLine;
            set => this.RaiseAndSetIfChanged(ref this.nearestObjectInLine, value);
        }
    }
}
