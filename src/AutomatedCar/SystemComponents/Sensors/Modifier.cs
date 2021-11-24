// <copyright file="Modifier.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Sensors
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using AutomatedCar.Models;
    using Avalonia;
    using Avalonia.Media;
    using Newtonsoft.Json;

    /// <summary>
    /// Radar modification methods.
    /// </summary>
    internal class Modifier
    {
        /// <summary>
        /// The reference point of the car.
        /// </summary>
        public static readonly IList<CarRefPoint> CarReferencePoints = LoadReferencePoints();

        /// <summary>
        /// The method that returns the nearest object.
        /// </summary>
        /// <param name="worldObjects">List of world objects.</param>
        /// <param name="car">Current car object.</param>
        /// <returns>The nearest object.</returns>
        public static WorldObject FindNearestObject(IList<WorldObject> worldObjects, AutomatedCar car)
        {
            Point carPoint = new (car.X, car.Y);
            WorldObject nearestObject = null;
            List<Point> temp = new ();

            double minDistance = double.MaxValue;
            foreach (WorldObject currObject in worldObjects)
            {
                foreach (Point currPoint in GetObjectPoints(currObject))
                {
                    double currDistance = DistanceBetween(carPoint, currPoint);
                    if (currDistance < minDistance)
                    {
                        minDistance = currDistance;
                        nearestObject = currObject;
                    }

                    temp.Add(new Point(currObject.X, currDistance));
                }
            }

            return nearestObject;
        }

        /// <summary>
        /// A method for calculating the distance between two elements.
        /// </summary>
        /// <param name="from">An object from which to calculate the distance.</param>
        /// <param name="to">Object as long as we calculate the distance.</param>
        /// <returns>The distance between two elements.</returns>
        public static double DistanceBetween(Point from, Point to)
        {
            return Math.Sqrt(Math.Pow(from.X - to.X, 2) + Math.Pow(from.Y - to.Y, 2));
        }

        /// <summary>
        /// Returns the x and y points of an object in a Point object.
        /// </summary>
        /// <param name="worldObject">The input world object.</param>
        /// <returns>Point type coordinate.</returns>
        public static List<Point> GetObjectPoints(WorldObject worldObject)
        {
            List<Point> points = new () { new Point(worldObject.X, worldObject.Y) };

            Point refPoint = new (0, 0);
            if (CarReferencePoints.Any(r => r.Type + ".png" == worldObject.Filename))
            {
                CarRefPoint currRefPoint = CarReferencePoints.Where(r => r.Type + ".png" == worldObject.Filename).FirstOrDefault();
                refPoint = new (currRefPoint.X, currRefPoint.Y);
            }

            foreach (PolylineGeometry currGeometry in worldObject.Geometries)
            {
                foreach (Point currPoint in currGeometry.Points)
                {
                    points.Add(new Point(currPoint.X + worldObject.X - refPoint.X, currPoint.Y + worldObject.Y - refPoint.Y));
                }
            }

            return points;
        }

        /// <summary>
        /// A method of rotating points.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="rotationPoint">The center of rotation.</param>
        /// <param name="rotation">The degree of rotation.</param>
        /// <returns>Return a rotated point.</returns>
        public static Point RotatePoint(Point point, System.Drawing.Point rotationPoint, double rotation)
        {
            Point transformedPoint = new (point.X - rotationPoint.X, point.Y - rotationPoint.Y);

            double sin = Math.Sin(Modifier.ConvertToRadians(rotation));
            double cos = Math.Cos(Modifier.ConvertToRadians(rotation));
            Point rotatedPoint = new ((transformedPoint.X * cos) - (transformedPoint.Y * sin), (transformedPoint.X * sin) + (transformedPoint.Y * cos));

            return rotatedPoint;
        }

        /// <summary>
        /// Method of rotating geometry with a specified value.
        /// </summary>
        /// <param name="geometry">Input geometry.</param>
        /// <param name="rotationPoint">The center of rotation.</param>
        /// <param name="rotation">The degree of rotation.</param>
        /// <returns>Return a rotated geometry.</returns>
        public static PolylineGeometry RotateRawGeometry(PolylineGeometry geometry, System.Drawing.Point rotationPoint, double rotation)
        {
            List<Point> rotatedPoints = new ();
            foreach (Point point in geometry.Points)
            {
                rotatedPoints.Add(Modifier.RotatePoint(point, rotationPoint, rotation));
            }

            return new PolylineGeometry(rotatedPoints, true);
        }

        /// <summary>
        /// Geometry shift is a method to some extent.
        /// </summary>
        /// <param name="geometry">Input geometry.</param>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <returns>Modified geometry.</returns>
        public static PolylineGeometry ShiftGeometryWithWorldCoordinates(PolylineGeometry geometry, int x, int y)
        {
            Points shiftedPoints = new ();

            foreach (Point point in geometry.Points)
            {
                shiftedPoints.Add(new Point((int)(point.X + x), (int)(point.Y + y)));
            }

            return new PolylineGeometry(shiftedPoints, true);
        }

        /// <summary>
        /// Convert degree value to radius value.
        /// </summary>
        /// <param name="angle">Input degree value.</param>
        /// <returns>Output radius value.</returns>
        public static double ConvertToRadians(double angle)
        {
            return angle * (Math.PI / 180);
        }

        private static IList<CarRefPoint> LoadReferencePoints()
        {
            string jsonString = new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"AutomatedCar.Assets.reference_points.json")).ReadToEnd();
            return JsonConvert.DeserializeObject<List<CarRefPoint>>(jsonString);
        }
    }
}
