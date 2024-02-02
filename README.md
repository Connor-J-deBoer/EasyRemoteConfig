<h1>Easy Remote Config</h1>
This is a unity package for Unity 2022.3 that simplifys the proccess of creating a remotely configurable field

Current the package only handles simple fields (int, float, string, bool, ect...) but I plan on adding support for more complex types as well as collections

<h1>Example</h1>
<code>[RemoteField] private int thisFieldIsRemote = 0;</code> 
<br/>
and the call
<br/>
<code>HandleRemoteFields.Service.UpdateFields();</code>
<br/>
Warning this guy is expensive, don't call him in update eh?

<h1>Some setup required</h1>
After adding the package via git url you need to head to [Edit > Project Settings > Player] and then change the Api Compatibility Level* from .NET Standard 2.1 to .NET Framework
Then you need to link your project to the UGS stuff. Unity has some great docs on this, I believe in you!
<br/>
Once you have all your UGS stuff set up you need to go to the Resources folder in your project, right click and head to Create > Remote Config Setup
<br/> 
Very important make sure the scriptable object is named "Environment", but after that feel free to just fill it with your environment Id's and you're off to the races!
