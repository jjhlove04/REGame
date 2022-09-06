using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemSpriteContainer : MonoBehaviour
{
    public Sprite[] itemSprites;
    public static itemSpriteContainer _instance = new itemSpriteContainer();

    private void Awake() {
        _instance = this;
    }
}
