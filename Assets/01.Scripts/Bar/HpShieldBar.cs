using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HpShieldBar : MonoBehaviour, IPointerExitHandler,IPointerEnterHandler
{
    public GameObject hpTip;
    public Slider hpBar;

    private void Update() {
        hpBar.value = TrainScript.instance.curTrainHp / TrainScript.instance.curTrainHpMax;
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
