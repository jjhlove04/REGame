using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using DG.Tweening;

public class SettingUI : MonoBehaviour
{

    [SerializeField] Dropdown _qualityOption;
    [SerializeField] private int _optionIndex;
    [SerializeField] private RectTransform _settingPanel;

    Text _resolutionTxt;
    Text _qualityTxt;
    private void Awake()
    {
        _qualityOption = GameObject.Find("QualitySelect").GetComponent<Dropdown>();
        _settingPanel = GameObject.Find("Setting").GetComponent<RectTransform>();
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
        ChangeQuality(_optionIndex);
    }
    public void ChangeQuality(int value)
    {
        QualitySettings.SetQualityLevel(value);
    }

    public void SeeMenu()
    {
        _settingPanel.DOAnchorPosX(620, 0.5f);
        _settingPanel.DOAnchorPosX(0, 1f).SetDelay(2f);

    }

}
