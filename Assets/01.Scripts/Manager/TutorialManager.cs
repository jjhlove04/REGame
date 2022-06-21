using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager _instance = new TutorialManager();
    public GameObject ba;
    public GameObject firstUI;
    private RectTransform firstUIRect;
    public Text welcomeTxt;
    public Text mainTxt;
    public Button nextBtn;
    private RectTransform nextBtnRect;

    public GameObject[] unmaskPanels;
    public bool isFirstTutorial = true;
    public int processIndex = 1;
    private void Awake() {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        firstUI.transform.localScale = Vector3.zero; 
        nextBtnRect = nextBtn.GetComponent<RectTransform>();
        firstUIRect = firstUI.GetComponent<RectTransform>();
    }
    private void Start() {
        TitleTuto();
        nextBtn.onClick.AddListener(()=>
        {
            ProcessTutorial(processIndex);
            processIndex ++;
        });
    }

    public void TitleTuto()
    {
        firstUI.transform.DOScale(new Vector3(1,1,1),0.6f).OnComplete(()=>
        {
            welcomeTxt.DOText("Welcome ! , Start Tutorial", 3f);
            nextBtnRect.DOAnchorPosY(-79, 0.6f).SetLoops(-1, LoopType.Yoyo);
        });
    }
    public void ProcessTutorial(int num)
    {
        if(num == 1)
        {
            welcomeTxt.enabled = false;
            ba.SetActive(false);
            firstUIRect.DOAnchorPosX(300, 0.8f).SetEase(Ease.OutExpo);
            mainTxt.DOText("Press the start button", 2);
            unmaskPanels[0].SetActive(true);
        }
        if(num == 2)
        {
            firstUIRect.DOLocalMoveX(-551,1.2f);
            unmaskPanels[1].SetActive(false);
            unmaskPanels[2].SetActive(true);
            mainTxt.DOText("This is a store. You can pay for gold and use items.", 3.5f);
        }
        if(num == 3)
        {
        }
        Debug.Log(processIndex);
    }
    
}
