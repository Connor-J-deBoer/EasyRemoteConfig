//=================================================================\\
//======Copyright (C) 2025 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MQG.EasyRemoteConfig.Runtime
{
    public static class SceneHash
    {
        /// <summary>
        /// Generates a hash based on the scene path
        /// </summary>
        /// <param name="scene">Scene to generate a hash from</param>
        /// <returns>The hash as a string</returns>
        public static string GetSceneId(Scene scene)
        {
            string scenePath = scene.path;

            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(scenePath));
                string hash = System.BitConverter.ToString(bytes).Replace("-", "").ToLower();
                return hash;
            }
        }

        /// <summary>
        /// Get a scene in the project based off it's hash
        /// </summary>
        /// <param name="hash">the hash as a string</param>
        /// <returns>The scene name, empty if nothing was found</returns>
        public static string GetScene(string hash)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                string sceneHash = GetSceneId(scene);
                Debug.Log(hash);
                if (sceneHash == hash)
                    return scene.name;
            }

            return "";
        }
    }
}