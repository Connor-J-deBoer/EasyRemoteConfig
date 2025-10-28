<h1>Easy Remote Config</h1>
This is a Unity package for Unity 2022.3+ that simplifys the proccess of creating a remotely configurable field
<br/>
<br/>
<h1>Example</h1>
<code>[RemoteField] private int thisFieldIsRemote = 0;</code> 
<br/>
and the call
<br/>
<code>Remote.ApplyRemoteValues();</code>
<br/>
This call is very expensive, avoid calling this method in loops or update methods because it will seriously affect FPS

<h1>Some setup required</h1>
After installing this package, Unity should prompt you to link your project, click the Project Settings button and it'll take you right to where you can link your project. You need to be signed into Unity for this to work. You also need to have set up an organization, then you can create a new project or link it to an exisiting one. While you're at the Unity Gaming Services site you'll wanna set up some environments as we'll need them later.
<br/>
<br/>
After linking the project you'll notice some errors, you need to head to [Edit > Project Settings > Player] and then change the Api Compatibility Level* from .NET Standard 2.1 to .NET Framework.
<br/>
<br/>
After all of that you need to go to the Resources folder in your project (create one if you don't already have one), right click and head to [Create > Remote Config Setup > Environment Config]. Very important make sure the scriptable object is named "Environment", but after that feel free to just fill it with your environment Id's and you're off to the races!

<h1>Optional Features</h1>
There's currently one optional feature, It allows you to automatically update the remote by updating remote fields in the inspector. All you have to do is right click your resourse folder once more [Create > Remote Config Setup > Service Account Config] and then fill in the details. You will need to set up a service account with UGS, you'll find that at the UGS website, click on [Administration > Service accounts] and then create one. Note that you will want to save your secrete key somewhere else before putting in the SO just so you can't get locked out of your service accounts. There's one notable limitation, collections don't work properly.
