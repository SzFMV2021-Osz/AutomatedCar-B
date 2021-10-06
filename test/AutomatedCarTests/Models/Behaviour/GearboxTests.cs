namespace Tests.Models.Behaviour
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Behaviour;

    [TestFixture]
    class GearboxTests
    {
        [TestCase]
        public void TestCarInParkByDefault()
        {
            AutomatedCar car = new AutomatedCar(0, 0, "");
            Assert.That(car.Gearbox.CurrentGear, Is.EqualTo(Gear.Park));
        }

        [TestCase]
        public void TestCarInSubgearZeroByDefault()
        {
            AutomatedCar car = new AutomatedCar(0, 0, "");
            Assert.That(car.Gearbox.DriveSubgear, Is.EqualTo(0));
        }

        [TestCase(Gear.Park, Gear.Reverse)]
        [TestCase(Gear.Reverse, Gear.Neutral)]
        [TestCase(Gear.Neutral, Gear.Drive)]
        public void TestGearshiftUp(Gear origGear, Gear expectedGear)
        {
            AutomatedCar car = new AutomatedCar(0, 0, "");
            car.Gearbox.CurrentGear = origGear;
            car.Gearbox.ShiftUp();
            Assert.That(car.Gearbox.CurrentGear, Is.EqualTo(expectedGear));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void TestShiftUpInDrive(int origSubgear)
        {
            AutomatedCar car = new AutomatedCar(0, 0, "");
            car.Gearbox.CurrentGear = Gear.Drive;
            car.Gearbox.DriveSubgear = origSubgear;
            car.Gearbox.ShiftUp();
            Assert.That(car.Gearbox.CurrentGear, Is.EqualTo(Gear.Drive));
            Assert.That(car.Gearbox.DriveSubgear, Is.EqualTo(origSubgear + 1));
        }

        [TestCase]
        public void TestShiftUpStaysBelowMaxGear()
        {
            AutomatedCar car = new AutomatedCar(0, 0, "");

            for(int i = 0; i < 100; i++)
                car.Gearbox.ShiftUp();

            Assert.That(car.Gearbox.DriveSubgear, Is.LessThanOrEqualTo(AutomaticGearbox.MaxDriveSubgears));
        }

        [TestCase(Gear.Reverse, Gear.Park)]
        [TestCase(Gear.Neutral, Gear.Reverse)]
        [TestCase(Gear.Drive, Gear.Neutral)]
        [TestCase(Gear.Park, Gear.Park)]
        public void TestGearshiftDown(Gear origGear, Gear expectedGear)
        {
            AutomatedCar car = new AutomatedCar(0, 0, "");
            car.Gearbox.CurrentGear = origGear;
            car.Gearbox.ShiftDown();
            Assert.That(car.Gearbox.CurrentGear, Is.EqualTo(expectedGear));
        }

        [TestCase]
        public void TestShiftUpStaysAboveZero()
        {
            AutomatedCar car = new AutomatedCar(0, 0, "");

            for (int i = 0; i < 100; i++)
                car.Gearbox.ShiftDown();

            Assert.That(car.Gearbox.DriveSubgear, Is.GreaterThanOrEqualTo(0));
        }
    }
}
