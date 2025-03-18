using System;
using System.Collections.Generic;
using UnityEngine;

namespace HiddenObjectGame.Runtime.Services
{
    [Serializable]
    public class HiddenObjectSaveData
    {
        public List<string> FoundedObjects = new List<string>();
        private const string DataKey = "HiddenObjectCollectService";

        public void Save()
        {
            var json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(DataKey, json);
        }

        public void AddFoundedObject(string id)
        {
            FoundedObjects.Add(id);
            Save();
        }
        public void Load()
        {
            if (PlayerPrefs.HasKey(DataKey))
            {
                var json = PlayerPrefs.GetString(DataKey);
                JsonUtility.FromJsonOverwrite(json, this);
            }
        }
    }
}