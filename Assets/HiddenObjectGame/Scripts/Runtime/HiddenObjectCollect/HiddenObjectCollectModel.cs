using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    [Serializable]
    public class HiddenObjectCollectModel: IHiddenObjectCollectModel
    {
        public List<string> FoundedObjects { get; set; } = new List<string>();
        private const string DataKey = "HiddenObjectCollectService";

        public void Save()
        {
            var json = JsonConvert.SerializeObject(this);
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
                JsonConvert.PopulateObject(json, this);
            }
        }
    }
}