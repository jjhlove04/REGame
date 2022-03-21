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
   //public RectTransform trs_cursor;
    
    public Button startBtn;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _ui = this;
       for(int i = 0; i < 7; i++)
       {
          buyBtns[i].gameObject.AddComponent<TooltipScript>();
       }
       startBtn.onClick.AddListener(() => {
        LoadingSceneUI.LoadScene("Main");
       });
    }
    private void Update()
    {
        Update_MousePosition();
    }
        
    private void Start()
    {

        //hp바 세
        //hpBar.value = (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp;
        //speedBtn.onClick.AddListener(ChangeSpeed);
        //hpBar.value = (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp;
        

    }
    
    
    
    //패널 오픈 함수
    

    private void Update_MousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        //{"Wav {0}"}
    }
    // public void TakeDamageHpBar()
    // {
    //     //Time.deltaTime 옆에 * (TakeDamage) 만큼 곱해줘야함. 생략되어 있음.
    //     hpBar.value = Mathf.Lerp(hpBar.value, (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp, Time.deltaTime);
    // }

    // public void ChangeSpeed()
    // {
    //     speedBtnCount++;
    //     switch (speedBtnCount)
    //     {
    //         case 0:
    //             GameManager.Instance.gameSpeed = 1f;
    //             speedTxt.text = GameManager.Instance.gameSpeed + "X";
    //             break;
    //         case 1:
    //             GameManager.Instance.gameSpeed = 1.5f;
    //             speedTxt.text = GameManager.Instance.gameSpeed + "X";
    //             break;
    //         case 2:
    //             GameManager.Instance.gameSpeed = 2f;
    //             speedTxt.text = GameManager.Instance.gameSpeed + "X";
    //             break;
    //         case 3:
    //             GameManager.Instance.gameSpeed = 4f;
    //             speedTxt.text = GameManager.Instance.gameSpeed + "X";
    //             speedBtnCount = -1;
    //             break;
    //         default:
    //             break;
    //     }



    }
   
    

