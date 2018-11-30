using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// D I T   S C R I P T   I S   G E C O P I E D   V A N   E E N   O U D   P R O J E C T !
public static class SaveLoad
{

    public static void SaveManager(SaveData manager)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        ManagerData data = new ManagerData(manager);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static uint LoadManager()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

			ManagerData data = bf.Deserialize(stream) as ManagerData;

			stream.Close();
			return data.lives;
        } else {
			//Debug.LogError("File does not exist.");
			return 10;
		}
    }
}
[Serializable]
public class ManagerData
{
    public uint lives;

    public ManagerData(SaveData manager)
    {
        lives = manager.lives;
    }
}
