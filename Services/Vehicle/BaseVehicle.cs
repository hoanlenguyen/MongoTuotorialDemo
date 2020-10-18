namespace MongoTutorialDemo.Services.Vehicle
{
    internal abstract class BaseVehicle
    {
        public int Name { get; set; }

        public abstract EngineType Type { get; }
    }
}