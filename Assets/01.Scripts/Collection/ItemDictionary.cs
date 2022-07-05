using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    public static ItemDictionary _instance = new ItemDictionary();
    
    //키값, item1 = 아이템 이름, item2 = 아이템 설명, item3 = 등급
    public Dictionary<int, (string,string,string)> itemContainerCom = new Dictionary<int, (string,string,string)>()
    {
        {0,("Red Nut", "8% chance to produces two nuts that heal 3 stamina (OverLap Stamina +3)","Common")},
        {1,("Explosive Shield", "Damage of up to 15% of the maximum stamina within 0.7 seconds causes about 200% of explosions around it. (OverLap Damage +20%)","Common")},
        {2,("Weak Lens", "In the event of an attack, a 10% chance of a fatal blow occurs and causes double damage. (OverLap Damage +7%)","Common")},
        {3,("CoolDown", "more than two turrets that have used up bullets in three seconds, 20 stamina per second is restored (OverLap Stamina +5)","Common")},
        {4,("Annuity", "Create 1 gold every 8 seconds (OverLap Glod +1)","Common")},
        {5,("Wire Entanglement", "Deal 50 percent damage per second to nearby enemies More enemies within range, damage single targets and ignore defenses (OverLap Range + 20%, OverLap Damage + 10%)","Common")},
        {6,("Hemostatic", "In attack, 15% chance of bleeding the enemy, causing 35% damage 4 times in 2 seconds (OverLap Chance +15%)","Common")},
        {7,("Spanner", "1.2 increase in basic physical recovery per second (OverLap Restore +1.2)","Common")},
    };
    Dictionary<int,(string,string,string)> itemContainerRare = new Dictionary<int, (string, string, string)>()
    {

    };

    private void Awake() {
        _instance = this;
    }
}
