using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuffItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InGameUII inGameUII;

    public bool onMouse = false;

    public Color bufColor;

    private bool onHold = false;
    private bool onActive = false;
    private float buffTime;
    private float activeTime;
    private float maxBuffTime = 5f;
    private float maxActiveTime = 5f;
    public string bufString;

    private void Start()
    {
        inGameUII = InGameUII._instance;
    }

    void Update()
    {
        bufColor = this.gameObject.GetComponent<Image>().color;

        ColorUtility.TryParseHtmlString("#92D050", out Color active);
        ColorUtility.TryParseHtmlString("#C9350D", out Color hold);
        //active
        if (bufColor == active)
        {
            onActive = true;
        }

        //hold
        if (bufColor == hold)
        {
            onHold = true;
        }

        if (inGameUII.buffStrPanel.gameObject.activeSelf == true)
        {
            Vector3 mousePos = Input.mousePosition;

            inGameUII.buffStrPanel.transform.position = mousePos;
        }

        if(onHold)
        {
            buffTime += Time.deltaTime;

            gameObject.GetComponent<CanvasGroup>().alpha = (buffTime / maxBuffTime);

            if(buffTime > maxBuffTime)
            {
                onHold = false;
                gameObject.SetActive(false);
            }
        }

        if (onActive)
        {
            activeTime += Time.deltaTime;

            gameObject.GetComponent<CanvasGroup>().alpha = (activeTime / maxActiveTime);

            if (activeTime > maxActiveTime)
            {
                onActive = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inGameUII.buffStrPanel.gameObject.SetActive(true);
        inGameUII.buffStrPanel.transform.GetChild(0).GetComponent<Text>().text = bufString;
        onMouse = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inGameUII.buffStrPanel.gameObject.SetActive(false);
        onMouse = false;
    }
}
