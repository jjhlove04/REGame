using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HpShieldBar : MonoBehaviour, IPointerExitHandler,IPointerEnterHandler
{
    public GameObject hpTip;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        hpTip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hpTip.SetActive(false);
    }
    
}
