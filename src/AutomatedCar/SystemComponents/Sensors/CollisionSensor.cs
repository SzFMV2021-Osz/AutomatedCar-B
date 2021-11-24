namespace AutomatedCar.SystemComponents.Sensors
{
    using Avalonia;
    using Models;
    using System;
    using System.Linq;
    using Avalonia.Media;
    using AutomatedCar.SystemComponents.Packets;
    using AutomatedCar.Models;

    public class CollisionSensor : SystemComponent
    {
        private CollisionSensorPacket collisionSensorPacket;

        public CollisionSensor(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.collisionSensorPacket = new CollisionSensorPacket();
            virtualFunctionBus.CollisionSensorPacket = collisionSensorPacket;
        }

        public override void Process()
        {
            AutomatedCar car = World.Instance.ControlledCar;

            foreach (WorldObject item in World.Instance.WorldObjects.Where(x => x.Collideable).ToList())
            {
                foreach (Point point in item.Geometries[0].Points)
                {
                    if (CarGeometry(car).FillContains(new Point(point.X + item.X, point.Y + item.Y)))
                    {
                        if (item.WorldObjectType is WorldObjectType.Car || item.WorldObjectType is WorldObjectType.Predestrian)
                        {
                            Console.WriteLine($"Collision: NPC");
                            this.collisionSensorPacket.CollisionType = CollisionType.NPC;
                        }
                        else
                        {
                            Console.WriteLine($"Collision: Object");
                            this.collisionSensorPacket.CollisionType = CollisionType.Object;
                        }
                    }
                }
            }
        }

        public PolylineGeometry CarGeometry(AutomatedCar car)
        {
            Points carPoints = ((PolylineGeometry)car.Geometry).Points;

            Points ret = new Points();

            for (int i = 0; i < carPoints.Count(); i++)
            {
                ret.Add(new Point(Math.Abs(carPoints[i].X + car.X), Math.Abs(carPoints[i].Y + car.Y)));
            }

            return new PolylineGeometry(ret, true);
        }
    }
}