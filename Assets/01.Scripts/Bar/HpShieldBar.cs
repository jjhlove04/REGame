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
    public Slider hpBar;
    public Slider shieldBar;
    float hpGauge;
    float shieldGauge;
    public TrainInfo trainInfo;

    private void Start() {
    }
    private void Update() {
        hpGauge =  (TrainScript.instance.CurTrainHp/TrainScript.instance.CurTrainHpMax);
        //shieldGauge = 1 - TrainScript.instance.curTrainShield/ trainInfo.trainMaxShield;
        curHp = (int)(TrainScript.instance.CurTrainHp);
        maxHp = (int)(TrainScript.instance.CurTrainHpMax);
        hpText.text = $"{curHp} / <color=yellow>{maxHp}</color>";
        hpLogic();
    }

    public void hpLogic()
    {
        if(TrainScript.instance.curTrainShield > 0)
        {
            hpBar.value = Mathf.Lerp(  shieldGauge , hpBar.value, 1 *Time.deltaTime);
            shieldBar.gameObject.SetActive(true);
            
        }
        if(TrainScript.instance.curTrainShield < 0)
        {
            hpBar.value = Mathf.Lerp(  hpGauge , hpBar.value, 1 * Time.deltaTime);
            shieldBar.gameObject.SetActive(false);
        }
        

    }
    
}
