using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HpShieldBar : MonoBehaviour, IPointerExitHandler,IPointerEnterHandler
{
    public GameObject hpTip;
    public Text hpText;
    float curHp;
    float maxHp;
    public Slider hpBar;

    private void Update() {
        curHp = TrainScript.instance.curTrainHp;
        maxHp = TrainScript.instance.curTrainHpMax;
        hpBar.value = TrainScript.instance.curTrainHp / TrainScript.instance.curTrainHpMax;
        hpText.text = $"{curHp} / <color=yellow>{maxHp}</color>";
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        hpTip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hpTip.SetActive(false);
    }
    
}
