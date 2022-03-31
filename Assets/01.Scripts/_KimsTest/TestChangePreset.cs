using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestChangePreset : MonoBehaviour
{
    public List<Button> presetBtn = new List<Button>();
    public List<CanvasGroup> presetCanvas = new List<CanvasGroup>();



    private void Start()
    {
        presetBtn[0].onClick.AddListener(() =>
        {
            onCan(0);
            onBtn(0);
        });
        presetBtn[1].onClick.AddListener(() =>
        {
            onCan(1);
            onBtn(1);
        });
        presetBtn[2].onClick.AddListener(() =>
        {
            onCan(2); 
            onBtn(2);
        });
        presetBtn[3].onClick.AddListener(() =>
        {
            onCan(3);
            onBtn(3);
        });
        presetBtn[4].onClick.AddListener(() =>
        {
            onCan(4);
            onBtn(4);
        });
    }

    void onCan(int count)
    {
        for (int i = 0; i < presetCanvas.Count; i++)
        {
            if (presetCanvas[i] == presetCanvas[count])
            {

                presetCanvas[i].alpha = 1;
                presetCanvas[i].interactable = true;
                presetCanvas[i].blocksRaycasts = true;
            }
            else
            {
                presetCanvas[i].alpha = 0;
                presetCanvas[i].interactable = false;
                presetCanvas[i].blocksRaycasts = false;
            }
        }
    }

    void onBtn(int count)
    {
        for (int i = 0; i < presetBtn.Count; i++)
        {

            if (presetBtn[i] == presetBtn[count])
            {
                presetBtn[i].interactable = false;
            }
            else
            {
                presetBtn[i].interactable = true;
            }
        }
    }
}
