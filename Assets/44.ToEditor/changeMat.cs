using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeMat : MonoBehaviour
{
    public Button changeBtn;

    public Renderer[] rend;

    public List<Texture> _textures = new List<Texture>();
    // Start is called before the first frame update
    void Start()
    {
        rend = FindObjectsOfType<Renderer>();
        changeBtn.onClick.AddListener(ChangeTexture);
    }

    void ChangeTexture()
    {
        int randooom = Random.Range(0, _textures.Count);
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].material.mainTexture = _textures[randooom];
        }
        
    }
}
