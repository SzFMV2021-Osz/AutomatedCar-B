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

    public class Camera : SystemComponent
    {
        private int distance;
        private int angle;
        private CameraPacket camPacket;

        public Point RelativeLocation { get; set; }

        public Camera(VirtualFunctionBus virtualFunctionBus, int angle = 60, int distance = 80)
            : base(virtualFunctionBus)
        {
            this.camPacket = new CameraPacket();
            virtualFunctionBus.CameraPacket = this.camPacket;

            this.angle = angle;
            this.distance = distance * 10; // 240 px ~= 4,5 m ==> * 53
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

        private void GetObjectsInTriangle()
        {
            Point carPoint = new (World.Instance.ControlledCar.X, World.Instance.ControlledCar.Y);
            foreach (var item in World.Instance.WorldObjects)
            {
                if (Math.Sqrt(Math.Pow(carPoint.X - item.X, 2) + Math.Pow(carPoint.Y - item.Y, 2)) <= this.distance)
                {
                    this.camPacket.ObjectsInArea.Add(item);
                }
            }
        }

        private void FilterRoads()
        {
            this.camPacket.Roads = this.camPacket.ObjectsInArea.Where(ro => ro.WorldObjectType == WorldObjectType.Road).ToList();
        }

        private void GetClosest()
        {
            Point carPoint = new (World.Instance.ControlledCar.X, World.Instance.ControlledCar.Y);
            WorldObject closestObject = null;
            double minDistance = double.MaxValue;
            IList<WorldObject> worldObjects = this.camPacket.ObjectsInArea.ToList();
            foreach (WorldObject currObject in worldObjects)
            {
                double currDistance = Math.Sqrt(Math.Pow(carPoint.X - currObject.X, 2) + Math.Pow(carPoint.Y - currObject.Y, 2));
                if (currDistance < minDistance && currDistance != 0)
                {
                    minDistance = currDistance;
                    closestObject = currObject;
                }
            }

            this.camPacket.ClosestObject = closestObject;
            this.camPacket.ClosestObject.Highlighted = true;
        }

        public override void Process()
        {
            this.GetObjectsInTriangle();
            this.FilterRoads();
            this.GetClosest();
        }
    }
}