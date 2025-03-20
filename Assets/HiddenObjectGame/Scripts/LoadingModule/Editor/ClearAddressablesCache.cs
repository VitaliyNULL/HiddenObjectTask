using UnityEditor;
using UnityEngine;

namespace HiddenObjectGame.LoadingModule.Editor
{
    public class ClearAddressablesCache : UnityEditor.Editor
    {
        [MenuItem("Tools/Clear Addressables Cache")]
        public static void ClearCache()
        {
            Caching.ClearCache();
      }
    }
}