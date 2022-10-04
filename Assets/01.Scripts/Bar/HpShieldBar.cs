using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HpShieldBar : MonoBehaviour{
    public GameObject hpTip;
    public Text hpText;
    int curHp;
    int maxHp;
    int curSd;
    int maxSd;
    public Slider hpBar;
    public Slider shieldBar;
    float hpGauge;
    float shieldGauge;
    public TrainInfo trainInfo;

    private void Start() {
        
    }
    private void Update() {
        hpGauge =  (TrainScript.instance.CurTrainHp/TrainScript.instance.CurTrainHpMax);
        shieldGauge = TrainScript.instance.CurTrainShield/ trainInfo.trainMaxShield;
        curHp = (int)(TrainScript.instance.CurTrainHp);
        maxHp = (int)(TrainScript.instance.CurTrainHpMax);
        curSd = (int)(TrainScript.instance.CurTrainShield);
        maxSd = (int)(trainInfo.trainMaxShield);
        hpText.text = $"{curHp} / <color=red>{maxHp}</color>     <color=#008AD6>{curSd}/{maxSd}</color>";
        hpLogic();
        
    }

    public void hpLogic()
    {
        
            hpBar.value = Mathf.Lerp(hpBar.value,hpGauge , 5 * Time.deltaTime);
            shieldBar.value = Mathf.Lerp(shieldBar.value, shieldGauge , 5 * Time.deltaTime);
            
        
        

    }
    
}
