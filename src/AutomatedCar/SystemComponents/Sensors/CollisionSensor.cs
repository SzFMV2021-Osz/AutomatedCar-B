namespace AutomatedCar.SystemComponents.Sensors
{
    using Avalonia;
    using Models;
    using System;
    using System.Linq;
    using Avalonia.Media;
    using AutomatedCar.SystemComponents.Packets;
    using AutomatedCar.Models;
    using MsgBox;

    public class CollisionSensor : SystemComponent
    {
        private CollisionSensorPacket collisionSensorPacket;

        private int messageShown = 0;

        public CollisionSensor(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.collisionSensorPacket = new CollisionSensorPacket();
            virtualFunctionBus.CollisionSensorPacket = collisionSensorPacket;
        }

        public async override void Process()
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
                            if(messageShown == 0)
                            {
                                messageShown = 1;
                                await MessageBox.Show("Collision: NPC", "GameOver", MessageBox.MessageBoxButtons.Ok).ContinueWith(task => {messageShown = 0;});
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine($"Collision: Object");
                            this.collisionSensorPacket.CollisionType = CollisionType.Object;
                            if(messageShown == 0)
                            {
                                messageShown = 1;
                                await MessageBox.Show("Collision: Object", "GameOver", MessageBox.MessageBoxButtons.Ok).ContinueWith(task => {messageShown = 0;});
                            }
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