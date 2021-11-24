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
        private IList<WorldObject> objectsInArea;
        private WorldObject closestObject;

        public CameraPacket()
        {
            this.Roads = new List<WorldObject>();
        }

        public IList<WorldObject> Roads
        {
            get => this.roads;
            set => this.RaiseAndSetIfChanged(ref this.roads, value);
        }

        public IList<WorldObject> ObjectsInArea
        {
            get => this.objectsInArea;
            set => this.RaiseAndSetIfChanged(ref this.objectsInArea, value);
        }

        public WorldObject ClosestObject
        {
            get => this.closestObject;
            set => this.RaiseAndSetIfChanged(ref this.closestObject, value);
        }
    }
}
