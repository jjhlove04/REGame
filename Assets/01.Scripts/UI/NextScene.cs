using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button SettingButton;

    private void Start() {
        StartButton.onClick.AddListener(()=> LoadingSceneUI.LoadScene("Station"));     
    }
}
