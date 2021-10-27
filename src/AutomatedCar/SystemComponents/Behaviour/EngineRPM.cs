// <copyright file="EngineRPM.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.SystemComponents.Behaviour
{
    using ReactiveUI;

    public class EngineRPM : ReactiveObject
    {
        private int rpm;

        public int RPM { get => this.rpm; set => this.RaiseAndSetIfChanged(ref this.rpm, value); }
    }
}
