using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    public Button[] commonItem;
    public GameObject detailPanel;
    public Button closeBtn;
    public Text itemNameText;
    public Text rankText;
    public Text effectiveText;
    

    private void Awake() {
        detailPanel.transform.localScale = Vector3.zero;
        closeBtn.onClick.AddListener(()=> detailPanel.transform.DOScale(Vector3.zero, 0.5f));
    }

    private void Start() {
        for(int i = 0; i < this.commonItem.Length; i++)
        {
            int itemIndex = i;
            commonItem[i].onClick.AddListener(()=> this.CommonCollection(itemIndex));
        }
        

    }
    public void CommonCollection(int index){
        detailPanel.transform.DOScale(new Vector3(1,1,1),0.5f);
        itemNameText.text = string.Format("-Order : <color=#A4A4A4>{0}</color>",ItemDictionary._instance.itemContainerCom[index].Item1);
        effectiveText.text = string.Format("-Effective : {0}", ItemDictionary._instance.itemContainerCom[index].Item2);
        rankText.text = string.Format("-Rank : <color=blue>{0}</color>",ItemDictionary._instance.itemContainerCom[index].Item3);

    //     if(index == 0)
    //     {
    //         detailPanel.transform.DOScale(new Vector3(1,1,1),0.5f);
    //         rankText.text = string.Format("-Rank : <color=White>Common</color>");
    //         itemNameText.text = string.Format("- Order : <color=#A4A4A4>Red Nut</color>");
    //         effectiveText.text = string.Format("-Effective : 8% chance to produces two nuts that heal 3 stamina (OverLap Stamina +3)");
    //     }
    //     if(index == 1)
    //     {
    //         detailPanel.transform.DOScale(new Vector3(1,1,1),0.5f);
    //         rankText.text = string.Format("-Rank : <color=White>Common</color>");
    //         itemNameText.text = string.Format("- Order : <color=#A4A4A4>Explosive Shield</color>");
    //         effectiveText.text = string.Format("-Effective : Damage of up to 15% of the maximum stamina within 0.7 seconds causes about 200% of explosions around it. (OverLap Damage +20%)");
    //     }
    //     if(index == 2)
    //     {
    //         detailPanel.transform.DOScale(new Vector3(1,1,1),0.5f);
    //         rankText.text = string.Format("-Rank : <color=White>Common</color>");
    //         itemNameText.text = string.Format("- Order : <color=#A4A4A4>Weak Lens</color>");
    //         effectiveText.text = string.Format("-Effective :In the event of an attack, a 10% chance of a fatal blow occurs and causes double damage. (OverLap Damage +7%)");
    //     }
    //     if(index == 3)
    //     {
    //         detailPanel.transform.DOScale(new Vector3(1,1,1),0.5f);
    //         rankText.text = string.Format("-Rank : <color=White>Common</color>");
    //         itemNameText.text = string.Format("- Order : <color=#A4A4A4>CoolDown</color>");
    //         effectiveText.text = string.Format("-Effective : more than two turrets that have used up bullets in three seconds, 20 stamina per second is restored (OverLap Stamina +5)");
    //     }
    //     if(index == 4)
    //     {
    //         detailPanel.transform.DOScale(new Vector3(1,1,1),0.5f);
    //         rankText.text = string.Format("-Rank : <color=White>Common</color>");
    //         itemNameText.text = string.Format("- Order : <color=#A4A4A4>Annuity</color>");
    //         effectiveText.text = string.Format("-Effective : Create 1 gold every 8 seconds (OverLap Glod +1)");
    //     }
    //     if(index == 5)
    //     {
    //         detailPanel.transform.DOScale(new Vector3(1,1,1),0.5f);
    //         rankText.text = string.Format("-Rank : <color=White>Common</color>");
    //         itemNameText.text = string.Format("- Order : <color=#A4A4A4>Wire Entanglement</color>");
    //         effectiveText.text = string.Format("-Effective : Deal 50 percent damage per second to nearby enemies More enemies within range, damage single targets and ignore defenses (OverLap Range + 20%, OverLap Damage + 10%)");
    //     }
    //     if(index == 6)
    //     {
    //         detailPanel.transform.DOScale(new Vector3(1,1,1),0.5f);
    //         rankText.text = string.Format("-Rank : <color=White>Common</color>");
    //         itemNameText.text = string.Format("- Order : <color=#A4A4A4>Hemostatic</color>");
    //         effectiveText.text = string.Format("-Effective :In attack, 15% chance of bleeding the enemy, causing 35% damage 4 times in 2 seconds (OverLap Chance +15%)");
    //     }
    //     if(index == 7)
    //     {
    //         detailPanel.transform.DOScale(new Vector3(1,1,1),0.5f);
    //         rankText.text = string.Format("-Rank : <color=White>Common</color>");
    //         itemNameText.text = string.Format("- Order : <color=#A4A4A4>Spanner</color>");
    //         effectiveText.text = string.Format("-Effective : 1.2 increase in basic physical recovery per second (OverLap Restore +1.2)");
    //     }
    }
}