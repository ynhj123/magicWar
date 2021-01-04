using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileUtils 
{
   
    public static void Save(string path,object data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Create);
        bf.Serialize(file, data);
        file.Close();
    }

    public static T Load<T>(string path)
    {
        if (!File.Exists(path))
        {
            return default(T);
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        T t = (T)bf.Deserialize(file);
        file.Close();
        return t;
    }
}
