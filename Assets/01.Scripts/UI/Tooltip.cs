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

    public Vector2 anchoredPos;

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
    }

    public void Show(string tooltipText)
    {
        gameObject.SetActive(true);
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
        
        anchoredPos = Input.mousePosition / canvasRectTrm.localScale.x;
        
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
