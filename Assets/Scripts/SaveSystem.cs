using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveConfig()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/config.cof";
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            ConfigData data = new ConfigData();
            formatter.Serialize(stream, data);
        }
    }

    public static void LoadConfig()
    {
        string path = Application.persistentDataPath + "/config.cof";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                ConfigData data = formatter.Deserialize(stream) as ConfigData;
                Config.Load(data);
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return;
        }
    }
}
