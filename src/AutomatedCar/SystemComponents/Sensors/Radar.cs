// <copyright file="Radar.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Sensors
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using Avalonia;
    using Avalonia.Media;

    /// <summary>
    /// Radar class.
    /// </summary>
    public class Radar : SystemComponent
    {
        private readonly int distance;
        private readonly int angleOfView;
        private readonly Dictionary<int, double> previousObjects;
        private PolylineGeometry laneGeometry;
        private RadarPacket radarPacket;
        private WorldObject radarObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="Radar"/> class.
        /// </summary>
        /// <param name="virtualFunctionBus">VirtualFunctionBus specified by dependency injection.</param>
        public Radar(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.previousObjects = new Dictionary<int, double>();
            this.radarPacket = new RadarPacket();
            virtualFunctionBus.RadarPacket = (IRadarPacket)this.radarPacket;
            this.distance = 200 * 53; // 240 px ~= 4,5 m ==> * 53
            this.angleOfView = 60; // 60° angle of view
        }

        /// <summary>
        /// Gets or sets relative radar location.
        /// </summary>
        public Point RelativeLocation { get; set; }

        /// <summary>
        /// Gets radar points.
        /// </summary>
        public IList<Point> Points { get => this.DrawTriangle(); }

        /// <summary>
        /// Radar process method.
        /// </summary>
        public override void Process()
        {
            AutomatedCar car = World.Instance.ControlledCar;

            this.CalculateRadarTriangleData(car, World.Instance.WorldObjects);
            this.CalculateRadarData(car);
        }

        /// <summary>
        /// Examining the relevance of the object.
        /// </summary>
        /// <param name="worldObject">World Object input.</param>
        /// <returns>The relevance value of the input object.</returns>
        protected bool IsRelevant(WorldObject worldObject)
        {
            return worldObject.Collideable;
        }

        private IList<Point> DrawTriangle()
        {
            int triangleBase = (int)(this.distance * Math.Tan(Modifier.ConvertToRadians(this.angleOfView / 2)));
            List<Point> points = new ()
            {
                new Point(0 + this.RelativeLocation.X, 0 + this.RelativeLocation.Y),
                new Point(-triangleBase + this.RelativeLocation.X, -this.distance + this.RelativeLocation.Y),
                new Point(triangleBase + this.RelativeLocation.X, -this.distance + this.RelativeLocation.Y),
                new Point(0 + this.RelativeLocation.X, 0 + this.RelativeLocation.Y),
            };

            return points;
        }

        private void CalculateRadarData(AutomatedCar car)
        {
            this.CreateLaneGeometry();

            Dictionary<int, Point> permanentObjectsInRadar = this.GetPermanentObjectsInRadar();
            IList<WorldObject> closingObjects = this.GetClosingObjects(permanentObjectsInRadar, car);

            ((IRadarPacket)this.radarPacket).NearingObjects = closingObjects;
            ((IRadarPacket)this.radarPacket).NearestObjectInLane = this.GetClosestObjectInLane(closingObjects, car);
            this.SavePreviousObjectDistances(car);
        }

        private void SavePreviousObjectDistances(AutomatedCar car)
        {
            this.previousObjects.Clear();

            foreach (WorldObject currObj in this.radarPacket.RelevantObjects)
            {
                this.previousObjects.Add(currObj.Id, Modifier.DistanceBetween(new Point(currObj.X, currObj.Y), new Point(car.X, car.Y)));
            }
        }

        private WorldObject GetClosestObjectInLane(IList<WorldObject> worldObjects, AutomatedCar car)
        {
            IList<WorldObject> incomingObjectsInLane = worldObjects.Where(c => this.IsObjectInLane(c)).ToList();
            return Modifier.FindNearestObject(incomingObjectsInLane, car);
        }

        private bool IsObjectInLane(WorldObject currObject)
        {
            PolylineGeometry geometry = Modifier.RotateRawGeometry(this.laneGeometry, this.radarObject.RotationPoint, this.radarObject.Rotation);
            geometry = Modifier.ShiftGeometryWithWorldCoordinates(geometry, this.radarObject.X, this.radarObject.Y);

            foreach (var point in Modifier.GetObjectPoints(currObject))
            {
                if (geometry.FillContains(point))
                {
                    return true;
                }
            }

            return false;
        }

        private IList<WorldObject> GetClosingObjects(Dictionary<int, Point> objectsInRadar, AutomatedCar car)
        {
            IList<WorldObject> closingObjects = new List<WorldObject>();
            foreach (var currPoint in objectsInRadar)
            {
                double currDst = Modifier.DistanceBetween(currPoint.Value, new Point(car.X, car.Y));
                if (currDst < this.previousObjects[currPoint.Key])
                {
                    closingObjects.Add(this.radarPacket.RelevantObjects.Where(d => d.Id == currPoint.Key).FirstOrDefault());
                }
            }

            return closingObjects;
        }

        private Dictionary<int, Point> GetPermanentObjectsInRadar()
        {
            Dictionary<int, Point> permanentObjects = new ();
            foreach (WorldObject currObj in this.radarPacket.RelevantObjects)
            {
                if (this.previousObjects.ContainsKey(currObj.Id))
                {
                    permanentObjects.Add(currObj.Id, new Point(currObj.X, currObj.Y));
                }
            }

            return permanentObjects;
        }

        private void CreateLaneGeometry()
        {
            if (this.laneGeometry == null)
            {
                this.laneGeometry = this.GetSelfLaneGeometry();
            }
        }

        private PolylineGeometry GetSelfLaneGeometry()
        {
            IList<Point> points = new List<Point>()
            {
                new Point(0, this.radarObject.RotationPoint.Y),
                new Point(2 * this.radarObject.RotationPoint.X, this.radarObject.RotationPoint.Y),
                new Point(2 * this.radarObject.RotationPoint.X, this.radarObject.RotationPoint.Y - this.distance),
                new Point(0, this.radarObject.RotationPoint.Y - this.distance),
                new Point(0, this.radarObject.RotationPoint.Y),
            };

            return new PolylineGeometry(points, true);
        }

        private void CalculateRadarTriangleData(AutomatedCar car, ObservableCollection<WorldObject> worldObjects)
        {
            this.SetTriangle(car);
            this.FindObjectsInSensorArea(worldObjects);
            this.FilterRelevantObjects();
            this.radarPacket.NearestObject = Modifier.FindNearestObject(this.radarPacket.RelevantObjects, car);
        }

        private void FilterRelevantObjects()
        {
            this.radarPacket.RelevantObjects = this.radarPacket.DetectedObjects.Where(wo => this.IsRelevant(wo)).ToList();
        }

        private void FindObjectsInSensorArea(ObservableCollection<WorldObject> worldObjects)
        {
            List<WorldObject> detectedObjects = new ();

            foreach (WorldObject currObject in worldObjects)
            {
                foreach (Point point in Modifier.GetObjectPoints(currObject))
                {
                    if (this.radarObject.Geometries[0].FillContains(point) && !detectedObjects.Contains(currObject))
                    {
                        detectedObjects.Add(currObject);
                    }
                }
            }

            this.radarPacket.DetectedObjects = detectedObjects;
        }

        private void SetTriangle(AutomatedCar car)
        {
            int triangleBase = (int)(this.distance * Math.Tan(Modifier.ConvertToRadians(this.angleOfView / 2)));

            if (this.radarObject == null)
            {
                this.radarObject = new WorldObject(car.X + car.RotationPoint.X, car.Y + car.RotationPoint.Y, "sensor.png");
                this.radarObject.RawGeometries.Add(this.GetRawGeometry(triangleBase, this.distance));
                this.radarObject.Collideable = false;
                this.radarObject.Geometries.Add(this.GetGeometry());
            }
            else
            {
                this.radarObject.X = car.X + car.RotationPoint.X;
                this.radarObject.Y = car.Y + car.RotationPoint.Y;
                this.radarObject.Geometries[0] = this.GetGeometry();
            }

            this.radarObject.RotationPoint = new (triangleBase, this.distance + car.RotationPoint.Y - (int)this.RelativeLocation.Y);
            this.radarObject.Rotation = car.Rotation;
        }

        private PolylineGeometry GetGeometry()
        {
            PolylineGeometry geometry = Modifier.RotateRawGeometry(this.radarObject.RawGeometries[0], this.radarObject.RotationPoint, this.radarObject.Rotation);

            return Modifier.ShiftGeometryWithWorldCoordinates(geometry, this.radarObject.X, this.radarObject.Y);
        }

        private PolylineGeometry GetRawGeometry(int triangleBase, int distance)
        {
            List<Point> points = new ()
            {
                new Point(0, 0),
                new Point(2 * triangleBase, 0),
                new Point(triangleBase, distance),
                new Point(0, 0),
            };

            return new PolylineGeometry(points, true);
        }
    }
}
