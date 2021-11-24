// <copyright file="Camera.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Sensors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using Avalonia;

    public class Camera
    {
        private int distance;
        private int angle;
        private CameraPacket camPacket;

        public Point RelativeLocation { get; set; }

        public Camera(VirtualFunctionBus virtualFunctionBus, int angle = 60, int distance = 80)
        {
            this.camPacket = new CameraPacket();
            virtualFunctionBus.CameraPacket = this.camPacket;

            this.angle = angle;
            this.distance = distance;
        }

        public IList<Point> Points
        {
            get => this.DrawTriangle();
        }

        private IList<Avalonia.Point> DrawTriangle()
        {
            int triangleBase = (int)(this.distance * Math.Tan((this.angle / 2) * (Math.PI / 180)));
            List<Point> points = new ()
            {
                new Point(0 + this.RelativeLocation.X, 0 + this.RelativeLocation.Y),
                new Point(-triangleBase + this.RelativeLocation.X, -this.distance + this.RelativeLocation.Y),
                new Point(triangleBase + this.RelativeLocation.X, -this.distance + this.RelativeLocation.Y),
                new Point(0 + this.RelativeLocation.X, 0 + this.RelativeLocation.Y),
            };

            return points;
        }

        private void FilterRoads(IList<WorldObject> worldObjects)
        {
            this.camPacket.Roads = worldObjects.Where(ro => ro.WorldObjectType == WorldObjectType.Road).ToList();
        }
    }
}
