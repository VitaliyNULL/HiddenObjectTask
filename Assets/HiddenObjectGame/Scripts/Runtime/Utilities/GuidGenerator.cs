using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HiddenObjectGame.Runtime.Utilities
{
    public static class GuidGenerator
    {
        public static void TryGenerateGuid(ref string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = Guid.NewGuid().ToString();

#if UNITY_EDITOR
                if (Application.isEditor && !Application.isPlaying)
                {
                    UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                }
#endif
            }
        }
    }
}