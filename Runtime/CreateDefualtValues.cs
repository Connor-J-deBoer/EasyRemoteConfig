using UnityEditor;

namespace Connor.EasyRemoteConfig.Runtime
{
    public static class CreateDefualtValues
    {
        [MenuItem("Easy Remote Config/Create Remote Asset")]
        private static void CreateRemoteAsset()
        {
            GetRemoteFields.CreateAsset();
        }
    }
}