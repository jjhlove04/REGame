using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionScript : MonoBehaviour
{
    public static ResolutionScript Instance;
    private static ResolutionScript _instance { get { return Instance; } }

    [Header("해상도")]
    public Dropdown resDropdown;
    public Toggle fullscreenToggle;

    List<Resolution> resolutions = new List<Resolution>();
    public int resNum; //선택한 해상도의 순서번호
    FullScreenMode screenMode;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(Instance);
    }
    void Start()
    {
        Resolutions();
    }

    public void Resolutions()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 144)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
            else if (Screen.resolutions[i].refreshRate == 60)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        
        resDropdown.options.Clear();

        int optionNum = 0;

        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + " X " + item.height + " " + item.refreshRate + "hz";
            resDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
            {
                resDropdown.value = optionNum;
            }
            optionNum++;
        }

        resDropdown.RefreshShownValue();
        fullscreenToggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void UIOptionChange(int x)
    {
        resNum = x;
    }

    //따로 버튼에 연결해야함
    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    //따로 버튼에 연결해야함
    public void OKBtnClick()
    {
        Screen.SetResolution(resolutions[resNum].width, resolutions[resNum].height, screenMode);
    }
}
