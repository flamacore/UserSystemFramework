namespace UserSystemFramework.Scripts.System.Structure.Interfaces
{
    /// <summary>
    /// This enum defines when the service gets initialized. The idea is to have a sort order for services so one can
    /// easily depend on another. Higher priority services are loaded before the lower ones.
    /// </summary>
    public enum ServicePriority
    {
        High,
        Medium,
        Low,
        VeryLow
    }
    /// <summary>
    /// Interface for creating a service. Use this if you intend to extend the system by implementing a new service.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Called when the service is initialized. Timing depends on <see cref="ServicePriority"/>.
        /// </summary>
        public void Init();
        public void OnDisable();
        public void TriggerSceneReady();
        public ServicePriority Priority { get; }
        public bool IsReady { get; set; }
    }
}