using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipScript : MonoBehaviour,  IPointerEnterHandler
{       
    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip._instance.Show("SSSSSSSSSSSSSSSSS");
    }
}
