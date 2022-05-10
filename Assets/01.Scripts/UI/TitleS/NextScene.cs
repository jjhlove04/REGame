using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class NextScene : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject titlePanel;
    

    private void Start() {
        startButton.onClick.AddListener(()=> LoadingSceneUI.LoadScene("Station"));     
        settingButton.onClick.AddListener(()=> OpenSetting());     
       backButton.onClick.AddListener(()=> BackToTitle());     
    }

    void OpenSetting()
    {
        titlePanel.transform.DOMoveX(-295,1.5f);
        settingPanel.transform.DOMoveX(295,1.5f);
    }
    void BackToTitle()
    {
        titlePanel.transform.DOMoveX(295,1.5f);
        settingPanel.transform.DOMoveX(870,1.5f);
    }
}
