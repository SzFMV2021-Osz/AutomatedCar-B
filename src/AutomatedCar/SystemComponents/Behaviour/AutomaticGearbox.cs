// <copyright file="AutomaticGearbox.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Behaviour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutomatedCar.SystemComponents.Packets;
    using global::AutomatedCar.SystemComponents;

    /// <summary>
    /// An automated gearbox implementation.
    /// </summary>
    public class AutomaticGearbox : SystemComponent, IGearbox
    {
        /// <summary>
        /// The maximum numbers of the drive subgears.
        /// </summary>
        public const int MaxDriveSubgears = 5;

        /// <summary>
        /// The maximum revolution of the motor, when a gearshift should happen.
        /// </summary>
        public const int MaxMotorRevolution = 800;

        /// <summary>
        /// The maximum revolution of the motor, when a gearshift should happen.
        /// </summary>
        public const int MinMotorRevolution = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutomaticGearbox"/> class.
        /// </summary>
        /// <param name="virtualFunctionBus">The car's virtual function bus.</param>
        public AutomaticGearbox(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.Gear = Gear.Park;
            this.DriveSubgear = 0;
            virtualFunctionBus.GearboxPacket = new GearboxPacket();
        }

        /// <inheritdoc/>
        public Gear Gear { get; set; }

        /// <inheritdoc/>
        public int DriveSubgear { get; set; }

        /// <inheritdoc/>
        public double GetGearboxTorgue()
        {
            // TODO use actual motor rev
            int motorRev = 300;

            switch (this.Gear)
            {
                case Gear.Park:
                    return 0;
                case Gear.Reverse:
                    return -1;
                case Gear.Neutral:
                    return 1;
                case Gear.Drive:
                    // TODO - make this make sense
                    return (this.DriveSubgear * MaxMotorRevolution) + motorRev;
            }

            throw new Exception("Invalid gear");
        }

        /// <inheritdoc/>
        public override void Process()
        {
            this.HandleAutomaticGearshift();
            this.virtualFunctionBus.GearboxPacket.Torque = this.GetGearboxTorgue();
            this.virtualFunctionBus.GearboxPacket.Gear = this.Gear;
            this.virtualFunctionBus.GearboxPacket.DriveSubgear = this.DriveSubgear;
        }

        /// <summary>
        /// Handles the logic behind the automatic gear shift.
        /// </summary>
        public void HandleAutomaticGearshift()
        {
            // TODO actually use motor's rev
            int motorRev = 300;

            if (motorRev >= MaxMotorRevolution)
            {
                this.ShiftUp();
            }
            else if (motorRev <= MinMotorRevolution)
            {
                this.ShiftDown();
            }
        }

        /// <inheritdoc/>
        public void ShiftDown()
        {
            switch (this.Gear)
            {
                case Gear.Drive:
                    if (this.DriveSubgear == 0)
                    {
                        this.Gear = Gear.Neutral;
                        break;
                    }

                    this.DriveSubgear = Math.Min(this.DriveSubgear - 1, 0);
                    break;
                case Gear.Neutral:
                    this.Gear = Gear.Reverse;
                    break;
                case Gear.Reverse:
                    this.Gear = Gear.Park;
                    break;
                case Gear.Park:
                    break;
            }
        }

        /// <inheritdoc/>
        public void ShiftUp()
        {
            switch (this.Gear)
            {
                case Gear.Park:
                    this.Gear = Gear.Reverse;
                    break;
                case Gear.Reverse:
                    this.Gear = Gear.Neutral;
                    break;
                case Gear.Neutral:
                    this.Gear = Gear.Drive;
                    break;
                case Gear.Drive:
                    this.DriveSubgear = Math.Min(this.DriveSubgear + 1, MaxDriveSubgears);
                    break;
            }
        }
    }
}
