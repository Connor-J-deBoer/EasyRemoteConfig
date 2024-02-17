using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Connor.RemoteConfigHelper.Editor
{
    public class CreateRemoteListsOnBuild : IProcessSceneWithReport
    {
        public int callbackOrder { get { return 0; } }

        public void OnProcessScene(Scene scene, BuildReport report)
        {
            Runtime.GetRemoteFields.CreateAsset();
        }
    }
}