using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class TitleUI : MonoBehaviour
{
     
    private static TitleUI _ui = new TitleUI();
    public static TitleUI UI { get { return _ui; } }

    public Button[] buyBtns; 

    [Header("업그레이드 관련")]
    [SerializeField] public Button[] upGradeBtns; //0번 터렛, 1번 기차, 2번 타워
    [SerializeField] public GameObject[] upGradePanels; //0번 터렛, 1번 기차, 2번 타워
   
    
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
       upGradeBtns[0].onClick.AddListener(()=> 
       {
           RemoveBtn();
           upGradePanels[0].SetActive(true);
           TitleMoveScript.indexNum = 4;
       });
       upGradeBtns[2].onClick.AddListener(()=>
       {
           RemoveBtn();
           upGradePanels[2].SetActive(true);
           TitleMoveScript.indexNum = 4;
       });

       
    }
    private void Update()
    {
        Update_MousePosition();
    }
    
    
    
    //패널 오픈 함수
    public void RemoveBtn()
    {
        for(int i = 0; i < 3; i++)
           {
            upGradeBtns[i].gameObject.SetActive(false);

           }
    }

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
   
    

