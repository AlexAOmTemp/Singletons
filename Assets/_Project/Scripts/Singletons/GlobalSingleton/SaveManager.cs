using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : SingletonBase<SaveManager>
{
    [SerializeField] private List<MonoBehaviour> saveDataObjects = new List<MonoBehaviour>(); // Перетаскиваем в Inspector
    
    public void SaveAll(string slot = "default")
    {
        try
        {
            var allData = new Dictionary<string, string>();
            foreach (var dataObject in saveDataObjects)
            {
                string id = dataObject.GetType().Name; // Используем имя типа
                string json = JsonUtility.ToJson(dataObject);
                allData[id] = json;
                Debug.Log($"Serialized {id}: {json}");
            }

            var saveFile = new SaveFile
            {
                version = 1
            };
            saveFile.data.FromDictionary(allData); // Конвертируем Dictionary в обертку

            string fullJson = JsonUtility.ToJson(saveFile);

            File.WriteAllText(GetSavePath(slot), fullJson);

#if UNITY_WEBGL
            Application.ExternalEval("syncfs()");
#endif

            Debug.Log($"Game state saved to slot: {slot}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save: {e.Message}");
        }
    }

    public void LoadAll(string slot = "default")
    {
        try
        {
            string path = GetSavePath(slot);
            if (File.Exists(path))
            {
                string fullJson = File.ReadAllText(path);
                var saveFile = JsonUtility.FromJson<SaveFile>(fullJson);
                
                if (saveFile.version == 1)
                {
                    var loadedData = saveFile.data.ToDictionary(); // Конвертируем обратно в Dictionary
                    foreach (var dataObject in saveDataObjects)
                    {
                        string id = dataObject.GetType().Name;
                        if (!loadedData.TryGetValue(id, out string json)) 
                            continue;
                        JsonUtility.FromJsonOverwrite(json, dataObject);
                        Debug.Log($"Loaded {id}: {json}");
                    }
                    
                    Debug.Log($"Game state loaded from slot: {slot}");
                }
                else
                {
                    Debug.LogWarning($"Unsupported save file version: {saveFile.version}");
                }
            }
            else
            {
                Debug.Log($"No save file found for slot: {slot}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load: {e.Message}");
        }
    }
    
    protected override void Awake()
    {
        base.Awake();
        if (saveDataObjects.Count == 0)
        {
            Debug.LogWarning("SaveManager: No objects assigned to saveDataObjects. Nothing will be saved.");
        }

        LoadAll();
    }

    private string GetSavePath(string slot)
    {
        return Path.Combine(Application.persistentDataPath, $"gameSave_{slot}.json");
    }

    [System.Serializable]
    private class SaveFile
    {
        public int version = 1;
        public SerializableDictionary data = new SerializableDictionary(); // Теперь используем обертку
    }

    [System.Serializable]
    private class SerializableDictionary
    {
        public List<string> keys = new List<string>();
        public List<string> values = new List<string>();

        // Метод для конвертации из Dictionary в эту обертку
        public void FromDictionary(Dictionary<string, string> dict)
        {
            keys.Clear();
            values.Clear();
            foreach (var kvp in dict)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        // Метод для конвертации обратно в Dictionary
        public Dictionary<string, string> ToDictionary()
        {
            var dict = new Dictionary<string, string>();
            for (int i = 0; i < keys.Count; i++)
            {
                dict[keys[i]] = values[i];
            }
            return dict;
        }
    }
}