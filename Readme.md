# User System Framework for Unity
User System Framework is a Unity plugin that helps you to create user systems for your games. It's a very flexible, extendable and easy to use system that can be integrated into any Unity project.
The idea is to eliminate needing to access to database. Just set up the PHP server and then handle everything else from inside Unity

### Important: This is a work in progress
This plugin is still in development. Although the features that are in are production ready, there are still some features that are not implemented yet and some parts may require some hand-touch. Also the ease of-use, user-friendliness and some QOL updates that were planned did not get in yet sadly. But regardless, the base system is working and can be used in production.

# User System Framework: Quick How-To

So basically what you would do is log the user in and access some data from the server right? Here is a quick rundown of how you can do that.
* Create a new IUser (or just UserData) object and set the username and password and any other data necessary.
* Call create a new **LoginApiRequest()**, subscribe to the SuccessCallbackEvent and FailureCallbackEvent events and then do Call() on the request.
  ```
    LoginApiRequest loginRequest = new LoginApiRequest(createdUserData);
    loginRequest.SuccessCallbackEvent += OnLoginSuccess;
    loginRequest.FailureCallbackEvent += OnLoginFailure;
    loginRequest.Call();
    ```
* In the success callback you'll receive your request back with headers and whatever data necessary
* In the failure callback you'll receive the error message and the request back
* Access the local user with ``` LocalAccountController.CurrentLocalUser ``` from anywhere
* Use anything from the user data like ``` LocalAccountController.CurrentLocalUser.Username ``` or ``` LocalAccountController.CurrentLocalUser.Email ```
* Set stuff with ``` LocalAccountController.CurrentLocalUser.Username = "newUsername"; ``` or ``` LocalAccountController.CurrentLocalUser.Email = "newEmail"; ```cand then calling ``` LocalAccountController.CurrentLocalUser.UpdateUserData(); ```
* You can define custom fields with CustomFieldsConfiguration and get/set them using ``` LocalAccountController.CurrentLocalUser.SetCustomUserData("key", "value"); ``` and ``` LocalAccountController.CurrentLocalUser.GetCustomUserDataString("key"); ```

For easier access, the system auto-logs the user in or registers the user if the user is not logged in. So you can just call ``` LocalAccountController.CurrentLocalUser ``` and it will be there. You can turn this off in configurations.

For PHP side, just drag & drop the PHP files in the PHP folder to your server and set the configurations in Unity.

# User System Framework: Code Base
Welcome to the brief explanation sheet that outlines the basic code structure of User System Framework and how it works in general.  
In this guide, you'll find the general descriptions of how the code base is built, what patterns, principles and techniques are used  
and how it's all glued together.

### Pattern & Principles
Main driver of this code base uses **Service Locator** pattern along with a mix of MVC and OOP. First of all we can split  
the entire system into 2 parts that we call:

* <span style="font-size:18px">**API :**</span> It's the high level access for the developers that intend to use this for their games. **So basically it's where you use the plugin**
* <span style="font-size:18px">**System :**</span> It's the low level structure of the plugin where API calls happen in a decoupled, isolated but very accessible fashion.

So for daily usage inside your code, API should suffice. But in edge cases, extension requirements and maybe just taking a look into how things internally work, you may take a look at the System classes.

When it comes to visibility and accessibility, the system classes have mostly private/internal setups as appropriate but almost all scripts have proper definitions, summaries or at least comments on internal workings of the code.

### Namespace Structure
System namespace has 5 main namespaces and all follow the same basic naming convention except the ones in **Structure** namespace.

* <span style="font-size:18px">**Config**</span> namespace has all the static data that is accessible anywhere and it strictly uses ScriptableObject pattern from Unity.
* <span style="font-size:18px">**Controllers**</span> namespace has all the MonoBehaviour driven classes which most of them act as controllers for the data in different services. They mostly handle scene work for non-scene scripts.
* <span style="font-size:18px">**Data**</span> namespace has data centric classes. Mostly acting as a repository.
* <span style="font-size:18px">**Services**</span> namespace is the backbone of the whole system. It has numerous services that help the system work and drive the API with the help of controllers.
* <span style="font-size:18px">**Structures**</span> namespace constructs the services and controllers so it acts like a base for all the other namespaces.