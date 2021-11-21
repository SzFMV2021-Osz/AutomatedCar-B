// <copyright file="Radar.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Sensors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Radar : SystemComponent
    {
        readonly int distance

        public Radar(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
        }

        public override void Process()
        {
            throw new NotImplementedException();
        }
    }
}
