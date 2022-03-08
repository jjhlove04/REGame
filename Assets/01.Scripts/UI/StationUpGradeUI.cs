using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StationUpGradeUI : MonoBehaviour
{
   [SerializeField] private GameObject[] panelGroup;
   [SerializeField] private Button[] buttonGroup;
   [SerializeField] private Button trainCountUp;
   [SerializeField] private Button trainHpUp;
   [SerializeField] private Button startBtn;

   [SerializeField] private Text countCurLevel;
   [SerializeField] private Text hpCurLevel;

   [SerializeField]private float upHp = 100;
   [SerializeField]private int index = 0;

    async void Start()
    {
        startBtn.onClick.AddListener(()=> {
            LoadingSceneUI.LoadScene("Main");
        });
        trainCountUp.onClick.AddListener(()=> {
            TrainManager.instance.maxTrainCount++;
            MaxCountText();
        });
        trainHpUp.onClick.AddListener(()=> {
            TrainScript.instance.maxTrainHp += upHp;
            MaxHpText();
        });
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(index);
        }

    }
    private void Update() {
        PanelOpen();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(index);
        }
    }

    public void MaxCountText()
    {
        countCurLevel.text = string.Format("{0} -> {1}",TrainManager.instance.maxTrainCount,TrainManager.instance.maxTrainCount+1);
    }
     public void MaxHpText()
    {
         hpCurLevel.text = string.Format("{0} -> {1}",TrainScript.instance .maxTrainHp,TrainScript.instance .maxTrainHp + upHp);
    }

    public  void PanelOpen()
    {
            buttonGroup[0].onClick.AddListener(()=>
            {
                index = 1;
                for(int i =0; i < 4; i++)
                {
                    panelGroup[i].SetActive(false);
                }
                 panelGroup[index - 1].SetActive(true);
            });

            buttonGroup[1].onClick.AddListener(()=>
            {
                index = 2;
                for(int i =0; i < 4; i++)
                {
                    panelGroup[i].SetActive(false);
                }
                 panelGroup[index - 1].SetActive(true);
            });

            buttonGroup[2].onClick.AddListener(()=>
            {
                index = 3;
                for(int i =0; i < 4; i++)
                {
                    panelGroup[i].SetActive(false);
                }
                 panelGroup[index - 1].SetActive(true);
            });

            buttonGroup[3].onClick.AddListener(()=>
            {
                index = 4;
                for(int i =0; i < 4; i++)
                {
                    panelGroup[i].SetActive(false);
                }
                 panelGroup[index - 1].SetActive(true);
            });
            
        }
       
        
    }
