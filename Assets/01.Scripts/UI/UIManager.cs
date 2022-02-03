using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _ui = new UIManager();
    public static UIManager UI { get { return _ui; } }
    public List<Button> panelOpenList = new List<Button>();

    private void Start() {
        
    }

}
