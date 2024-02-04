using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;

namespace Connor.RemoteConfigHelper
{
    internal static class HandleUGSApi
    {
        private static string _responseJson = "";
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
                    Debug.Log("Fetched JSON");
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

        internal async static Task PushToRemote(ServiceAccountConfig serviceConfig, EnvironmentConfig environmentConfig, object newValue)
        {
            // this just prevents us from spam calling ugs for our get, we only want it to happen once
            if (_busy) return;
            if (string.IsNullOrEmpty(_responseJson))
            {
                await GetRemote(serviceConfig, environmentConfig);
                _busy = false;
            }

            Debug.Log(_responseJson);
        }
    }
}
