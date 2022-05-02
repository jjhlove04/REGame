using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using DG.Tweening;

public class SettingUI : MonoBehaviour
{


    [SerializeField] Dropdown _qualityOption;
    [SerializeField] Dropdown _resolutionOption;
    [SerializeField] private int _optionIndex;
    [SerializeField] private int _resolutionIndex;
    [SerializeField] private RectTransform _settingPanel;

    Text _resolutionTxt;
    Text _qualityTxt;
    private void Awake()
    {
    }
    private void Start()
    {
        _qualityOption.onValueChanged.AddListener(delegate{
            SeeMenu();
        });


            }
    private void Update()
    {
        _optionIndex = _qualityOption.value;
        _resolutionIndex = _resolutionOption.value;
        ChangeQuality(_optionIndex);
        ChangeResolution(_resolutionIndex);
    }
    public void ChangeQuality(int value)
    {
        QualitySettings.SetQualityLevel(value);
    }

    public void SeeMenu()
    {
        _settingPanel.DOAnchorPosY(900, 1f);
        _settingPanel.DOAnchorPosY(0, 1f).SetDelay(1f);
    }
    public void ChangeResolution(int value)
    {
        switch(value)
        {
            case 0:
            Screen.SetResolution(1920,1080, true);
            break;
            case 1:
            Screen.SetResolution(2560,1440, true);
            break;
            case 2:
            Screen.SetResolution(1440,900, false);
            break;
            case 3:
            Screen.SetResolution(1440,1050, false);
            break;
            case 4:
            Screen.SetResolution(1280, 1024, false);
            break;
            case 5:
            Screen.SetResolution(1280, 800, false);
            break;
            case 6:
            Screen.SetResolution(1280, 720, false);
            break;
            case 7:
            Screen.SetResolution(800,600,false);
            break;


        }
    }

}
