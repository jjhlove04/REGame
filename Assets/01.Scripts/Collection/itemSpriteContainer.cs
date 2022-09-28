using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpriteContainer : MonoBehaviour
{
    public Sprite[] itemSprites;
    private static ItemSpriteContainer _instance = null;
    public static ItemSpriteContainer Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ItemSpriteContainer>();
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
