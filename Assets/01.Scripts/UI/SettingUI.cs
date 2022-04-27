using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using DG.Tweening;

public class SettingUI : MonoBehaviour
{

    public RenderPipelineAsset[] qualityLevel;

    //option은 setting 안에서 움직이는 인덱스 Ex => settingIndex 1, 
    //optionIndex 3
    //지금 settingPanel 기준으로 resolution에 3번쨰 세팅

    [SerializeField] private int _settingIndex;
    [SerializeField] private int _optionIndex;
    
    Text _resolutionTxt;
    Text _qualityTxt;

    public void SetResolution()
    {
        switch(_optionIndex)
        {
            case 0: _resolutionTxt.text = string.Format("{0} * {1}",1920, 1080);
            break;
        }
        
    }
    public  void ChangeQuality(int value)
    {
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = qualityLevel[value];
    }

}
