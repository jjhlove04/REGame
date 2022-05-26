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

    private float cameraChangeTime = 1;
    private float cameraChangeCurTime;

    private bool canChangeCamera = true;

    private GameObject curCamera;

    private Transform curCameraTrm;

    [SerializeField]
    private GameObject nuclearCamera;

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
        if (canChangeCamera && curCamera == null)
        {
            topCamera.SetActive(!topCamera.activeSelf);
            quaterCamera.SetActive(!quaterCamera.activeSelf);

            canChangeCamera = false;
        }
    }


    public void Shake(float duration, float magnitude)
    {
        if(topCamera.activeSelf)
            topCamera.GetComponent<CameraShake>().Shake(duration, magnitude);

        else if(quaterCamera.activeSelf)
            quaterCamera.GetComponent<CameraShake>().Shake(duration, magnitude);

        if(curCamera != null)
        {
            curCamera.GetComponent<CameraShake>().Shake(duration, magnitude);
        }
    }

    public bool ReturnCanCam()
    {
        return canChangeCamera;
    }

    public void OnNuclearView()
    {
        if (topCamera.activeSelf)
        {
            curCamera = topCamera;

            curCameraTrm = null;
        }

        else
        {
            curCamera = quaterCamera;

            curCameraTrm = quaterCamera.transform;

            topCamera.SetActive(true);

            quaterCamera.SetActive(false);
        }

        topCamera.GetComponent<PanAndZoom>().targetorthographicSize = 185;
    }

    public void OffNuclearView()
    {
        curCamera = null;
        //topCamera.SetActive(true);

        /*if(curCameraTrm != null)
        {
            curCamera.transform.position = curCameraTrm.position;
        }

        curCamera = null;
        curCameraTrm = null;*/
    }

    public bool IsTopView()
    {
        return topCamera.activeSelf;
    }
}