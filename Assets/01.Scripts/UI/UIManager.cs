using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
     
    private static UIManager _ui = new UIManager();
    public static UIManager UI { get { return _ui; } }

    public Button[] buyBtns; 
   public RectTransform trs_cursor;
    public Text text_mouse;
    public Slider hpBar;




    private void Awake()
    {
        _ui = this;
       for(int i = 0; i < 7; i++)
       {
          buyBtns[i].gameObject.AddComponent<TooltipScript>();
       }
    }
    private void Update()
    {
        //Update_MousePosition();
    }
        
    private void Start()
    {
        //hp바 세
        //hpBar.value = (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp;
        hpBar.value = (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp;
    }
    
    private void Update_MousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
       
        // float w = trs_icon.rect.width;
        // float h = trs_icon.rect.height;
        // trs_icon.position = trs_cursor.position + (new Vector3(w, h) * 0.5f);

        string message = mousePos.ToString();
        //text_mouse.text = message;
        Debug.Log(message);
    }
    //패널 오픈 함수
  

    public void PanelMove(GameObject panel, float endValue)
    {
        panel.transform.DOMoveZ(endValue, 1.2f);
    }
    // public void TakeDamageHpBar()
    // {
    //     //Time.deltaTime 옆에 * (TakeDamage) 만큼 곱해줘야함. 생략되어 있음.
    //     hpBar.value = Mathf.Lerp(hpBar.value, (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp, Time.deltaTime);
    // }
   

}
    

