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
        public const int MaxMotorRevolution = 3000;

        /// <summary>
        /// The maximum revolution of the motor, when a gearshift should happen.
        /// </summary>
        public const int MinMotorRevolution = 2000;

        private double[] driveSubgearRatios = new double[] { 0.0823, 0.0823, 0.1647, 0.2470, 0.3293, 0.4116 };

        private GearboxPacket gearboxPacket;

        private bool selfDriveMode = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutomaticGearbox"/> class.
        /// </summary>
        /// <param name="virtualFunctionBus">The car's virtual function bus.</param>
        public AutomaticGearbox(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.Gear = Gear.Park;
            this.DriveSubgear = 0;
            this.gearboxPacket = new GearboxPacket();
            virtualFunctionBus.GearboxPacket = this.gearboxPacket;
        }

        /// <inheritdoc/>
        public Gear Gear { get; set; }

        /// <inheritdoc/>
        public int DriveSubgear { get; set; }

        /// <inheritdoc/>
        public double GetGearboxTorgue()
        {
            switch (this.Gear)
            {
                case Gear.Park:
                    return 0;
                case Gear.Reverse:
                case Gear.Neutral:
                    return this.driveSubgearRatios[0] * this.virtualFunctionBus.ReadonlyEnginePacket.EngineRPM;
                case Gear.Drive:
                    return this.driveSubgearRatios[this.DriveSubgear] * this.virtualFunctionBus.ReadonlyEnginePacket.EngineRPM;
            }

            throw new Exception("Invalid gear");
        }

        /// <inheritdoc/>
        public override void Process()
        {
            if (selfDriveMode)
            {
                this.HandleAutomaticGearshift();
            }
            else
            {
                this.subGearShift();
            }
            this.gearboxPacket.Torque = this.GetGearboxTorgue();
            this.gearboxPacket.Gear = this.Gear;
            this.gearboxPacket.DriveSubgear = this.DriveSubgear;
        }

        /// <summary>
        /// Handles the logic behind the automatic gear shift.
        /// </summary>
        public void HandleAutomaticGearshift()
        {
            // TODO actually use motor's rev
            int motorRev = this.virtualFunctionBus.ReadonlyEnginePacket.EngineRPM;

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

                    this.DriveSubgear = Math.Max(this.DriveSubgear - 1, 0);
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

        /// <inheritdoc/>
        public void ManualShiftUp()
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
                    break;
            }
        }

        /// <inheritdoc/>
        public void ManualShiftDown()
        {
            switch (this.Gear)
            {
                case Gear.Drive:
                    this.Gear = Gear.Neutral;
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

        public void subGearShift()
        {
            int motorRev = this.virtualFunctionBus.ReadonlyEnginePacket.EngineRPM;

            if (this.Gear == Gear.Drive)
            {
                if (motorRev >= MaxMotorRevolution)
                {
                    this.DriveSubgear = Math.Min(this.DriveSubgear + 1, MaxDriveSubgears);
                }
                else if (motorRev <= MinMotorRevolution)
                {
                    this.DriveSubgear = Math.Max(this.DriveSubgear - 1, 0);
                }
            }
        }
    }
}
