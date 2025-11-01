//=================================================================\\
//======Copyright (C) 2025 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Firestore;

namespace MQG.EasyRemoteConfig.Runtime
{
    public class FirebaseConnect
    {
        private static FirebaseFirestore _db;

        private static async Task InitializeDB()
        {
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
            if (dependencyStatus != DependencyStatus.Available)
                throw new Exception("Could not resolve all Firebase dependencies");

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