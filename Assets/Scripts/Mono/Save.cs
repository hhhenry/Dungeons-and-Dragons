using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Save
{
    public int level = 1;
    public int playerLevel = 1;
    public List<int> equippedEquipment = new();

    public void Equip(int equipmentId)
    {
        equippedEquipment.Add(equipmentId);
        // TODO: Apply the bonuses of the new equipment to the player's attributes
    }
    public void SaveByJSON(Save save)
    {
        string JsonString = JsonUtility.ToJson(save);
        StreamWriter sw = new(Application.persistentDataPath+"/save.yj");
        sw.Write(JsonString);
        sw.Close();
    }

    public void LoadByJSON()
    {
        if(File.Exists(Application.persistentDataPath+"/save.yj"))
        {
            StreamReader sr = new(Application.persistentDataPath+"/save.yj");
            string JsonString = sr.ReadToEnd();
            sr.Close();
            Save save = JsonUtility.FromJson<Save>(JsonString);
            level = save.level;
            playerLevel = save.playerLevel;
            equippedEquipment = save.equippedEquipment;
        }
        else{
            Save save = new();
            save.SaveByJSON(save);
        }
    }
}