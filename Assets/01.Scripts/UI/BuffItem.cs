using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuffItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float buffTime;

    InGameUII inGameUII;
    private void Start()
    {
        inGameUII = InGameUII._instance;
        Color bufColor = this.gameObject.GetComponent<Image>().color;
        
        //active
        if (bufColor == new Color(0.57f, 0.81f, 0.31f))
        {

        }

        //pasive
        if (bufColor == new Color(0.36f, 0.61f, 0.83f))
        {

        }

        //hold
        if (bufColor == new Color(0.78f, 0.2f, 0.05f))
        {

        }
    }

    void Update()
    {
        if(inGameUII.buffStrPanel.gameObject.activeSelf == true)
        {
            Vector3 mousePos = Input.mousePosition;

            inGameUII.buffStrPanel.transform.position = mousePos;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inGameUII.buffStrPanel.gameObject.SetActive(true);     
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inGameUII.buffStrPanel.gameObject.SetActive(false);
    }
}
