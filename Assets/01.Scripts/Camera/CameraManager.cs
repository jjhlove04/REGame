using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;

    public static CameraManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;

        cinemachineBrain = GetComponent<CinemachineBrain>();
    }
    private CinemachineBrain cinemachineBrain;

    [SerializeField]
    private GameObject topCamera;

    [SerializeField]
    private GameObject quaterCamera;

    public void CameraChangeView()
    {
        cinemachineBrain.m_DefaultBlend.m_Time = 2.5f * Time.timeScale;
        topCamera.SetActive(!topCamera.activeSelf);
        quaterCamera.SetActive(!quaterCamera.activeSelf);
    }


    public void Shake(float duration, float magnitude)
    {
        if (topCamera.activeSelf)
        {
            topCamera.GetComponent<CameraShake>().Shake(duration, magnitude);
        }

        if (quaterCamera.activeSelf)
        {
            quaterCamera.GetComponent<CameraShake>().Shake(duration, magnitude);
        }
    }
}