using System.Collections;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Controllers.Interfaces
{
    public interface IController
    {
        public bool ControllerInitComplete { get; set; }
        public IEnumerator InitializeController();
        public void CompleteInitialization();
    }
}