using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameUI : MonoBehaviour
{
    //UImanager로 다 옮겨야 함
    [SerializeField] private GameObject bluePrintTop;
    [SerializeField] private GameObject bluePrintBot;

    int index = 1;
    int num = 1;
    [SerializeField] private GameObject[] menuBtn;
    [SerializeField] private RectTransform[] menuBtnRect ;
    private List<RectTransform> menuBtnList = new List<RectTransform>();
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private GameObject btnGroup;
    int screenHeight = Screen.height;
    int screenWidth = Screen.width;

    RectTransform bpTop;
    RectTransform bpBot;

    public Text goldAmounTxt;
    public Text waveTxt;
    private void Awake()
    {
        bpTop = bluePrintTop.GetComponent<RectTransform>();
        bpBot = bluePrintBot.GetComponent<RectTransform>();
        for(int i = 0; i < 4; i++)
        {
            menuBtnRect[i] = menuBtn[i].GetComponent<RectTransform>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        mainMenuBtn.onClick.AddListener(() =>
        {
            num *= -1;

            OpenMenu();
        });

        goldAmounTxt.text = GameManager.Instance.goldAmount.ToString();
        waveTxt.text = "WAVE : " + SpawnMananger.Instance.round.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            index = index * -1;
            OpenBluePrint(index);
        }

        waveTxt.text = "WAVE : " + SpawnMananger.Instance.round.ToString();
    }

    public void OpenBluePrint(int index)
    {
        if (index == -1)
        {
            bpTop.DOAnchorPosY(300, 1.5f).SetEase(Ease.OutQuart);
            bpBot.DOAnchorPosY(-300, 1.5f).SetEase(Ease.OutQuart);
            

        }
        if (index == 1)
        {

            bpTop.DOAnchorPosY(781, 1.5f).SetEase(Ease.OutQuart);
            bpBot.DOAnchorPosY(-789, 1.5f).SetEase(Ease.OutQuart);
        }
    }

    public void OpenMenu()
    {
        if (num == -1)
        {
            btnGroup.SetActive(true);
            menuBtnRect[0].DOAnchorPosY(-60,1f);
            menuBtnRect[1].DOAnchorPosY(-100,1f);
            menuBtnRect[2].DOAnchorPosY(-140,1f);
            menuBtnRect[3].DOAnchorPosY(-180,1f);
        }

        if (num == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                menuBtnRect[i].DOAnchorPosY(0,1).OnComplete(()=>
                btnGroup.SetActive(false)
                );
            }
            
        }

    }
}
