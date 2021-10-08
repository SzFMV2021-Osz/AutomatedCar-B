namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface IControlledCar
    {
        public int GasPosition { get; set; }

        public int BrakePosition { get; set; }
    }
}
