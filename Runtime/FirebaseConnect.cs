//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase;
using Firebase.Firestore;
using UnityEditor;
using UnityEngine;

namespace Connor.EasyRemoteConfig.Runtime
{
    public static class FirebaseConnect
    {
        private static FirebaseFirestore _db;
        
        private static async Task InitializeDB()
        {
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
            if (dependencyStatus != DependencyStatus.Available)
                throw new Exception("Could not resolve all Firebase dependencies");
            
            FirebaseApp app = FirebaseApp.DefaultInstance;
            _db = FirebaseFirestore.DefaultInstance;
        }

        public static async Task<FirebaseFirestore> DB()
        {
            if (_db == null)
                await InitializeDB();
            return _db;
        }
    }
}