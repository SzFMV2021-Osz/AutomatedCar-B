// <copyright file="Radar.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Sensors
{
    using System;

    /// <summary>
    /// Radar class.
    /// </summary>
    public class Radar : SystemComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Radar"/> class.
        /// </summary>
        /// <param name="virtualFunctionBus"></param>
        public Radar(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override void Process()
        {
            throw new NotImplementedException();
        }
    }
}
