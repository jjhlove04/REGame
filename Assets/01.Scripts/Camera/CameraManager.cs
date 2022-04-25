using Cinemachine;
using System;
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

    public Action TopView;
    public Action QuarterView;

    private float cameraChangeTime = 2;
    private float cameraChangeCurTime;

    private bool canChangeCamera = true;

    private void Awake()
    {
        cameraChangeCurTime = cameraChangeTime;

        instance = this;


        TopView = () => { };
        QuarterView = () => { };
    }

    private void Update()
    {
        if (canChangeCamera)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                canChangeCamera = false;

                CameraChangeView();
            }
        }

        else
        {
            cameraChangeCurTime -= Time.unscaledDeltaTime;

            if (cameraChangeCurTime < 0)
            {
                canChangeCamera = true;

                cameraChangeCurTime = cameraChangeTime;
            }
        }

        if (topCamera.activeSelf)
        {
            TopView.Invoke();
        }

        else
        {
            QuarterView.Invoke();
        }
    }

    [SerializeField]
    private GameObject topCamera;

    [SerializeField]
    private GameObject quaterCamera;

    public void CameraChangeView()
    {
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