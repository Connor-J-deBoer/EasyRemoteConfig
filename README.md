<h1>Easy Remote Config</h1>

This is a Unity package for Unity 6.000.0.60f1 that simplify the process of creating a remotely configurable field

<h1>Example</h1>
<code>[RemoteField] private int thisFieldIsRemote = 0;</code>

and the call

<code>Remote.ApplyRemoteValues();</code>

This call is very expensive, avoid calling this method in loops or update methods because it will seriously affect FPS
<h1>Limitations</h1>

Because this package uses the Firebase SDK it's limited by the Firebase SDK's available plateforms, which as of today means this package is only functional for Andriod and IOS projects. There is a plan to change Easy Remote Config away from the SDK to make installation and setup easier, as well as making the package fully functional on all build platforms.

<h1>Some setup required</h1>

Easy Remote Config uses Firebase Firestore's Unity SDK to handle remote fields.

1. Head to <a herf="https://firebase.google.com/?gclsrc=aw.ds&gad_source=1&gad_campaignid=12301997661&gbraid=0AAAAADpUDOj_ufjvBJ4BPMcf1daxtZZCK&gclid=CjwKCAjwmNLHBhA4EiwA3ts3md-0uGaMMkA19Zr_s5rk3R_D4Pe3H-aoyLkaoCL42_n9uSJrcmkTTBoCvk8QAvD_BwE">Firebase Website</a> create an account if you don't already have one, then click the "Go to console" button
2. Create or open a project
3. Go to project overview
4. Press the "+" button to add another app
5. Follow Firebase instructions, make sure to install the Firebase Analytics and Firebase Firestore SDK's
6. Return to the unity editor, go to the Resources folder in your project (create one if you don't already have one), right click and head to [Create > Remote Config Setup > Remote Config Context]. This creates a scriptable object that currently has one field for the environment name, by default it's "Development"

<h1>How to Use</h1>

1. To add all the remote values to the Firebase Firestore go to the menu bar and select [Easy Remote Config > Create Remote Asset] and then [Easy Remote Config > Push Current Asset]
2. To update local fields, simply call ``Remote.ApplyRemoteValues();`` anywhere you want
3. To change the values on the remote, head to <a herf="https://firebase.google.com/?gclsrc=aw.ds&gad_source=1&gad_campaignid=12301997661&gbraid=0AAAAADpUDOj_ufjvBJ4BPMcf1daxtZZCK&gclid=CjwKCAjwmNLHBhA4EiwA3ts3md-0uGaMMkA19Zr_s5rk3R_D4Pe3H-aoyLkaoCL42_n9uSJrcmkTTBoCvk8QAvD_BwE">Firebase Website</a> and navigate to your desired scene and your desired object and update your desired values
