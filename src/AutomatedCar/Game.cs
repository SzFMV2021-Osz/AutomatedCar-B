namespace AutomatedCar
{
    using System;
    using AutomatedCar.Models;
    using Avalonia.Input;

    public class Game : GameBase
    {
        private readonly World world;

        public Game(World world)
        {
            this.world = world;
        }

        public World World { get => this.world; }

        private Random Random { get; } = new Random();

        protected override void Tick()
        {
            if (Keyboard.IsKeyDown(Key.Up))
            {
                World.Instance.ControlledCar.Pedals.Gas.Increase(3);
            }
            else
            {
                World.Instance.ControlledCar.Pedals.Gas.Decrease(4);
            }

            if (Keyboard.IsKeyDown(Key.Down))
            {
                World.Instance.ControlledCar.Pedals.Brake.Increase(3);
            }
            else
            {
                World.Instance.ControlledCar.Pedals.Brake.Decrease(4);
            }


            if (Keyboard.IsKeyDown(Key.Left))
            {
                World.Instance.ControlledCar.Steering.RotateSteeringWheelLeft();
            }

            if (Keyboard.IsKeyDown(Key.Right))
            {
                World.Instance.ControlledCar.Steering.RotateSteeringWheelRight();
            }

            if (!Keyboard.IsKeyDown(Key.Left) && !Keyboard.IsKeyDown(Key.Right))
            {
                World.Instance.ControlledCar.Steering.ReleaseSteeringWheel();
            }

            if (Keyboard.IsKeyDown(Key.PageUp))
            {
                this.world.ControlledCar.Rotation += 5;
            }

            if (Keyboard.IsKeyDown(Key.PageDown))
            {
                this.world.ControlledCar.Rotation -= 5;
            }

            if (Keyboard.IsKeyDown(Key.D1))
            {
                this.world.DebugStatus.Enabled = !this.world.DebugStatus.Enabled;
            }

            if (Keyboard.IsKeyDown(Key.D2))
            {
                this.world.DebugStatus.Camera = !this.world.DebugStatus.Camera;
            }

            if (Keyboard.IsKeyDown(Key.D3))
            {
                this.world.DebugStatus.Radar = !this.world.DebugStatus.Radar;
            }

            if (Keyboard.IsKeyDown(Key.D4))
            {
                this.world.DebugStatus.Ultrasonic = !this.world.DebugStatus.Ultrasonic;
            }

            if (Keyboard.IsKeyDown(Key.D5))
            {
                this.world.DebugStatus.Rotate = !this.world.DebugStatus.Rotate;
            }

            this.world.ControlledCar.CalculateNextPosition();
        }
    }
}