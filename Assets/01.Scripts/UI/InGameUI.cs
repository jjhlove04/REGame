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
    [SerializeField] private Button mainMenuBtn;


    // Start is called before the first frame update
    void Start()
    {
        mainMenuBtn.onClick.AddListener(()=>
        {
            num *= -1;

            OpenMenu();
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            index = index * -1;
            OpenBluePrint(index);
        }
    }

    public void OpenBluePrint(int index)
    {
        if(index == -1)
        {
            bluePrintBot.transform.DOMoveY(100,1.5f).SetEase(Ease.OutQuart);
            bluePrintTop.transform.DOMoveY(410,1.5f).SetEase(Ease.OutQuart);
        }
        if(index == 1)
        {
            bluePrintTop.transform.DOMoveY(700,1.5f).SetEase(Ease.OutQuart);
            bluePrintBot.transform.DOMoveY(-100,1.5f).SetEase(Ease.OutQuart);
            
        }
    }
    
    public void OpenMenu()
    {
        if(num == -1)
        {
        menuBtn[0].SetActive(true);
        menuBtn[1].SetActive(true);
        menuBtn[2].SetActive(true);
        menuBtn[3].SetActive(true);
        menuBtn[0].transform.DOMoveY(470,1);
        menuBtn[1].transform.DOMoveY(450,1);
        menuBtn[2].transform.DOMoveY(430,1);
        menuBtn[3].transform.DOMoveY(410,1);
        }
        
        if(num == 1)
        {
            for(int i = 0; i < 4; i++)
            {
                menuBtn[i].transform.DOMoveY(500,1);
                menuBtn[i].SetActive(false);
            }
        }
        
    }
}
