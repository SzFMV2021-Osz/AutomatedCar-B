using ReactiveUI;

namespace AutomatedCar.SystemComponents
{
    public abstract class SystemComponent : ReactiveObject
    {
        protected VirtualFunctionBus virtualFunctionBus;

        protected SystemComponent(VirtualFunctionBus virtualFunctionBus)
        {
            this.virtualFunctionBus = virtualFunctionBus;
            virtualFunctionBus.RegisterComponent(this);
        }

        public abstract void Process();
    }
}