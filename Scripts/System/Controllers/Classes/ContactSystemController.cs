using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UserSystemFramework.Scripts.System.Controllers.Attributes;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using UserSystemFramework.Scripts.System.Data.Classes;
using UserSystemFramework.Scripts.System.Data.Enums;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Controllers.Classes
{
    
    [DependsOn(Controller = typeof(LocalAccountController))]
    public class ContactSystemController : BaseController<ContactSystemController>, IController
    {
        protected ServerRequestSenderService ServerRequest { private set; get; }

        [ControllerInitialization]
        public override IEnumerator InitializeController()
        {
            yield return base.InitializeController();
            yield return new WaitForControllers<ContactSystemController>();
            ServerRequest = ServiceHandler.Locator.Get<ServerRequestSenderService>();
            CompleteInitialization();
        }

        internal async void GetContacts(IRequest completedRequest, int forUserId = -99, int page = 0, int limit = 5000)
        {
            IRequest contactGetterRequest = ServerRequestGetterService.Get(RequestType.ContactGetterRequest, true, false, new Dictionary<string, string>()
            {
                {"forUserId", forUserId == -99 ? LocalAccountController.CurrentLocalUser.ID.ToString() : forUserId.ToString()},
                {"page", page.ToString()},
                {"limit", limit.ToString()}
            });
            if(completedRequest != null) 
                contactGetterRequest.CopyTokenFromRequest(completedRequest);
            await ServerRequest.SendRequest(contactGetterRequest, GetContactsCallback);
        }
        
        [RequestCallbackMethod]
        private void GetContactsCallback(IRequest returnRequest)
        {
            bool isSuccess = returnRequest.ResultType == RequestResultType.Success;
            bool isNeutral = returnRequest.ResultType == RequestResultType.Neutral;
            if(isNeutral)
            {
                EventPublisher.TriggerGetContactsComplete(returnRequest);
                return;
            }
            if (isSuccess)
            {
                IUser requestingUser =
                    JsonConvert.DeserializeObject<UserData>(returnRequest.ConnectionResponseHeaders["RequestingUser"]);
                
                List<UserData> returnedContacts = new List<UserData>(
                    JsonConvert.DeserializeObject<List<UserData>>(
                        returnRequest.ConnectionResponseHeaders["RequestResultUsers"])!);
                
                List<ContactData> returnedContactData = new List<ContactData>(
                    JsonConvert.DeserializeObject<List<ContactData>>(
                        returnRequest.ConnectionResponseHeaders["RequestResultContactData"])!);
                
                ContactList<IUser> contactList = new ContactList<IUser>();
                LocalAccountController.CurrentLocalUser.Contacts = contactList;
                for (var index = 0; index < returnedContacts.Count; index++)
                {
                    UserData contact = returnedContacts[index];
                    ContactData contactData = returnedContactData[index];
                    LocalAccountController.CurrentLocalUser.Contacts.UserContacts.Add(contact, (ContactStatus)contactData.RelationshipStatus);
                    DebugService.Log("Contact added" + contact.UserName, DebuggingLevel.Everything);
                }
                EventPublisher.TriggerGetContactsComplete(returnRequest);
            }
            else
            {
                EventPublisher.TriggerGetContactsErrorFired(returnRequest);
            }
        }
        
        [Obsolete("This method is obsolete. Please use the data received from GetContacts")]
        internal async void GetMySentRequests(IRequest completedRequest, int page = 0, int limit = 50)
        {
            IRequest mySentRequestsGetterRequest = ServerRequestGetterService.Get(RequestType.MySentRequestsGetterRequest, true, false, new Dictionary<string, string>()
            {
                {"page", page.ToString()},
                {"limit", limit.ToString()}
            });
            await ServerRequest.SendRequest(mySentRequestsGetterRequest, GetMySentRequestsCallback);
        }

        [RequestCallbackMethod]
        private void GetMySentRequestsCallback(IRequest returnRequest)
        {
            bool isSuccess = returnRequest.ResultType == RequestResultType.Success;
            bool isNeutral = returnRequest.ResultType == RequestResultType.Neutral;
            if(isNeutral)
            {
                EventPublisher.TriggerGetMySentRequests(returnRequest);
                return;
            }
            if(isSuccess)
            {
                List<ContactRequestData> sentRequests = JsonConvert.DeserializeObject<List<ContactRequestData>>(
                    returnRequest.ConnectionResponseHeaders["RequestResult"]);
                LocalAccountController.CurrentLocalUser.SentRequests = sentRequests;
                EventPublisher.TriggerGetMySentRequests(returnRequest);
            }
            else
            {
                EventPublisher.TriggerGetMySentRequestsErrorFired(returnRequest);
            }
        }
        [Obsolete("This method is obsolete. Please use the data received from GetContacts")]
        internal async void GetRequestsForMe(IRequest completedRequest, int page = 0, int limit = 50)
        {
            IRequest myRequestsGetterRequest = ServerRequestGetterService.Get(RequestType.MyRequestsGetterRequest, true, false, new Dictionary<string, string>()
            {
                {"page", page.ToString()},
                {"limit", limit.ToString()}
            });
            await ServerRequest.SendRequest(myRequestsGetterRequest, GetRequestsForMeCallback);
        }

        [RequestCallbackMethod]
        private void GetRequestsForMeCallback(IRequest returnRequest)
        {
            bool isSuccess = returnRequest.ResultType == RequestResultType.Success;
            bool isNeutral = returnRequest.ResultType == RequestResultType.Neutral;
            if(isNeutral)
            {
                EventPublisher.TriggerGetMyRequests(returnRequest);
                return;
            }
            if(isSuccess)
            {
                List<ContactRequestData> receivedRequests = JsonConvert.DeserializeObject<List<ContactRequestData>>(
                    returnRequest.ConnectionResponseHeaders["RequestResult"]);
                LocalAccountController.CurrentLocalUser.ReceivedRequests = receivedRequests;
                EventPublisher.TriggerGetMyRequests(returnRequest);
            }
            else
            {
                EventPublisher.TriggerGetMyRequestsErrorFired(returnRequest);
            }
        }

        internal async void ChangeContactRelationship(int otherUserID, int status, int fromUserId = -99)
        {
            IRequest contactGetterRequest = ServerRequestGetterService.Get(RequestType.ContactStatusChangerRequest, true, false, new Dictionary<string, string>()
            {
                {"fromUserId", fromUserId == -99 ? LocalAccountController.CurrentLocalUser.ID.ToString() : fromUserId.ToString()},
                {"toUserId", otherUserID.ToString()},
                {"toStatus", status.ToString()}
            });
            await ServerRequest.SendRequest(contactGetterRequest, ChangeContactRelationshipCallback);
        }

        [RequestCallbackMethod]
        private void ChangeContactRelationshipCallback(IRequest returnRequest)
        {
            bool isSuccess = returnRequest.ResultType == RequestResultType.Success;
            bool isNeutral = returnRequest.ResultType == RequestResultType.Neutral;
            if(isNeutral)
            {
                EventPublisher.TriggerChangeContactRelationshipComplete(returnRequest);
                return;
            }
            if(isSuccess)
            {
                EventPublisher.TriggerChangeContactRelationshipComplete(returnRequest);
            }
            else
            {
                EventPublisher.TriggerChangeContactRelationshipErrorFired(returnRequest);
            }
        }
    }
}