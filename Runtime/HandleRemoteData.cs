//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Reflection;
using System.Threading.Tasks;
using Unity.Services.RemoteConfig;

namespace Connor.RemoteConfigHelper.Runtime
{
    internal class HandleRemoteData
    {
        // this is a shorthand, just so we don't have to say RemoteConfigService so much
        private RemoteConfigService Remote => RemoteConfigService.Instance;

        private RuntimeConfig _jsonObject = null;

        /// <summary>
        /// This retrieves the remote of a field
        /// </summary>
        /// <param name="field">The field you want to find the remote of</param>
        /// <returns>the remote value, null if it fails to find a remote</returns>
        internal async Task<dynamic> GetValue(FieldInfo field)
        {
            /*var theObject = await GetSpecificJSON(field.DeclaringType?.Name);
            var value;
            foreach (var val in theObject)
            {
                if (field.Name == val.Name)
                {
                    value = val.Value;
                }
            }*/
            return null;
        }

        /// <summary>
        /// this guy gets the runtime config and gets a specific object from it
        /// </summary>
        /// <param name="objectName">The name of the object you want to get from the remote</param>
        /// <returns>The remote object</returns>
        private async Task<dynamic> GetSpecificJSON(string objectName)
        {
            RuntimeConfig json = await GetRemote();
            string objectJson = json.GetJson(objectName);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(objectJson);
        }

        /// <summary>
        /// this guy authenticates and fetches the remote, theres a check to make sure that if we try and 
        /// call him more than once he doesn't authenticate and fetch more than once
        /// </summary>
        /// <returns>the remote RuntimeConfig</returns>
        private async Task<RuntimeConfig> GetRemote()
        {
            await Connection.Service.Authenticate();
            Remote.SetEnvironmentID(Connection.Service.RemoteID);

            Remote.FetchConfigs(new userAttributes(), new appAttributes());

            // Check to make sure the scene has not been unloaded
            if (this == null)
            {
                return null;
            }

            _jsonObject = Remote.appConfig;

            return _jsonObject;
        }

        private struct userAttributes { }
        private struct appAttributes { }
    }
}