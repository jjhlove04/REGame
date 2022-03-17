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

    //public RectTransform trs_icon;
    //public Text text_mouse;


    private void Awake()
    {
        _ui = this;
       for(int i = 0; i < 7; i++)
       {
          buyBtns[i].gameObject.AddComponent<TooltipScript>();
       }
    }
    private void Start(){

    }
    private void Update()
    {
        //Update_MousePosition();
        
        
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
   

}
    

