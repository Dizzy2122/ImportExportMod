// CarSourceScenario.cs
using GTA;

namespace ImportExportModNamespace
{
    public abstract class CarSourceScenario
    {
        protected Vehicle TargetVehicle;

        public CarSourceScenario()
        {
            GenerateTargetVehicle();
        }

        protected abstract void GenerateTargetVehicle();

        public abstract void StartScenario();
    }
}

