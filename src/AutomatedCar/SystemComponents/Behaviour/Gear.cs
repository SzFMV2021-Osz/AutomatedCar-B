namespace AutomatedCar.SystemComponents.Behaviour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// All possible gear positions.
    /// </summary>
    public enum Gear
    {
        /// <summary>
        /// In this gear the car is stationarry.
        /// </summary>
        Park = 0,

        /// <summary>
        /// In this gear the car moves backwards.
        /// </summary>
        Reverse = 1,

        /// <summary>
        /// In this gear the motor has no effect on the car's movement.
        /// </summary>
        Neutral = 2,

        /// <summary>
        /// In this gear the car accelerates with the motor.
        /// </summary>
        Drive = 3,
    }
}
