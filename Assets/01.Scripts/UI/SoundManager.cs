using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Slider masterSlider;
    private Slider backgroundSlider;
    private Slider effectSlider;

    private const string MIXER_MASTER = "Master";
    private const string MIXER_BACKGROUND = "Background";
    private const string MIXER_EFFECT = "Effect";

    

    private void Awake()
    {
        instance = this;

        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetMusicVolum(float value)
    {
        audioMixer.SetFloat(MIXER_BACKGROUND, value);
    }
}