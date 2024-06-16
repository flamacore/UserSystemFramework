using System.Collections.Generic;
using UserSystemFramework.Scripts.System.Controllers.Attributes;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Controllers.Classes
{
    public class ClanSystemController : BaseController<ClanSystemController>, IController
    {
        //Controller class for managıng Clan System
        [ControllerInitialization]
        public void Initialize()
        {
            //Initialize controller
            
        }
        
        //method for create clan callback
        public void CreateClanCallback(IRequest returnRequest)
        {
            //Create Clan Callback
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerCreateClan(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerCreateClanErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    EventPublisher.TriggerCreateClan(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerCreateClanErrorFired(returnRequest);
                    break;
            }
        }
    }
}