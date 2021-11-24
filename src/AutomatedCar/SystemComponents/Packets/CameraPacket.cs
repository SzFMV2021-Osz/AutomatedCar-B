// <copyright file="CameraPacket.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Packets
{
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using ReactiveUI;

    public class CameraPacket : ReactiveObject
    {
        private IList<WorldObject> roads;

        public CameraPacket()
        {
            this.Roads = new List<WorldObject>();
        }

        public IList<WorldObject> Roads
        {
            get => this.roads;
            set => this.RaiseAndSetIfChanged(ref this.roads, value);
        }
    }
}
