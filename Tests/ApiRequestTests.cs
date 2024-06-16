using System.Collections;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UserSystemFramework.Scripts.API.ApiRequests;
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Tests
{
    public class ApiRequestTests
    {
        private IUser _testUser;
        private IUser _testUser2;
        private IRequest _loginRequest;
        private IRequest _registerRequest;
        [UnitySetUp]
        public IEnumerator Setup()
        {
            yield return EditorSceneManager.LoadSceneAsyncInPlayMode(Application.dataPath+ "/UserSystemFramework/Tests/TestScene.unity", new LoadSceneParameters(LoadSceneMode.Single));
            ServiceBootstrap.Initialize();
            yield return new WaitForServices();
            yield return new WaitForControllerGetter();
            yield return new WaitUntil(() => ServerRequestSenderService.CanConnect);
            _testUser = new UserData("testUserName", "testMail", "testPassword");
            _testUser2 = new UserData("testUserName2", "testMail2", "testPassword2");
            yield return ApiRequestTest(new RegisterApiRequest(_testUser), true);
            yield return ApiRequestTest(new RegisterApiRequest(_testUser2), true);
        }
        private IEnumerator ApiRequestTest(BaseApiRequest request, bool ignoreAssert = false)
        {
            IRequest testRequest = null;
            request.SuccessCallbackEvent += ApiRequestCallbackEvent;
            request.FailureCallbackEvent += ApiRequestCallbackEvent;
            request.Call();
            yield return new WaitUntil(() => request.CallbackFired);

            void ApiRequestCallbackEvent(IRequest completedRequest)
            {
                testRequest = completedRequest;
                DebugService.LogTest(request.GetType().Name + " Api Request Test completed", DebuggingLevel.Everything);
            }
            if(!ignoreAssert)
                Assert.True(testRequest.ResultType == RequestResultType.Success ||
                            testRequest.ResultType == RequestResultType.Neutral);
        }
        
        [UnityTest]
        public IEnumerator A02_LoginApiRequestTest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
        }

        [UnityTest]
        public IEnumerator A01_RegisterApiRequestTest()
        {
            yield return ApiRequestTest(new RegisterApiRequest(new UserData()));
        }
        
        [UnityTest]
        public IEnumerator A03_AddContactApiRequestTest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new LoginApiRequest(_testUser2));
            _testUser2 = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new AddContactApiRequest(_testUser, _testUser2));
        }
        [UnityTest]
        public IEnumerator A04_IgnoreContactApiRequest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new LoginApiRequest(_testUser2));
            _testUser2 = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new IgnoreContactApiRequest(_testUser, _testUser2));
        }
        
        [UnityTest]
        public IEnumerator A05_BlockContactApiRequest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new LoginApiRequest(_testUser2));
            _testUser2 = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new BlockContactApiRequest(_testUser, _testUser2));
        }
        
        [UnityTest]
        public IEnumerator A06_GetContactsApiRequestTest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser2));
            _testUser2 = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new GetContactsApiRequest(_testUser2));
        }
        
        [UnityTest]
        public IEnumerator B01_GetAchievementsApiRequestTest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new GetAchievementsApiRequest());
        }
        
        [UnityTest]
        public IEnumerator B02_CheckAchievementForUserApiRequestTest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            IRequest testRequest = null;
            CheckAchievementForUserApiRequest request = new CheckAchievementForUserApiRequest(_testUser, 1);
            request.SuccessCallbackEventWithData += CheckAchievementForUserApiRequestCallbackEvent;
            request.FailureCallbackEvent += CheckAchievementForUserApiRequestFailCallbackEvent;
            request.Call();
            yield return new WaitUntil(() => request.CallbackFired);
            void CheckAchievementForUserApiRequestCallbackEvent(IRequest completedRequest, UserAchievementData achievementData)
            {
                testRequest = completedRequest;
                DebugService.LogTest("Get Achievements Api Test completed", DebuggingLevel.Everything);
            }
            void CheckAchievementForUserApiRequestFailCallbackEvent(IRequest completedRequest)
            {
                testRequest = completedRequest;
                DebugService.LogTest("Get Achievements Api Test completed fail", DebuggingLevel.Everything);
            }
            Assert.True(testRequest.ResultType == RequestResultType.Success || testRequest.ResultType == RequestResultType.Neutral);
        }
        [UnityTest]
        public IEnumerator B03_UnlockAchievementForUserApiRequestTest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new UnlockAchievementForUserApiRequest(_testUser, 1, 1));
        }

        [UnityTest]
        public IEnumerator C01_AddAchievementApiRequestTest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new AddAchievementApiRequest("testAchievement", "testLongDescription", "testShortDesc", 100));
        }
        
        [UnityTest]
        public IEnumerator C02_RemoveAchievementApiRequestTest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new AddAchievementApiRequest("testAchievement", "testLongDescription", "testShortDesc", 100));
            yield return ApiRequestTest(new RemoveAchievementApiRequest("testAchievement"));
        }
        
        [UnityTest]
        public IEnumerator D01_SendMessageApiRequest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new LoginApiRequest(_testUser2));
            _testUser2 = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new SendMessageApiRequest(_testUser, "TestMessage"));
        }
        
        [UnityTest]
        public IEnumerator D02_GetMessagesApiRequest() 
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser2));
            _testUser2 = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new GetMessagesApiRequest(0, 50));
        }
        
        [UnityTest]
        public IEnumerator D03_GetMessagesWithUserApiRequest() 
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new LoginApiRequest(_testUser2));
            _testUser2 = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new GetMessagesWithUserApiRequest(_testUser, 0, 50));
        }
        
        [UnityTest]
        public IEnumerator D04_DeleteMessageApiRequest() 
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new DeleteMessageApiRequest(0));
        }
        
        [UnityTest]
        public IEnumerator D05_ReadMessageApiRequest() 
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new ReadMessageApiRequest(0));
        }
        
        [UnityTest]
        public IEnumerator E01_AddItemToInventoryApiRequestTest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new AddItemToUserApiRequest(_testUser, 1, 5));
        }
        
        [UnityTest]
        public IEnumerator E02_RemoveItemFromInventoryApiRequestTest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new RemoveItemFromUserApiRequest(_testUser, 1, 5));
        }
        
        [UnityTest]
        public IEnumerator E03_CheckIfUserHasItemApiRequestTest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new CheckIfUserHasItemApiRequest(_testUser, 1));
        }
        
        [UnityTest]
        public IEnumerator E04_GetUserItemsApiRequestTest()
        {
            yield return ApiRequestTest(new LoginApiRequest(_testUser));
            _testUser = LocalAccountController.CurrentLocalUser;
            yield return ApiRequestTest(new GetUserInventoryApiRequest(_testUser));
        }
        
    }
}
