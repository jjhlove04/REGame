using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private static UIManager _ui = new UIManager();
    public static UIManager UI { get { return _ui; } }
    [SerializeField] private GameObject upGradePanel;
    [Space]
    [SerializeField] private Slider hpBar;
    [Space]
    public bool openPanel = false;
    private int dex = 1;
    public List<Button> panelOpenList = new List<Button>();

    private void Start() {
        //hp바 세팅
        hpBar.value = (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp;
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.T))
        {
            dex = dex * -1;
        }
        if(dex > 0)
        {
            openPanel = false; //닫기
        }
        if(dex < 0)
        {
            openPanel = true;
        }
        UpGradePanelOpen();
        
    }


    //패널 오픈 함수
    public void UpGradePanelOpen()
    {
        if(openPanel)
        {
            upGradePanel.transform.DOMoveX(130, 1.2f);
        }
        if(openPanel == false)
        {
            upGradePanel.transform.DOMoveX(-130, 1.2f);

        }
    }

    public void TakeDamageHpBar()
    {
        //Time.deltaTime 옆에 * (TakeDamage) 만큼 곱해줘야함. 생략되어 있음.
        hpBar.value = Mathf.Lerp(hpBar.value, (float)TrainScript.instance.curTrainHp / (float)TrainScript.instance.maxTrainHp, Time.deltaTime);
    }

}
