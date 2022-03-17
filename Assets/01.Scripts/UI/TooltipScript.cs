using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipScript : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
{       
    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip._instance.Show("");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
