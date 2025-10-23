//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Connor.EasyRemoteConfig.Editor
{
    internal static class HandleUGSApi
    {
        private static string _responseJson = "";
        private static string _configId = "";
        private static HttpClient _httpClient = null;
        private static bool _busy = false;

        private static HttpClient _client
        {
            get 
            {
                if (_httpClient == null)
                {
                    // if there was no client make one
                    _httpClient = new HttpClient();
                    // set it's base address to the ugs remote config one
                    _httpClient.BaseAddress = new Uri("https://services.api.unity.com/remote-config/v1/projects/");
                    // add the basic header, we'll add the security one later
                    _httpClient.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                }
                return _httpClient;
            }
        }

        private async static Task GetRemote(ServiceAccountConfig serviceConfig, EnvironmentConfig environmentConfig)
        {
            _busy = true;
            string endpoint = $"{serviceConfig.ProjectID}/environments/{environmentConfig.CurrentEnvironment}/configs";
            try
            {
                _client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", serviceConfig.Key);

                HttpResponseMessage response = await _client.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string responseJson = await response.Content.ReadAsStringAsync();
                    _responseJson = responseJson;
                }
                else
                {
                    Debug.LogWarning(response.StatusCode);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        private async static Task UpdateRemoteFields(ServiceAccountConfig serviceConfig, EnvironmentConfig environmentConfig, SerializedProperty newValue)
        {
            if (string.IsNullOrEmpty(_responseJson))
            {
                await GetRemote(serviceConfig, environmentConfig);
                _busy = false;
            }

            string propertyName = newValue.name;
            string className = newValue.serializedObject.targetObject.GetType().Name;
            dynamic values = JsonConvert.DeserializeObject<dynamic>(_responseJson);

            foreach (dynamic value in values.configs[0].value)
            {
                if (value.key == className)
                {
                    if (value.value[propertyName] == null) return;

                    value.value[propertyName] = (dynamic)newValue.boxedValue;
                    break;
                }
            }
            _configId = values.configs[0].id;
            _responseJson = JsonConvert.SerializeObject(values);
        }

        internal async static Task PushToRemote(ServiceAccountConfig serviceConfig, EnvironmentConfig environmentConfig, SerializedProperty newValue)
        {
            // this just prevents us from spam calling ugs for our get, we only want it to happen once
            if (_busy) return;
            await UpdateRemoteFields(serviceConfig, environmentConfig, newValue);

            var responseString = JsonConvert.DeserializeObject<dynamic>(_responseJson);
            Payload payload = new Payload();
            List<PayloadValue> payloadValues = new List<PayloadValue>();
            foreach(var values in responseString.configs[0].value) 
            {
                PayloadValue payloadValue = new PayloadValue();
                
                payloadValue.key = values.key;
                payloadValue.type = values.type;
                payloadValue.value = values.value;

                payloadValues.Add(payloadValue);
            }
            payload.value = payloadValues;

            var payloadString = JsonConvert.SerializeObject(payload);
            try
            {
                string endpoint = $"{serviceConfig.ProjectID}/configs/{_configId}";

                HttpContent body = new StringContent(payloadString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(endpoint, body);
                if (response.IsSuccessStatusCode)
                {
                    Debug.Log($"Updated remote {newValue.name} to {newValue.boxedValue}");
                }
                else
                {
                    Debug.LogWarning(response.StatusCode);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}