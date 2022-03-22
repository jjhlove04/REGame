using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAspectPin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetResolution();
    }
    ///<summary>
    ///해상도 설정하는 함수
    ///</summary>

    public void SetResolution()
    {
        Debug.Log("SetResolution");
        int setWidth = 1920;
        int setHeight = 1080;

        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        //setresolution함수 제대로 사용하기
        Screen.SetResolution(setWidth, (int)(((float)deviceHeight/ deviceWidth)* setWidth), true);
        
        //기기의 해상도 비가 더 큰 경우 기기 > 게임화면
        if((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); //새로운 너비
            Camera.main.rect = new Rect((1f - newWidth)/2f,0f,newWidth,1f); //새로운 Rect적용
        }
        else //게임의 해상도가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight);//새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); //새로운 Rect적용
        }
    }
}
