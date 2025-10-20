//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Connor.RemoteConfigHelper.Runtime
{
    public sealed class HandleRemoteFields
    {
        private HandleRemoteFields() { }
        private static HandleRemoteFields _instance = null;
        public static HandleRemoteFields Service
        {
            get
            {
                if (_instance == null) _instance = new HandleRemoteFields();
                return _instance;
            }
        }
        
        //private RemoteFieldList _remoteFields => UpdateRemoteFieldList();

        /// <summary>
        /// This guy Updates every remote field with their respective remote
        /// IMPORTANT This guy uses a lot of reflection and dynamic types, meaning he's quite expensive, use him carefully
        /// </summary>
        public async void UpdateFields()
        {
#if UNITY_EDITOR
            GetRemoteFields.CreateAsset();
#endif
            HandleRemoteData remote = new HandleRemoteData();
            
            string[] fieldNames = Array.Empty<string>();// = _remoteFields.RemoteFields.ToArray();
            string[] declaringTypeName = Array.Empty<string>();;// = _remoteFields.RemoteFieldsParentClass.ToArray();
            string[] objectNames = Array.Empty<string>();;// = _remoteFields.RemoteGameObjects.ToArray();
            FieldInfo[] fields = new FieldInfo[fieldNames.Length];
            MonoBehaviour[] objects = new MonoBehaviour[fieldNames.Length];
            for (int i = 0; i < fields.Length; ++i)
            {
                objects[i] = (MonoBehaviour)GameObject.Find(objectNames[i]).GetComponent(declaringTypeName[i]);
                fields[i] = objects[i].GetType().GetField(fieldNames[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                // get the remote value
                //var remoteValue = await remote.GetValue(fields[i]);
                if (false)
                {
                    // TODO: create the field in the remote
                    Debug.LogWarning($"No remote found for {fields[i].Name}");
                    continue;
                }
                try
                {
                    // Cast remoteValue to the type of the field
                    //object convertedValue = HandleConversions.Convert(remoteValue, fields[i].GetValue(objects[i]));

                    // override the local with the remote
                    //fields[i].SetValue(objects[i], convertedValue);
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning($"Failed to override local field \"{fields[i].Name}\" because {ex}");
                }
            }
        }

        /*private RemoteFieldList UpdateRemoteFieldList()
        {
            string remoteListJson = System.IO.File.ReadAllText($"Assets/Resources/RemoteList_{SceneManager.GetActiveScene().name}/RemoteList.json");
            RemoteFieldList remoteList = JsonUtility.FromJson<RemoteFieldList>(remoteListJson);
            return remoteList;
        }*/
    }
}