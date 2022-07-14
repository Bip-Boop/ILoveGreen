using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem<T> where T : class
{
    static SaveSystem()
    { 
        if (!typeof(T).IsSerializable)
        {
            Debug.LogError("type of save data must be serializable, however "
                + typeof(T).ToString() + " is not");
        }
    }

    public static void Save(T saveData, string saveFileName)
    {
        if (!saveData.GetType().IsSerializable)
        {
            Debug.LogError("type of save data must be serializable, however "
                + saveData.GetType().ToString() + " is not");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        string path = $"{Application.persistentDataPath}/{saveFileName}.s";

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            bf.Serialize(fs, saveData);
        }
    }

    public static T Load(string saveFileName)
    {
        string path = $"{Application.persistentDataPath}/{saveFileName}.s";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                T data = bf.Deserialize(fs) as T;
                return data;
            }

        }
        else
        {
            Debug.LogError("Save file not found");
            return default;
        }
    }


}
