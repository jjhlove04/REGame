using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public static Tooltip _instance = new Tooltip();
     [SerializeField] private RectTransform canvasRectTrm;

    private RectTransform rectTrm;

    private RectTransform toolTipBackgroundRectTrm;

    private TooltipTimer tooltipTimer;

    //private TextMeshProUGUI textMeshPro;

    private void Awake() {
        _instance = this;
         Hide();
        rectTrm = this.GetComponent<RectTransform>();
         toolTipBackgroundRectTrm = this.transform.Find("TooltopBackground").GetComponent<RectTransform>(); 
        // textMeshPro = this.transform.Find("text").GetComponent<TextMeshProUGUI>();  
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleFollowMouse();
         if(tooltipTimer != null)
        {
            tooltipTimer.timer -= Time.deltaTime;
            if(tooltipTimer.timer <= 0)
            {
                Hide();
            }
        }
    }

    public void Show(string tooltipText)
    {
        gameObject.SetActive(true);
        SetText(tooltipText);
    }
    void SetText(string tooltipText)
    {
        //textMeshPro.SetText(tooltipText);
        //textMeshPro.ForceMeshUpdate();

        //Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);

        //toolTipBackgroundRectTrm.sizeDelta = textSize + padding;
    }

    public class TooltipTimer
    {
        public float timer;
    }
     public void Hide()
    {
        gameObject.SetActive(false);
    }

     void HandleFollowMouse()
    {
        Vector2 anchoredPos = Input.mousePosition / canvasRectTrm.localScale.x;
         UIManager.UI.trs_cursor.position = anchoredPos;
        if (anchoredPos.x + toolTipBackgroundRectTrm.rect.width > canvasRectTrm.rect.width)
        {
            anchoredPos.x = canvasRectTrm.rect.width - toolTipBackgroundRectTrm.rect.width;
        }

        if (anchoredPos.y + toolTipBackgroundRectTrm.rect.height > canvasRectTrm.rect.height)
        {
            anchoredPos.y = canvasRectTrm.rect.height - toolTipBackgroundRectTrm.rect.height;
        }

        if (anchoredPos.x - toolTipBackgroundRectTrm.rect.width < 0)
        {
            anchoredPos.x = 0;
        }

        if (anchoredPos.y - toolTipBackgroundRectTrm.rect.height < 0)
        {
            anchoredPos.y = 0;
        }

        rectTrm.anchoredPosition = anchoredPos;
    }
}
