using System.IO;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    private class SaveObject
    {
        public int RoomLevel;
        public int Health;
        public int Gold;
    }

    public static void Save()
    {
        var so = new SaveObject
        {
            RoomLevel = PlayerStats.Floor,
            Health = PlayerStats.Health,
            Gold = PlayerStats.Gold
        };
        File.WriteAllText(Application.dataPath + "/save.txt", JsonUtility.ToJson(so));

        print("Saved!");
    }

    public static void Load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            var so = JsonUtility.FromJson<SaveObject>(File.ReadAllText(Application.dataPath + "/save.txt"));

            PlayerStats.Floor = so.RoomLevel;
            PlayerStats.Health = so.Health;
            PlayerStats.Gold = so.Gold;
        }
        else
        {
            PlayerStats.Floor = 1;
            PlayerStats.Health = 3;
            PlayerStats.Gold = 0;
            print("No Save File Present");
        }
    }
}