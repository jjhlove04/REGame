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
    public bool openPanel = false;
    public List<Button> panelOpenList = new List<Button>();

    private void Start() {
        
    }

    //패널 오픈 함수
    public void UpGradePanelOpen()
    {
        if(openPanel == false)
        {
            upGradePanel.transform.DOMoveX(130, 1.2f);
        }
        if(openPanel)
        {
            upGradePanel.transform.DOMoveX(-130, 1.2f);

        }
    }

}
