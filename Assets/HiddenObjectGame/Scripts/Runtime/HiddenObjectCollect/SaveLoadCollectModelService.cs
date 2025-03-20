using System;
using Newtonsoft.Json;
using ObservableCollections;
using UnityEngine;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    public class SaveLoadCollectModelService : IDisposable
    {
        private readonly IHiddenObjectCollectModel _collectModel;
        private const string DataKey = "HiddenObjectCollectService";

        public SaveLoadCollectModelService(IHiddenObjectCollectModel collectModel)
        {
            _collectModel = collectModel;
            Load();
            _collectModel.FoundedObjects.CollectionChanged += OnCollectionChanged;
        }


        private void OnCollectionChanged(in NotifyCollectionChangedEventArgs<string> args)
        {
            Save();
        }

        private void Save()
        {
            var json = JsonConvert.SerializeObject(_collectModel);
            PlayerPrefs.SetString(DataKey, json);
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey(DataKey))
            {
                var json = PlayerPrefs.GetString(DataKey);
                JsonConvert.PopulateObject(json, _collectModel);
            }
        }

        public void Dispose()
        {
            _collectModel.FoundedObjects.CollectionChanged -= OnCollectionChanged;
        }
    }
}