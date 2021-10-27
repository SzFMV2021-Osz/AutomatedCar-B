using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Behaviour;
using Xunit;

namespace AutomatedCarTests.Models
{
    public class SteeringTests
    {
        [Theory]
        [InlineData(10, 10, "WorldObjects/car_1_blue.png", 2, 2)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 0, 0)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 20, 20)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 100, 100)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 101, 100)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 200, 100)]
        public void SteeringWheelRotationRight(int x, int y, string filename, int rotation, int expected)
        {
            AutomatedCar.Models.AutomatedCar AutCar = new AutomatedCar.Models.AutomatedCar(x, y, filename);
            for (int i = 0; i < rotation; i++)
            {
                AutCar.Steering.RotateSteeringWheelRight();
                AutCar.Steering.Process();
            }
            Assert.Equal(expected, AutCar.VirtualFunctionBus.SteeringPacket.WheelPosition);
        }

        [Theory]
        [InlineData(10, 10, "WorldObjects/car_1_blue.png", 2, -2)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 0, 0)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 20, -20)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 100, -100)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 101, -100)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 200, -100)]
        public void SteeringWheelRotationLeft(int x, int y, string filename, int rotation, int expected)
        {
            AutomatedCar.Models.AutomatedCar AutCar = new AutomatedCar.Models.AutomatedCar(x, y, filename);
            for (int i = 0; i < rotation; i++)
            {
                AutCar.Steering.RotateSteeringWheelLeft();
                AutCar.Steering.Process();
            }
            Assert.Equal(expected, AutCar.VirtualFunctionBus.SteeringPacket.WheelPosition);
        }

        [Theory]
        [InlineData(10, 10, "WorldObjects/car_1_blue.png", 2)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 0)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 20)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 100)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 101)]
        [InlineData(200, 400, "WorldObjects/car_1_blue.png", 200)]
        public void SteeringWheelRelease(int x, int y, string filename, int rotation)
        {
            AutomatedCar.Models.AutomatedCar AutCar = new AutomatedCar.Models.AutomatedCar(x, y, filename);
            for (int i = 0; i < rotation; i++)
            {
                AutCar.Steering.RotateSteeringWheelLeft();
                AutCar.Steering.Process();
            }
            for (int i = 0; i < rotation; i++)
            {
                AutCar.Steering.ReleaseSteeringWheel();
                AutCar.Steering.Process();
            }
            Assert.Equal(0, AutCar.VirtualFunctionBus.SteeringPacket.WheelPosition);
        }
    }
}