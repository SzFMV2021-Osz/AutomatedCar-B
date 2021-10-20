// <copyright file="EngineComponent.cs" company="PlaceholderCompany">
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

    public class Engine : SystemComponent
    {
        private const int MaxRPM = 7000; // Maximálisan megengedett fordulatszám
        private const int BaseRPM = 500; // Naggyáboli alapjárati érték

        private double gasPedalValue; // Gázpedál állásának tárolása
        private double breakPedalValue; // Fékpedál állásának tárolása

        private EnginePacket enginePacket;

        public Engine(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            //this.gasPedalValue = 
            this.enginePacket = new EnginePacket();
            virtualFunctionBus.ReadonlyEnginePacket = this.enginePacket;
        }

        public override void Process()
        {
            throw new NotImplementedException();
        }
    }
}
