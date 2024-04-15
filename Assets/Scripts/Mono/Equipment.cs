using System.Collections.Generic;
using cfg;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public int Value { get; set; }
    public string Icon { get; set; }
    public string Description { get; set; }
    public Image armor;    
    public Image helmet;   
    public Image gloves;   
    public Image rings;   
    public Image necklace;   
    public Image shoes;   
    public Image belt;   
    public Image weapons;   
    public Equipment equipment;
    
    public void EquipIcon(List<Equipment> equippedEquipment) {
        Debug.LogWarning("icon " + equipment.Icon);
        string icon = "";
        foreach(Equipment equipment in equippedEquipment) {
            switch(equipment.Type) {
                case 1: //armor
                    armor.sprite = Resources.Load<Sprite>(equipment.Icon);
                    break;
                case 2: //armor
                    icon = equipment.Icon;
                    break;
                case 3: //armor
                    icon = equipment.Icon;
                    break;
                case 4: //armor
                    icon = equipment.Icon;
                    break;
                case 5: //armor
                    icon = equipment.Icon;
                    break;
                case 6: //armor
                    icon = equipment.Icon;
                    break;
                case 7: //armor
                    icon = equipment.Icon;
                    break;
                case 8: //armor
                    icon = equipment.Icon;
                    break;
            }
        }
        Debug.LogWarning("icon " + equipment.Icon);
    }
}