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

    private int level = 1;
    public int curExp = 0;
    public int maxExp = 7;
    private int tp;
    public Slider expBar;
    public Text levelTxt;
    //public Text techPointTxt;
    //public Text goldTxt;

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

        curExp += TestTurretDataBase.Instance.resultEXP;
    }
    private void Update()
    {
        Update_MousePosition();

        ExpBar();
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

    private void ExpBar()
    {
        expBar.value = Mathf.Lerp(expBar.value, (float)curExp / (float)maxExp, Time.deltaTime * (2 +(curExp / 500)));

        if (expBar.value >= 0.99f)
        {
            if (curExp >= maxExp)
            {
                level++;
                curExp = curExp - maxExp;
                maxExp += maxExp;
                expBar.value = 0;
                levelTxt.text = level.ToString();
            }
        }
    }
}