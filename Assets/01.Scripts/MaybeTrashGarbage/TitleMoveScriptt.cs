using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TitleMoveScriptt : MonoBehaviour
{ // 0: ��ŸƮ ��ư . 1: ���׷��̵� ��ư, 2: ��� ��ư , 3: ���� ��ư, 4: ���� ��ư, 5: ���� ��ư, 6: ����ȭ��
    public Button[] titleActionBtn;
    //0: start��ư ������ �� ����Ǵ� Ÿ�Ӷ���, 1: �ڷΰ��� �������� ����Ǵ� Ÿ�Ӷ���, 2: ���׷��̵� �������� ����Ǵ� Ÿ�Ӷ��� , 3:���׷��̵忡�� �ڷΰ���, 4: �� Ÿ�Ӷ���, 5:������
    public PlayableDirector[] timelines;
    public GameObject blurPanel;

    [Header("��ư �׷�")]
    [SerializeField] private GameObject btnGroup;
    [SerializeField] private RectTransform btnGroupRect;
    [SerializeField] private GameObject backBtn;
    [SerializeField] private Button repairBtn;
    [SerializeField] private GameObject resultPanel;
    private RectTransform resultPanelRect;

    public static int indexNum = 0;

    public GameObject startC;
    public GameObject turretC;
    public GameObject turretP;
    public GameObject towerP;
    private void Awake()
    {
        repairBtn = repairBtn.GetComponent<Button>();
        btnGroupRect = btnGroup.GetComponent<RectTransform>();
        resultPanelRect = resultPanel.GetComponent<RectTransform>();

        //���۹�ư
        titleActionBtn[0].onClick.AddListener(() => {
            timelines[0].Play();
            indexNum = 1;
            BtnSlide(1);
            TitleUII.UI.ReadySetUpPanel(1);
        });

        //���׷��̵� ��ư
        titleActionBtn[1].onClick.AddListener(() => {
            timelines[2].Play();
            indexNum = 2;
            BtnSlide(1);
        });
        //��� ��ư
        titleActionBtn[2].onClick.AddListener(() => {
            blurPanel.SetActive(true);
            timelines[4].Play();
            indexNum = 3;
            BtnSlide(1);
            backBtn.SetActive(true);
        });
        //�÷��� ��ư
        titleActionBtn[3].onClick.AddListener(() => {
            blurPanel.SetActive(true);
            timelines[4].Play();
            indexNum = 3;
            BtnSlide(1);
        });
        //���� ��ư
        titleActionBtn[4].onClick.AddListener(() => {
            blurPanel.SetActive(true);
            timelines[4].Play();
            indexNum = 3;
            BtnSlide(1);
        });
        //���� ��ư
        titleActionBtn[5].onClick.AddListener(() => {
            indexNum = 3;
            BtnSlide(1);
            Application.Quit();
        });
        //����ȭ�� ��ư
        titleActionBtn[6].onClick.AddListener(() => {
            if (indexNum == 1)
            {
                TitleUII.UI.ReadySetUpPanel(2);
                timelines[1].Play();
                BtnSlide(2);

            }
            if (indexNum == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    TitleUII.UI.upGradeBtns[i].gameObject.SetActive(false);
                }

                timelines[3].Play();
                BtnSlide(2);
            }
            if (indexNum == 3)
            {
                timelines[5].Play();
                BtnSlide(3);
            }
            //���׷��̵� ȭ�鿡�� ������
            if (indexNum == 4)
            {
                for (int i = 0; i < 3; i++)
                {
                    TitleUII.UI.upGradePanels[i].SetActive(false);
                    TitleUII.UI.upGradeBtns[i].gameObject.SetActive(true);
                }
                indexNum = 2;

            }



        });

        //������ư ��������
        repairBtn.onClick.AddListener(() =>
        {
            btnGroup.SetActive(true);
            resultPanelRect.DOAnchorPosX(1742, 0.8f);
            btnGroupRect.DOAnchorPosX(70, 0.5f);


            TestDatabase.Instance.resultEXP = 0;
            TestDatabase.Instance.resultDamage = 0;
        });

        if (InGameUI.sceneIndex == 1)
        {
            btnGroup.SetActive(false);
            btnGroupRect.DOAnchorPosX(-600, 0.1f);
            resultPanel.SetActive(true);
        }
        if (InGameUI.sceneIndex == 0)
        {
            btnGroup.SetActive(true);
            resultPanel.SetActive(false);
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (backBtn.activeSelf == true)
            {
                if (indexNum == 1)
                {
                    timelines[1].Play();
                    BtnSlide(2);
                }
                if (indexNum == 2)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        TitleUII.UI.upGradeBtns[i].gameObject.SetActive(false);
                    }

                    timelines[3].Play();
                    BtnSlide(2);
                }
                if (indexNum == 3)
                {
                    timelines[5].Play();
                    BtnSlide(3);
                }
                //���׷��̵� ȭ�鿡�� ������
                if (indexNum == 4)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        TitleUII.UI.upGradePanels[i].SetActive(false);
                        TitleUII.UI.upGradeBtns[i].gameObject.SetActive(true);
                    }
                    indexNum = 2;
                }
            }
        }

        if (startC.activeSelf == true || turretC.activeSelf == true || turretP.activeSelf == true || towerP.activeSelf == true)
        {
            backBtn.SetActive(true);
        }
        else
        {
            backBtn.SetActive(false);
        }

    }

    /// <summary>��� ��ư ��ȣ�ۿ� ����</summary>
    public void BtnSlide(int index)
    {
        if (index == 1)
        {
            BtnGroupMove(0);
            backBtn.SetActive(true);

        }
        if (index == 2 || Input.GetKeyDown(KeyCode.Escape))
        {
            BtnGroupMove(1);
            backBtn.SetActive(false);
        }
        if (index == 3 || Input.GetKeyDown(KeyCode.Escape))
        {
            BtnGroupMove(2);
            backBtn.SetActive(false);
        }
    }

    /// <summary>
    ///��ư �׷� �̵���Ű�� �⺻ �Լ� 0: ������ ������, 1: ������ ������
    ///</summary>
    public void BtnGroupMove(int index)
    {
        float dexSpeed = 0;
        if (index == 0)
        {
            btnGroupRect.DOAnchorPosX(-578, 0.5f);
        }
        //��� ���׷��̵� ��
        if (index == 1)
        {
            dexSpeed = 0.5f;
            btnGroupRect.DOAnchorPosX(70, dexSpeed).SetDelay(0.5f);
        }
        //������ ��ư 3�� ��
        if (index == 2)
        {
            dexSpeed = 0.3f;
            btnGroupRect.DOAnchorPosX(70, dexSpeed).SetDelay(0.4f);

        }

    }
}
