using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager _instance = new TutorialManager();
    public bool isFirstTutorial = true;
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);

    }
    private void Start() {
        
    }

    public void TitleTuto()
    {
        
    }
    
}
