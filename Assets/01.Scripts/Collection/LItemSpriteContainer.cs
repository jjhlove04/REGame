using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LItemSpriteContainer : MonoBehaviour
{
    public Sprite[] itemSprites;
    private static LItemSpriteContainer _instance = null;
    public static LItemSpriteContainer Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LItemSpriteContainer>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }

}
